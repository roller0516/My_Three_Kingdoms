// 게임 기준 시간

using UnityEngine;
using System.Collections;

using System;


//----------------------------------------------------------------------------------------

public enum eRetryDelay //연타 금지.
{
    None = 0,
    message,
    ranking,
    match_leave,
    match_rematch,
    buy_item,
    charge,
    max
}

//----------------------------------------------------------------------------------------
public class CGameTime : MonoBehaviour 
{
    public static string datePatt = "yyyy-MM-dd HH:mm:ss";
    //public static string datePatt = "yyyy/MM/dd hh:mm:ss tt";
    
    //DateTime newdate = DateTime.Now;
    //newdate = DateTime.ParseExact(mystring,datePatt,null);
    //newdate = new DateTime(2011, 3, 15, 2, 47, 0);		
    //DateTime saveNow = DateTime.Now;
    //saveNow = DateTime.Now;
    //print( saveNow.ToString(datePatt) );		

    private static CGameTime s_instance = null;	
	public static CGameTime Instance
	{
		get 
	    {
	    	if (s_instance == null)
	        {
	        	s_instance //= new CGameTime();
					= FindObjectOfType(typeof(CGameTime)) as CGameTime;
	        }
	        return s_instance;
	    }
	}

	void Awake () 
	{
		if(s_instance != null) 
		{ 
			//Debug.LogError("Cannot have two instances of CGameTime."); 
			return; 
		}
		s_instance = this;


        RetryDelay_init();

        TimeStamp_init();
        TimeStamp_reset((int)eTimeStamp.SceneUpdate);

        DontDestroyOnLoad(this);
		//Debug.Log("CGameTime Start");		
	}	
	
	// Use this for initialization
	void Start () 
	{			
	}
    /*	
        //string t_playtime_last = "";	
        void Update () {

            //if( Input.GetKeyDown( KeyCode.Alpha1) )		{	t_playtime_last = GetNowTime();		}		
            //if( Input.GetKeyDown( KeyCode.Alpha2) )		{	SetPlayTime( 8, t_playtime_last );	print( GetPlaytime_remain() );		}		
        }
    */



    //----------------------------------------------------------------------------------

    // 현재시간 string 값을 얻어온다. .
    public string GetNowTime()	{	return DateTime.Now.ToString(datePatt); }

    //DateTime, string 형변환.
	public string DateToString(DateTime _date)
	{
		return _date.ToString(datePatt);
	}	
	public DateTime StringToDate(string _string)
	{
		return DateTime.ParseExact(_string, datePatt, null);
	}

