using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public int MonsterCount = 1;
    public float curStage = 1;
    public float MaxStage =5;
    public float BossStage = 50;
    public MeshRenderer[] BackGroud = new MeshRenderer[3];
    public int Count;
    private void Start()
    {
        stageCount();
        StageText();
        StageSound_p();
    }
    private void Update()
    {
        
        if (curStage == 1 || curStage % 50 == 1)
        {
            StartCoroutine("ChangeStageRendder");
            StageSound_p();
        }
        else
        {
            Count = 0;
        }
    }
    private void stageCount() 
    {

        if (curStage >= 101)
        {
            for (int i = 0; i < BackGroud.Length; i++)
            {
                BackGroud[i].GetComponent<MeshRenderer>().material = Resources.Load("Material/BackGround03", typeof(Material)) as Material;
            }
        }
        else if (curStage >= 51&& curStage <101)
        {
            for (int i = 0; i < BackGroud.Length; i++)
            {
                BackGroud[i].GetComponent<MeshRenderer>().material = Resources.Load("Material/BackGround02", typeof(Material)) as Material;
            }
        }
        else if (curStage < 51)
        {
            for (int i = 0; i < BackGroud.Length; i++)
            {
                BackGroud[i].GetComponent<MeshRenderer>().material = Resources.Load("Material/BackGround01", typeof(Material)) as Material;
            }
        }
    }
    public void StageSound_p() 
    {
        if (curStage >= 101)
            stageSound(2);
        else if (curStage >= 51 && curStage < 101)
            stageSound(1);
        else if (curStage < 51)
            stageSound(0);
    }
    public void stageSound(int num)
    {
        if (Count == 0)
        {
            SoundManager.instance.BgmSource.Stop();
            SoundManager.instance.BgmSound(num);
            SoundManager.instance.BgmSource.Play();
            print("11111"+Count);
        }
        Count = 1; 
    }
    public void StageText()
    {
        text.text = "제" + curStage.ToString() + "장";
    }
    IEnumerator ChangeStageRendder() 
    {
        yield return new WaitForSeconds(1.0f);
        stageCount();
    }
}
