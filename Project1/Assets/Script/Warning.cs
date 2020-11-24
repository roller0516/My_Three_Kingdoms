using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Warning : MonoBehaviour
{
    public Image Panel;
    float time = 0f;
    public void Fade()
    {
        StartCoroutine(FadeFlow());
    }
    IEnumerator FadeFlow()
    {
        Panel.gameObject.SetActive(true);
        time = 0f;
        Color AlPha = Panel.color;
        while (AlPha.a < 0.5f)
        {
            time += Time.deltaTime * 1f;
            AlPha.a = Mathf.Lerp(0, 1, time);
            Panel.color = AlPha;
            yield return null;
        }
        time = 0f;
        while (AlPha.a > 0f)
        {
            time += Time.deltaTime * 1f;
            AlPha.a = Mathf.Lerp(1, 0, time);
            Panel.color = AlPha;
            yield return null;
        }
        while (AlPha.a < 0.5f)
        {
            time += Time.deltaTime * 1f;
            AlPha.a = Mathf.Lerp(0, 1, time);
            Panel.color = AlPha;
            yield return null;
        }
        time = 0f;
        while (AlPha.a > 0f)
        {
            time += Time.deltaTime * 1f;
            AlPha.a = Mathf.Lerp(1, 0, time);
            Panel.color = AlPha;
            yield return null;
        }
       
        Panel.gameObject.SetActive(false);
        yield return null;
    }
}