    public bool Time_is_over(string _timestr) // 가능한 시간인가? // 시간이 지났는가?
    {
        if (_timestr == "0") return false;
        if (_timestr == "000000000000") return false;

        DateTime date = DateTime.ParseExact(_timestr, "yyyyMMddHHmm", null);
        if (DateTime.Now > date)  return true;

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
    //----------------------------------------------------------------------------------
    public string GetRemainTime( DateTime _time_goal )
	{
		string rt = "";
		TimeSpan ts_remainingtime = _time_goal - DateTime.Now;
        if (ts_remainingtime.Seconds < 0) return "00:00";

		////print("" + ts_remainingtime.ToString());		
        if (ts_remainingtime.Days > 0)  // 1 days
            rt = String.Format("{0} days", ts_remainingtime.Days);
        if (ts_remainingtime.Hours > 0) // 00:00:00
            rt = String.Format("{0:00}:{1:00}:{2:00}", ts_remainingtime.Hours, ts_remainingtime.Minutes, ts_remainingtime.Seconds);
        else                            // 00:00
            rt = String.Format("{0:00}:{1:00}", ts_remainingtime.Minutes, ts_remainingtime.Seconds);
			
		//if( ts_remainingtime.Days > 0 ) 		    rt = ts_remainingtime.Days + CGame.Instance.GetText(10100);
		//else if( ts_remainingtime.Hours > 0 )	    rt = ts_remainingtime.Hours + CGame.Instance.GetText(10101);
		//else if( ts_remainingtime.Minutes > 0 )	rt = ts_remainingtime.Minutes + CGame.Instance.GetText(10102);
		//else 								 	    rt = ts_remainingtime.Seconds + CGame.Instance.GetText(10103);
		
		return rt;
	}
    public int GetRemainTime_sec(DateTime _time_goal)
    {
        double sec = 0;
        TimeSpan ts_remainingtime = _time_goal - DateTime.Now;
        if (ts_remainingtime.Seconds < 0) return 0;

        sec = ts_remainingtime.TotalSeconds;
        return (int)sec;
    }
    public string GetTimeFromRemain(int _time_remain) //분-> 현재기준 날자.
    {
        string rt = "";

        DateTime Goal_time = DateTime.Now.AddMinutes(_time_remain);
        rt = String.Format("{0:0000}.{1:00}.{2:00}", Goal_time.Year, Goal_time.Month, Goal_time.Day);
        return rt;
    }

    public static DateTime Delay(int MS)
    {
        DateTime ThisMoment = DateTime.Now;
        TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
        DateTime AfterWards = ThisMoment.Add(duration);
        while (AfterWards >= ThisMoment)
        {
            ThisMoment = DateTime.Now;
        }
        return DateTime.Now;
    }

    // time stamp -----------------------------------------------------------------	
    public enum eTimeStamp //일정주기간격 호출.
    {
        None = 0,
        SceneUpdate,
        StageProcess,
        max
    }
    private float[] fTimeStamp = new float[(int)eTimeStamp.max];

    public void TimeStamp_init()
    {
        for (int i = 0; i < fTimeStamp.Length; i++)
            TimeStamp_reset(i);
    }

    public void TimeStamp_reset(int UID)
    {
        fTimeStamp[UID] = Time.time;
    }

    // fTime 초에 한번씩 true //중복 호출 주의. eTimeStamp로 구별.	
    public bool TimeStamp_is(int UID, float fTime)
    {
        if (Time.time > fTimeStamp[UID] + fTime)
        {
            fTimeStamp[UID] = Time.time;
            return true;
        }
        return false;
    }

    // time over ---------------------------------------------------------------
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

    // retry delay, unity -------------------------------------------------------------------
    private bool[] fTimeRetry = new bool[(int)eRetryDelay.max]; //일정시간 다시시도 금지. //eRetryDelay

    public void RetryDelay_init() //초기화
    {
        for (int i = 0; i < fTimeRetry.Length; i++) fTimeRetry[i] = false; //지연중 아님.
    }
    public bool RetryDelay_is(int UID) //true 면 지연중.
    {
        return fTimeRetry[UID];
    }
    public void RetryDelay_set(int UID, float fTime) //일정시간 지연.
    {
        fTimeRetry[UID] = true;
        StartCoroutine(RetryDelay_cancel(UID, fTime));
    }
    IEnumerator RetryDelay_cancel(int UID, float _time)
    {
        yield return new WaitForSeconds(_time);
        fTimeRetry[UID] = false;
    }
}


// example
//CTimeCall kTimeCall;    //스케쥴. // 일정시각에 콜을 알린다.    
//kTimeCall = new CTimeCall(); 
//kTimeCall.SetCall( -1, -1, -1, 0, 0);   //매시 0분.
//kTimeCall.SetCall( -1, -1, 0, 0, 0);    //매일 0시 0분.
//kTimeCall.SetCall( -1, 2, 11, 0, 0);    //매주 화요일 11시.
//if ( kTimeCall.IsCall() )    {    }

public class CTimeCall
{
    DateTime kDate;
    public int iTimeCall_count = 0;     // 체크할 갯수.

