using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    //public Slider MonsterCountSlider;
    public float MonsterCount = 1;
    public float curStage = 1;
    public float MaxStage =10;

    private void Update()
    {
        stageCount();
    }
 
    private void stageCount() 
    {
      

        text.text = curStage.ToString() + "stage";
    }
}
