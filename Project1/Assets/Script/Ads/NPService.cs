#define USE_NP
using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
#if USE_NP
using VoxelBusters.NativePlugins;
using VoxelBusters.NativePlugins.Internal;
#endif

namespace ProjectD
{
    public class NPService : MonoBehaviour
    {
        public static NPService Instance { get; private set; }
        private void Awake()
        {
            if (Instance) Destroy(gameObject);
            else
            {
                Instance = this;
            }
        }
    
        private void Start()
        {
        }
        
        protected void OnEnable ()
        {
#if USES_CLOUD_SERVICES
            // Register to event
            CloudServices.KeyValueStoreDidInitialiseEvent		+= OnKeyValueStoreDidInitialise;
            CloudServices.KeyValueStoreDidSynchroniseEvent		+= OnKeyValueStoreDidSynchronise;
            CloudServices.KeyValueStoreDidChangeExternallyEvent	+= OnKeyValueStoreChanged;
#endif
                        
        }
		
        protected void OnDisable ()
        {
#if USES_CLOUD_SERVICES
            // Deregister to event
            CloudServices.KeyValueStoreDidInitialiseEvent		-= OnKeyValueStoreDidInitialise;
            CloudServices.KeyValueStoreDidSynchroniseEvent		-= OnKeyValueStoreDidSynchronise;
            CloudServices.KeyValueStoreDidChangeExternallyEvent	-= OnKeyValueStoreChanged;
#endif
            
        }
    
        #region GAME_SERVICES

#if USES_GAME_SERVICES
        public bool UseLeaderboard = true;
        public bool UseAchievement = true;
    
    
        public void CreateGameServiceData()
        {
            // 리더보드 Key 입력
            if (UseLeaderboard && GameServicesUtils.leaderboardMetadataCollection != null)
            {
                foreach (var data in GameServicesUtils.leaderboardMetadataCollection)
                {
                    CreateLeaderboardWithGlobalID(data.GlobalID);
                }
            }

            if (UseAchievement && GameServicesUtils.achievementMetadataCollection != null)
            {
                foreach (var data in GameServicesUtils.achievementMetadataCollection)
                {
                    CreateAchievementWithGlobalID(data.GlobalID);
                }
            }
        }
    
        public void Login(Action success)
        {
            NPBinding.GameServices.LocalUser.Authenticate((bool _success, string _error) =>
            {
                if (_success)
                {
                    Debug.Log("Local User Details : " + NPBinding.GameServices.LocalUser.ToString());
                    success?.Invoke();

                    CreateGameServiceData();
                    NPBinding.CloudServices.Initialise();
                    NPBinding.CloudServices.Synchronise ();
                }
                else
                {
                    Debug.Log("Sign-In Failed with error " + _error);
                }
            });
        }

        #region Leaderboard
        private Leaderboard CreateLeaderboardWithGlobalID(string leaderboardGid)
        {
            var m_curLeaderboard = NPBinding.GameServices.CreateLeaderboardWithGlobalID(leaderboardGid);
            m_curLeaderboard.MaxResults = 15;

            return m_curLeaderboard;
        }

        // 기록
        public void ReportScoreWithGlobalID(string leaderboardGid, long score)
        {
            NPBinding.GameServices.ReportScoreWithGlobalID(leaderboardGid, score, (bool _success, string _error) =>
            {

                if (_success)
                {
                    Debug.Log(string.Format("Request to report score to leaderboard with GID= {0} finished successfully.", leaderboardGid));
                    Debug.Log(string.Format("New score= {0}.", score));
                }
                else
                {
                    Debug.Log(string.Format("Request to report score to leaderboard with GID= {0} failed.", leaderboardGid));
                    Debug.Log(string.Format("Error= {0}.", _error));
                }
            });
        }

        public void ShowLeaderboardUIWithGlobalID(string leaderboadGid)
        {
            Debug.Log("Sending request to show leaderboard view.");

            NPBinding.GameServices.ShowLeaderboardUIWithGlobalID(leaderboadGid, eLeaderboardTimeScope.ALL_TIME, (string _error) =>
            {
                Debug.Log("Leaderboard view dismissed.");
                Debug.Log(string.Format("Error= {0}.", _error));
            });
        }

        #endregion

        #region Achievement
        private Achievement CreateAchievementWithGlobalID(string achievementGid)
        {
            return NPBinding.GameServices.CreateAchievementWithGlobalID(achievementGid);
        }

        public void ReportProgressWithGlobalId(string achievementGid)
        {
            int _noOfSteps = NPBinding.GameServices.GetNoOfStepsForCompletingAchievement(achievementGid);
            int _randomNo = Random.Range(0, _noOfSteps + 1);
            double _progress = 100;// ((double)_randomNo / _noOfSteps) * 100d;

            // If its an incremental achievement, make sure you send a incremented cumulative value everytime you call this method
            NPBinding.GameServices.ReportProgressWithGlobalID(achievementGid, _progress, (bool _status, string _error) =>
            {

                if (_status)
                {
                    Debug.Log(string.Format("Request to report progress of achievement with GID= {0} finished successfully.", achievementGid));
                    Debug.Log(string.Format("Percentage completed= {0}.", _progress));
                }
                else
                {
                    Debug.Log(string.Format("Request to report progress of achievement with GID= {0} failed.", achievementGid));
                    Debug.Log(string.Format("Error= {0}.", _error));
                }
            });
        }