    public int iTimeCall_day = -1;
    public int iTimeCall_week = -1;     // 0-6 일-토.
    public int iTimeCall_hour = -1;
    public int iTimeCall_min = -1;
    public int iTimeCall_sec = -1;
    public int iTimeCall_sec2 = -1;     //초단위 갱신이므로 확인용.

    // 각 매월,매주,매시,매분,매초 일 때 알려주는 세팅.
    public void SetCall(int _day, int _week, int _hour, int _min, int _sec)
    {
        iTimeCall_count = 0;

        iTimeCall_day = _day;
        iTimeCall_week = _week;
        iTimeCall_hour = _hour;
        iTimeCall_min = _min;
        iTimeCall_sec = _sec;
        iTimeCall_sec2 = iTimeCall_sec + 1; if (iTimeCall_sec2 >= 60) iTimeCall_sec2 = 59;

        if (_day != -1) iTimeCall_count++;
        if (_week != -1) iTimeCall_count++;
        if (_hour != -1) iTimeCall_count++;
        if (_min != -1) iTimeCall_count++;
        if (_sec != -1) iTimeCall_count++;

        kDate = DateTime.Now;
    }

    public bool IsCall()
    {
        DateTime kNow = DateTime.Now;

        if (kNow < kDate) return false; // 최소시간 간격 동안 중복체크 방지.

        int count = 0;

        if (iTimeCall_day != -1) if (kNow.Day == iTimeCall_day) count++;
        if (iTimeCall_week != -1) if (Convert.ToInt16(kNow.DayOfWeek) == iTimeCall_week) count++;
        if (iTimeCall_hour != -1) if (kNow.Hour == iTimeCall_hour) count++;
        if (iTimeCall_min != -1) if (kNow.Minute == iTimeCall_min) count++;
        if (iTimeCall_sec != -1) if (kNow.Second == iTimeCall_sec || kNow.Second == iTimeCall_sec2) count++;

        if (iTimeCall_count == count) // 체크가 되었다. 중복체크 못하도록 타임 추가.
        {
            if (iTimeCall_day != -1) kDate = kNow.AddDays(1);
            else if (iTimeCall_week != -1) kDate = kNow.AddDays(1);
            else if (iTimeCall_hour != -1) kDate = kNow.AddHours(1);
            else if (iTimeCall_min != -1) kDate = kNow.AddMinutes(1);
            else if (iTimeCall_sec != -1) kDate = kNow.AddSeconds(2);

            return true;
        }

        return false;
    }
}

//CTimeUnit kTime = new CTimeUnit();
//string lasttime = kTime.SetGoalTime(5);
//MyUtil.print("" + kTime.GetString_remain_time());

public class CTimeUnit
{
    public static string datePatt = "yyyy-MM-dd HH:mm:ss";
    //public static string datePatt = "yyyy/MM/dd hh:mm:ss tt";

    DateTime m_time_start;
    DateTime m_time_goal;

    public string SetGoalTime(int _sec) //n초후로 설정
    {
        m_time_start = DateTime.Now;
        m_time_goal = DateTime.Now.AddSeconds(_sec);

        string goal_time = m_time_goal.ToString(datePatt);
        return goal_time;
    }

    public string SetGoalTime(string _time_last) //특정 시간으로 설정
    {
        m_time_start = DateTime.Now;
        m_time_goal = DateTime.ParseExact(_time_last, datePatt, null);

        string goal_time = m_time_goal.ToString(datePatt);
        return goal_time;
    }

    public bool IsTimeOver() //경과여부
    {
        TimeSpan ts_time = m_time_goal - DateTime.Now;
        if (ts_time.TotalSeconds <= 0) return true;
        return false;
    }

    public int GetRemainTime() //남은시간,초. //0 이하면 기간이 지난것.
    {
        TimeSpan ts_time = m_time_goal - DateTime.Now; //현재시간기준
        int sec = (int)ts_time.TotalSeconds;
        return sec;
    }

