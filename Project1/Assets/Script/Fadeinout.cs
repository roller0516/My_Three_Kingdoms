using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fadeinout : MonoBehaviour
{
    public GameObject SearchRewardPanel;
    public Image Panel;
    float time = 0f;
    float f_time = 1f;
    int TouchCount;
    public void Fade()
    {
        StartCoroutine(FadeFlow());
    }
    IEnumerator FadeFlow()
    {
        Panel.gameObject.SetActive(true);
        time = 0f;
        Color AlPha = Panel.color;
        while (AlPha.a < 1f)
        {
            time += Time.deltaTime * 5f;
            AlPha.a = Mathf.Lerp(0,1, time);
            Panel.color = AlPha;
            yield return null;
        }
        time = 0f;
        yield return new WaitForSeconds(0.8f);
        while (AlPha.a >0f)
        {
            time += Time.deltaTime * 5f;
            AlPha.a = Mathf.Lerp(1, 0, time);
            Panel.color = AlPha;
            yield return null;
        }
        Panel.gameObject.SetActive(false);
        yield return null;
    }
    public void SearchReward()
    {
        SearchRewardPanel.SetActive(true);
        
    }
    private void Update()
    {
        if (SearchRewardPanel.activeSelf == true)
        {

            if (Input.GetMouseButtonDown(0))
            {
                TouchCount++;
                if (TouchCount == 1)
                {
                    //애니메이션 실행 
                }
                else if (TouchCount >= 2)
                {
                    SearchRewardPanel.SetActive(false);
                    TouchCount = 0;
                }

            }
                
        }
            

    }
}
