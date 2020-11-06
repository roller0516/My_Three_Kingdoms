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
    public MeshRenderer[] BackGroud = new MeshRenderer[3];

    private void Start()
    {
        stageSound();

    }
    private void Update()
    {
        stageCount();
    }
    private void stageCount() 
    {
        text.text = "제"+curStage.ToString() + "장";
        if (curStage >= 100)
        {
           
            for (int i = 0; i < BackGroud.Length; i++)
            {
                BackGroud[i].GetComponent<MeshRenderer>().material = Resources.Load("Material/BackGround03", typeof(Material)) as Material;
            }
        }
        else if (curStage >= 50)
        {
            
            for (int i = 0; i < BackGroud.Length; i++)
            {
                BackGroud[i].GetComponent<MeshRenderer>().material = Resources.Load("Material/BackGround02", typeof(Material)) as Material;
            }
        }
        else if (curStage < 50)
        {
            
            for (int i = 0; i < BackGroud.Length; i++)
            {
                BackGroud[i].GetComponent<MeshRenderer>().material = Resources.Load("Material/BackGround01", typeof(Material)) as Material;
            }
        }
    }
    public void stageSound() 
    {
        if (curStage >= 100)
        {
            SoundManager.instance.BgmSource.Stop();
            SoundManager.instance.BgmSound(2);
            SoundManager.instance.BgmSource.Play();
        }
        else if (curStage >= 50)
        {
            SoundManager.instance.BgmSource.Stop();
            SoundManager.instance.BgmSound(1);
            SoundManager.instance.BgmSource.Play();
        }
        else if (curStage< 50) 
        {
            SoundManager.instance.BgmSource.Stop();
            SoundManager.instance.BgmSound(0);
            SoundManager.instance.BgmSource.Play();
        }
    }
}