    public string GetRemainTimeString()
    {
        string rt = "";
        TimeSpan ts_remainingtime = m_time_goal - DateTime.Now;
        //print ("" + ts_remainingtime.ToString());
        if (ts_remainingtime.TotalSeconds > 0)
            rt = String.Format("{0:00}:{1:00}:{2:00}", ts_remainingtime.Hours, ts_remainingtime.Minutes, ts_remainingtime.Seconds);
        else
            rt = "00:00:00";           
        
        return rt;
    }

    public string GetRemainTimeString2()
    {
        string rt = "";
        TimeSpan ts_remainingtime = m_time_goal - DateTime.Now;
        //print ("" + ts_remainingtime.ToString());	
        if (ts_remainingtime.Seconds > 0)
            rt = String.Format("{0:00}:{1:00}", ts_remainingtime.Minutes, ts_remainingtime.Seconds);
        else
            rt = String.Format("00:00");

        return rt;
    }

}

//CTimePoint kTP_stamina = new CTimePoint();
//kTP_stamina.TimePoint_init( 60*5 , sp, sp_max, DateTime.Now, 0 );
//kTP_stamina.TimePoint_init_client(60*5, stamina, 500, _last_time); 

public class CTimePoint
{
    public static string datePatt = "yyyy-MM-dd HH:mm:ss";
    //public static string datePatt = "yyyy/MM/dd hh:mm:ss tt";

    int m_TimePoint_time = 30;  //포인트 증가시간. sec

    int m_TimePoint_cur = 0;
    int m_TimePoint_max = 10;
    public int Get_point_max() { return m_TimePoint_max; }
    public void Set_point_max(int _value) { m_TimePoint_max = _value; }

    DateTime m_last_add_time = DateTime.Now;   // 업데이트 한 최종 시간.
    DateTime m_next_add_time = DateTime.Now;   // 포인트 증가해야 할 시간.
    public DateTime Get_last_time() { return m_last_add_time; }
    public DateTime Get_next_time() { return m_next_add_time.AddSeconds(1); }
    public string Get_last_time_string() { return m_last_add_time.ToString(datePatt); }
    public string Get_next_time_string() { return m_next_add_time.ToString(datePatt); }

    public void AddPoint(int _value) //for server //실재 연산할때 사용.
    {
        TimePoint_update(); //recalc time //갱신후 추가.
        Set_point_cur(m_TimePoint_cur + _value);
    }

    // m_TimePoint 를 얻어온다.
    public int Get_point_cur() { return m_TimePoint_cur; }
    public void Set_point_cur(int _value) //server
    {
        m_TimePoint_cur = _value;   //경과시간 유지.

        if (m_TimePoint_cur < 0)
        {
            m_TimePoint_cur = 0;
            m_last_add_time = DateTime.Now;
            m_next_add_time = m_last_add_time.AddSeconds(m_TimePoint_time); //경과시간 리셋.
        }
        if (m_TimePoint_cur >= m_TimePoint_max)
        {
            //m_TimePoint_cur = m_TimePoint_max; //한도초과 허용.
            m_last_add_time = DateTime.Now;
            m_next_add_time = m_last_add_time.AddSeconds(m_TimePoint_time); //경과시간 리셋.
        }
    }

    // 최종값을 입력하여 재세팅한다. //초기화.
    public void TimePoint_init(int _timepoint_time, int _timepoint_cur, int _timepoint_max, string _last_add_time)
    {
        // init set 
        m_TimePoint_time = _timepoint_time;
        m_TimePoint_max = _timepoint_max;
        m_TimePoint_cur = _timepoint_cur;
        // server
        m_last_add_time = DateTime.ParseExact(_last_add_time, datePatt, null);  // 업데이트 한 최종 시간.
        m_next_add_time = m_last_add_time.AddSeconds(m_TimePoint_time);         // 다음 포인트 증가할 시간
                                                                                //TimePoint_update(); //recalc //m_next_add_time
    }
    //DB에 현재시간,경과시간 저장 - DateTime.Now, Get_past_time_sec()
    //DB에서 불러온 시간으로 재세팅.
    public void TimePoint_reset(int _timepoint_cur, string _last_add_time) //server from db
    {
        m_TimePoint_cur = _timepoint_cur;
        // server
        m_last_add_time = DateTime.ParseExact(_last_add_time, datePatt, null);  // 업데이트 한 최종 시간.

        TimePoint_update(); //recalc //m_next_add_time
    }

