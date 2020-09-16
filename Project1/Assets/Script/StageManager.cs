using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class StageManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public int curStage = 1;
    public int MaxStage =10;

    private void Update()
    {
        stageCount();
    }
    private void stageCount() 
    {
        text.text = curStage.ToString() + "/" + MaxStage.ToString();
    }
}
