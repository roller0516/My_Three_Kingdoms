using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataController : MonoBehaviour
{
    public Text Text_;
    private int Food;
    
    public TimeScroll[] time_scroll;
    void Start()
    {   for (int i = 0; i > time_scroll.Length; ++i) 
        {
            time_scroll[i] = GameObject.FindWithTag("TimeScroll").GetComponent<TimeScroll>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (time_scroll[0].isDone == true) 
        {
            time_scroll[0].isDone = false;
            Food += 100;
        }
        if (time_scroll[1].isDone == true)
        {
            time_scroll[1].isDone = false;
            Food += 1000;
        }
        if (time_scroll[2].isDone == true)
        {
            time_scroll[2].isDone = false;
            Food += 10000;
        }
        if (time_scroll[3].isDone == true)
        {
            time_scroll[3].isDone = false;
            Food += 100000;
        }
        Text_.text = "" + Food;
    }
}