    public void TimePoint_update()  //server //호출하는 순간 경과시간 계산. //m_next_add_time, m_TimePoint_cur
    {
        if (m_TimePoint_cur >= m_TimePoint_max) //max 이상이면 그냥 리턴.
        {
            m_last_add_time = DateTime.Now;
            m_next_add_time = m_last_add_time.AddSeconds(m_TimePoint_time); //경과시간 리셋.
            return;
        }

        double past_time_sec = Get_past_time_sec();
        int point_add = (int)(past_time_sec / m_TimePoint_time);                    // 흘러간 시간의 포인트.
        //MyUtil.print(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> " + m_last_add_time + " " + point_add);
        if (point_add >= 1) // 포인트가 증가되었다면.
        {
            int sec_past = (int)(past_time_sec - point_add * m_TimePoint_time);     // 포인트 단위 시간 흘러간 후의 남은 시간.
            m_last_add_time = DateTime.Now.AddSeconds(-sec_past);                   // 최종 m_timepoint_lasttime
        }
        m_next_add_time = m_last_add_time.AddSeconds(m_TimePoint_time);             // 다음 포인트 증가할 시간

        //--------------------------------------------------------------------------------------------------
        m_TimePoint_cur = m_TimePoint_cur + point_add;
        if (m_TimePoint_cur >= m_TimePoint_max)
        {
            m_TimePoint_cur = m_TimePoint_max; //한도초과.
            m_last_add_time = DateTime.Now;
            m_next_add_time = m_last_add_time.AddSeconds(m_TimePoint_time); //경과시간 리셋.
        }
        //MyUtil.print(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> " + m_TimePoint_cur);
    }

    public double Get_past_time_sec() //경과시간. 디비에 저장후 로딩해서 시간 재계산. //past_time_sec
    {
        TimeSpan ts_past = DateTime.Now - m_last_add_time;
        double past_time_sec = ts_past.TotalSeconds; if (past_time_sec < 0) past_time_sec = 0; //max
        return past_time_sec;
    }

