using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameDef : MonoBehaviour
{
    // local db --------------------------------------------------------------

    public void LocalDB_init() //클라 정보, 최초 1회. 초기값.
	{
        PlayerPrefs.SetString("Game_init", "on");
        //PlayerPrefs.SetInt("isAutoBattle", 1); //자동전투 //default

        //MyPlayer player = CGame.Instance.kPlayer;
        //player.user_id = "";
		//player.nickname = "";
        //player.user_id_Test = "";
        //player.nickname_Test = "";
        //player.user_id_Deve = "";
        //player.nickname_Deve = "";

        LocalDB_save();
	}

    public void LocalDB_load() //클라 정보, 초기화 로딩.
    {
        if (!PlayerPrefs.HasKey("GAR_init"))
        {
            LocalDB_init(); // init
            return;
        }

        //MyPlayer player = CGame.Instance.kPlayer;

        string db = PlayerPrefs.GetString("project_data");  //load                                                            
        print("LoadDatabase : " + db); //+ db.Length

        //DeSerialize -----------------------------

        string[] source = db.Split("\t"[0]);
        int offset = 0;

        string sVersion_saved = source[offset++];
        /*
                string sVersion_reset = "0.000"; //이 버전 이하는 초기화.
                if (sVersion_saved.CompareTo(sVersion_reset) <= 0)   // 리셋버전 이하이다. // live에선 하면 절대 안됨.
                {            
                    //기존의 저장된 데이타를 쓰지 않는다. // 리셋. //캐릭 재생성.            
                    LocalDB_init(); // init
                    print("RESET:    sVersion_reset :" + sVersion_reset + "  sVersion_saved :" + sVersion_saved);
                    return;
                }
        */
        //if (sVersion_saved.CompareTo("0.000") <= 0) //이전버전
        //{
        //	player.user_id = source[offset++];
        //	player.nickname = source[offset++];
        //}
        //else //현재버전
        {
            //player.user_id = source[offset++];
            //player.nickname = source[offset++];
            //player.user_id_Test = source[offset++];
            //player.nickname_Test = source[offset++];
            //player.user_id_Deve = source[offset++];
            //player.nickname_Deve = source[offset++];
        }

    }

    public void LocalDB_save() 
	{
        //MyPlayer player = CGame.Instance.kPlayer;
        
		//Serialize -----------------------------
		string db =
			//player.sPlayerVersion + "\t" + //버전정보..
            CGame.Instance.sVersion_client + "\t" + //버전정보..
            //player.user_id + "\t" +
			//player.nickname + "\t" +
            //player.user_id_Test + "\t" +
            //player.nickname_Test + "\t" +
            //player.user_id_Deve + "\t" +
            //player.nickname_Deve + "\t" +
            "" + "\t" +
            "" + "\t" +
            "" + "\t" +
            "" + "\t" +
            "" + "\t" +
            "";

		PlayerPrefs.SetString("project_data", db);  //save
		//print("LocalDB_save " + db);
	}


    public static string GetGuid(int _length) //랜덤 스트링
    {
        String sSeed = Guid.NewGuid().ToString().Replace("-", "");
        return sSeed.Substring(0, _length);
    }

}
/*
class MyTime
{
    public static readonly MyTime Instance = new MyTime();

    public static string datePatt = "yyyy-MM-dd HH:mm:ss";
    //public static string datePatt = "yyyy/MM/dd hh:mm:ss tt";

    //System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); = 연(yyyy)월일 24시간제 시분초 
    //System.DateTime.Now.ToString("yyyyMMddhhmmss"); = 연(yyyy)월일 12시간제 시분초
    //DateTime.ParseExact("199412310000","yyyyMMddhhmmss",null)

    //var timeout = Environment.TickCount + 10000; 
    
    // 현재시간 string 값을 얻어온다.
    public string GetNowTime()
    {
        return DateTime.Now.ToString(datePatt);
    }

    //str = MyTime.Instance.DateToString((DateTime)datetime);
    public string DateToString(DateTime _date)
    {
        return _date.ToString(datePatt);
    }
    //DateTime datetime = MyTime.Instance.StringToDate(str);
    public DateTime StringToDate(string _string)
    {
        return DateTime.ParseExact(_string, datePatt, null);
    }
    public bool Time_is_over(string _timestr) // 가능한 시간인가? // 시간이 지났는가?
    {
        if (_timestr == "0") return false;
        if (_timestr == "000000000000") return false;

        DateTime date = DateTime.ParseExact(_timestr, "yyyyMMddHHmm", null);
        if (DateTime.Now > date) return true;

        return false;
    }
    public bool IsCanTime_hour(int _fr_hour, int _to_hour) //가능한 시간대 인가?
    {
        if (_fr_hour < 0 || _fr_hour > 24) return false;
        if (_to_hour < 0 || _to_hour > 24) return false;
        if (DateTime.Now.Hour >= _fr_hour && DateTime.Now.Hour < _to_hour)
            return true;

        return false;
    }

    // time stamp -----------------------------------------------------
    private static int iTimeStamp_max = 99;     // time stamp
    private DateTime[] kTimeStamp = new DateTime[iTimeStamp_max];

    public void InitTimeStamp(int UID)
    {
        kTimeStamp[UID] = DateTime.Now;
    }

    // fTime 초에 한번씩 true //중복 호출 주의.	
    public bool IsTimeStamp(int UID, double fTime)
    {
        TimeSpan ts = DateTime.Now - kTimeStamp[UID];
        if (ts.TotalSeconds >= fTime)
        {
            kTimeStamp[UID] = DateTime.Now;
            return true;
        }
        return false;
    }

    // time over -----------------------------------------------------
    DateTime kTimeOver;
    public void InitTimeOver()
    {
        kTimeOver = DateTime.Now;
    }
    public bool IsTimeOver(double fTime)
    {
        TimeSpan ts = DateTime.Now - kTimeOver;
        if (ts.TotalSeconds > fTime)
        {
            return true;
        }
        return false;
    }

}

//CTimeUnit kTime = new CTimeUnit();
//string lasttime = kTime.CreateTime(5);
//MyApplication.print("" + kTime.GetString_remain_time());

public class CTimeUnit
{
	public static string datePatt = "yyyy-MM-dd HH:mm:ss";
	//public static string datePatt = "yyyy/MM/dd hh:mm:ss tt";

	DateTime m_time_start;
	DateTime m_time_goal;


	public string CreateTime(int _sec)
	{
		m_time_start = DateTime.Now;
		m_time_goal = DateTime.Now.AddSeconds(_sec);

		string nowtime = DateTime.Now.ToString(datePatt);
		return nowtime;
	}

	public void SetGoalTime(string _time_last)
	{
		m_time_goal = DateTime.ParseExact(_time_last, datePatt, null);
	}

	public int GetRemainTime()
	{
		TimeSpan ts_time = m_time_goal - DateTime.Now;
		int sec = (int)ts_time.TotalSeconds;
		return sec;
	}

	public string GetString_remain_time()
	{
		string rt = "";
		TimeSpan ts_remainingtime = m_time_goal - DateTime.Now;
		//print ("" + ts_remainingtime.ToString());	
		if (ts_remainingtime.Seconds > 0) {
			rt = String.Format("{0:00}:{1:00}", ts_remainingtime.Minutes, ts_remainingtime.Seconds);
		} else {
			rt = String.Format ("00:00");
		}

		return rt;
	}

}
*/