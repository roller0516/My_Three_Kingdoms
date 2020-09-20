using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class StageManager : MonoBehaviour
{
    public TextMeshProUGUI[] text;
    public int MonsterCount = 1;
    public int curStage = 1;
    public int MaxStage =10;

    private void Update()
    {
        stageCount();
    }
    private void stageCount() 
    {
        if(MonsterCount%10 >0)
            text[0].text = (MonsterCount %10).ToString() + "/" + MaxStage.ToString();
        else if (MonsterCount%MaxStage == 0)
            text[0].text = (MonsterCount/curStage).ToString() + "/" + MaxStage.ToString();
        text[1].text = curStage.ToString() + "stage";
    }
}