        public void ShowAchievementsUi()
        {
            Debug.Log("Sending request to show achievements view.");

            NPBinding.GameServices.ShowAchievementsUI((string _error) =>
            {
                Debug.Log("Achievements view dismissed.");
                Debug.Log(string.Format("Error= {0}.", _error));
            });
        }
        #endregion
#endif

        #endregion
        #region CLOUD_SERVICES
#if USES_CLOUD_SERVICES
        public bool initCloud = false;
        public void SetBool (string key, bool value)
        {
            NPBinding.CloudServices.SetBool(key, value);
        }

        public bool GetBool (string key)
        {
            return NPBinding.CloudServices.GetBool(key);
        }
		
        public void SetLong (string key, long value)
        {
            NPBinding.CloudServices.SetLong(key, value);
        }
		
        public long GetLong (string key)
        {
            return NPBinding.CloudServices.GetLong(key);
        }
		
        public void SetDouble (string key, double value)
        {
            NPBinding.CloudServices.SetDouble(key, value);
        }
		
        public double GetDouble (string key)
        {
            return NPBinding.CloudServices.GetDouble(key);
        }
		
        public void SetString (string key, string value)
        {
            NPBinding.CloudServices.SetString(key, value);
        }
		
        private string GetString (string key)
        {
            return NPBinding.CloudServices.GetString(key);
        }
        
        public void ClearAllData ()
        {
            NPBinding.CloudServices.RemoveAllKeys();
        }

        // private void SetDictionary ()
        // {
        //     m_dictionaryValue.Add ("Key", "Value");
        //     NPBinding.CloudServices.SetDictionary(kKeyForDictionaryValue, m_dictionaryValue);
        // }
        //
        // private void GetDictionary ()
        // {
        //     Dictionary<string, object> dict		= NPBinding.CloudServices.GetDictionary(kKeyForDictionaryValue) as Dictionary<string, object>;
        //     m_dictionaryValue = dict.ToDictionary (kvp => kvp.Key, kvp => (string)kvp.Value);
        // }
        public void Synchronise ()
        {
            NPBinding.CloudServices.Synchronise();
        }
        private void OnKeyValueStoreDidInitialise (bool success)
        {
            if (success)
            {
                print("Successfully Initialised keys and values.");
                initCloud = true;
            }
            else
            {
                print("Failed initialising keys and values.");
            }
        }

        private void OnKeyValueStoreDidSynchronise (bool success)
        {
            if (success)
            {
                print("Successfully synchronised in-memory keys and values.");
            }
            else
            {
                print("Failed to synchronise in-memory keys and values.");
            }
        }

        private void OnKeyValueStoreChanged (eCloudDataStoreValueChangeReason reason, string[] changedKeys)
        {
            //AddNewResult("Cloud key-value store has been changed.");
            //AppendResult(string.Format("Reason: {0}.", _reason));

            if (changedKeys != null)
            {
                //AppendResult(string.Format("Total keys changed: {0}.", _changedKeys.Length));
                //AppendResult(string.Format("Pick a value from old and new and set the value to cloud for resolving conflict."));
					
                foreach (string changedKey in changedKeys)
                {
                    //                if (_changedKey.Equals(kKeyForBoolValue))
                    // {
                    // 	//AppendResult(string.Format("New value for key: {0} is {1}. Old value is {2}", _changedKey, NPBinding.CloudServices.GetBool(_changedKey), m_boolValue)); 
                    // }
                    // else if (_changedKey.Equals(kKeyForLongValue)) // Shows example of resolving a conflict
                    // {
                    // 	long _newCloudLongValue = NPBinding.CloudServices.GetLong(_changedKey);
                    // 	//AppendResult(string.Format("New value for key: {0} is {1}. Old value is {2}", _changedKey, _newCloudLongValue, m_longValue)); 
                    // 	// Lets assume, we pick the bigger value and set it here.
                    // 	long _biggerValue = _newCloudLongValue > m_longValue ? _newCloudLongValue : m_longValue;
                    // 	NPBinding.CloudServices.SetLong(_changedKey, _biggerValue);
                    // 	//AppendResult(string.Format("Picking bigger value {0} and setting to cloud.", _biggerValue));           
                    // }
                    // else if (_changedKey.Equals(kKeyForDoubleValue))
                    // {
                    // 	//AppendResult(string.Format("New value for key: {0} is {1}. Old value is {2}", _changedKey, NPBinding.CloudServices.GetDouble(_changedKey), m_doubleValue)); 
                    // }
                    // else if (_changedKey.Equals(kKeyForStringValue))
                    // {
                    // 	//AppendResult(string.Format("New value for key: {0} is {1}. Old value is {2}", _changedKey, NPBinding.CloudServices.GetString(_changedKey).GetPrintableString(), m_stringValue)); 
                    // }
                }
            }
        }
#endif
        #endregion
      
    }
}