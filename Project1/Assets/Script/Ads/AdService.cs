using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectD
{
    public class AdService : MonoBehaviour
    {
        public static AdService Instance { get; private set; }
        Action completeCallback;
        // Use this for initialization
        private void Awake()
        {
            if (Instance) Destroy(gameObject);
            else
            {
                Instance = this;
            }
        }
        void Start()
        {
            print("== AdService Start");
            Initialize();
        }

        private void Initialize()
        {
            //if user consent was set, just initialize the SDK, else request user consent
            if (Advertisements.Instance.UserConsentWasSet())
            {
                Advertisements.Instance.Initialize();
            }
            else
            {

                Advertisements.Instance.SetUserConsent(true);
                Advertisements.Instance.Initialize();
            }
        }

        public void ShowBanner()
        {
            Advertisements.Instance.ShowBanner(BannerPosition.BOTTOM);
        }

        public void HideBanner()
        {
            Advertisements.Instance.HideBanner();
        }

        public void ShowInterstitial(Action _completeCallback = null)
        {
            //_completeCallback?.Invoke();
            completeCallback = _completeCallback;
            Advertisements.Instance.ShowInterstitial(InterstitialClosed);
        }

        public bool IsInterstitialAvailable()
        {
            return Advertisements.Instance.IsInterstitialAvailable();
        }

        //callback called each time an interstitial is closed
        private void InterstitialClosed(string advertiser)
        {
            completeCallback?.Invoke();
            completeCallback = null;
            if (Advertisements.Instance.debug)
            {
                Debug.Log("Interstitial closed from: " + advertiser + " -> Resume Game ");
            }
        }
        
        public void ShowRewardedVideo(Action _completeCallback)
        {
            completeCallback = delegate
            {
                _completeCallback();
            };

            if (IsRewardVideoAvailable() == false)
            {
                Debug.Log("IsRewardVideoAvailable = false");
#if UNITY_EDITOR
                completeCallback?.Invoke();
                completeCallback = null;
#endif
                return;
            }
            
            Advertisements.Instance.ShowRewardedVideo(CompleteMethod);
        }

        public bool IsRewardVideoAvailable()
        {
            return Advertisements.Instance.IsRewardVideoAvailable();
        }

        private void CompleteMethod(bool completed, string advertiser)
        {
            Debug.Log("Closed rewarded from: " + advertiser + " -> Completed " + completed);
            if (completed == true)
            {
                //give the reward
                completeCallback?.Invoke();
                completeCallback = null;

            }
            else
            {
                //no reward
            }
        }
    }
}
