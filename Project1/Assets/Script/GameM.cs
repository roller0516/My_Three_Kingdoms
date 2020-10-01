using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameM : MonoBehaviour
{

    public int SceneNumber_cur = 0;

    private static GameM s_instance = null;
    
    public static GameM Instance 
    {
        get 
        {
            if (s_instance == null) 
            {
                s_instance = FindObjectOfType(typeof(GameM)) as GameM;
            }
            return s_instance;
        }
    }
    private void Awake()
    {
        if (s_instance != null) 
        {
            return;
        }
        s_instance = this;

        DontDestroyOnLoad(this.gameObject);

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        Application.targetFrameRate = 30;
    }
    public void Init()
    {
        PlayerPrefs.DeleteAll();
    }

    public void SceneChagne(int number) 
    {
        SceneManager.LoadScene(number);
        SceneNumber_cur = number;
        if (SceneNumber_cur != 0) //로딩시 화면처리
        {

        }
    }
  
}