    //최종값을 입력하여 재세팅한다.  client      
    public void TimePoint_init_client(int _timepoint_time, int _timepoint_cur, int _timepoint_max, string _last_add_time)
    {
        m_TimePoint_time = _timepoint_time;
        m_TimePoint_cur = _timepoint_cur;
        m_TimePoint_max = _timepoint_max;
        m_last_add_time = DateTime.ParseExact(_last_add_time, datePatt, null);
        m_next_add_time = m_last_add_time.AddSeconds(m_TimePoint_time);
    }
    //실 시간단위로 갱신한다. client
    public void TimePoint_update_client()
    {
        //check addtime per time
        TimeSpan ts_time = m_next_add_time - DateTime.Now;
        if (ts_time.TotalSeconds <= 0)
        {
            m_next_add_time = DateTime.Now.AddSeconds(m_TimePoint_time);

            if (m_TimePoint_cur < m_TimePoint_max)
            {
                m_TimePoint_cur += 1;                
            }
        }
    }
    //시간표시
    public string GetString_remain_time()
    {
        if (m_TimePoint_cur >= m_TimePoint_max) return "00:00";

        string rt = "";

        TimeSpan ts_remainingtime = m_next_add_time - DateTime.Now;  //print ("" + ts_remainingtime.ToString());		
        rt = String.Format("{0:00}:{1:00}", ts_remainingtime.Minutes, ts_remainingtime.Seconds);
        return rt;
    }
    public string GetString_remain_time_fullcharge()
    {
        if (m_TimePoint_cur >= m_TimePoint_max) return "00:00";

        string rt = "";

        int sec = 0;
        int rm = (m_TimePoint_max - m_TimePoint_cur) - 1;
        if (rm > 0)  sec = m_TimePoint_time * rm;

        TimeSpan ts_remainingtime = m_next_add_time.AddSeconds(sec) - DateTime.Now;
        rt = String.Format("{0:00}:{1:00}", ts_remainingtime.Minutes, ts_remainingtime.Seconds);
        return rt;
    }

}


/*
	m_play_time_start	= DateTime.Now;	//start time	
	m_playcube_addtime	= DateTime.Now.AddMinutes( GameDefine.PLAYCUBE_ADD_TIME );	// from server
	m_playcube_count 	= 1;								// from server	
		
	m_rank_deadline_time = DateTime.Now.AddMinutes( 5 );	// from server
	//new DateTime(2013,1,10,0,0,0);
		

	// Update is called once per frame
	void Update () 
	{
//		if (!Application.)
		if (Application.isLoadingLevel)
			PlayerPrefs.SetInt("hide", 0);

		
		m_play_time_cur			= DateTime.Now;
		
		//check playcube add per time
		TimeSpan ts_time = m_playcube_addtime - m_play_time_cur;
		if( ts_time.TotalSeconds <= 0 )
		{
			m_playcube_addtime 	= DateTime.Now.AddMinutes( GameDefine.PLAYCUBE_ADD_TIME ); //todo from server
			
			if(m_playcube_count < GameDefine.PLAYCUBE_MAX_COUNT )
				m_playcube_count += 1;
		}
		
		ts_time = m_rank_deadline_time - m_play_time_cur;
		if( ts_time.TotalSeconds <= 0 )
		{
			m_rank_deadline_time = DateTime.Now.AddMinutes( 5 ); // from server
		}
	}

	string GetPlaycubeTime()
	{
		string rt ="";
		
		DateTime dt_addtime = baseScript.GetPlaycubeAddtime();
		
		TimeSpan ts_remainingtime = dt_addtime - DateTime.Now;			
		////print("" + ts_remainingtime.ToString());
		
		rt =	String.Format("{0:00}:{1:00}", ts_remainingtime.Minutes, ts_remainingtime.Seconds);
		
		return rt;
	}

    /// <summary>
    /// 랭킹 마감 시간을 Get 한다 : 
    /// m_rank_deadline_time에 서버에서 넘어온 시간을 받아와서
    /// 현재시간을 뺀 만큼의 마감까지 남은 시간을 string값으로 언더온다..
    /// </summary>
    /// <returns>마감 시간</returns>
	public string GetDeadlineTime()
	{
		string rt ="";

		//DateTime dt_now = DateTime.Now;

		//m_rank_deadline_time = DateTime.Now.AddSeconds(2);
		DateTime deadline_time = GetRankDeadline();
		
		TimeSpan ts_time = deadline_time - DateTime.Now;

		if( ts_time.Days > 0 ) 			rt = ts_time.Days  + CGame.Instance.GetText(1000); //" Days";
		else if( ts_time.Hours > 0 ) 	rt = ts_time.Hours + CGame.Instance.GetText(1001); //" Hours";
		else if( ts_time.Minutes > 0 )	rt = ts_time.Minutes + CGame.Instance.GetText(1002); //" Minutes";		
		else if( ts_time.Seconds >= 0 )	rt = ts_time.Seconds + CGame.Instance.GetText(1003);
		else
		{
			//print("deadline_time over" );
		}
		
		//print( rt);
		
		return rt;
	}	
}
*/
