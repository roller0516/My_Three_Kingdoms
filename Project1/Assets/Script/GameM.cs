using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameM : MonoBehaviour
{

    public int SceneNumber_cur = 0;

    private static GameM instance = null;
    
    public static GameM GetInstance 
    {
        get 
        {
            if (instance == null) 
            {
                instance = FindObjectOfType(typeof(GameM)) as GameM;
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance != null) 
        {
            return;
        }
        instance = this;

        DontDestroyOnLoad(this.gameObject);

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        PlayerPrefs.DeleteAll();
    }

    public void SceneChagne(int number) 
    {
        SceneManager.LoadScene(number);
        SceneNumber_cur = number;
    }
}
