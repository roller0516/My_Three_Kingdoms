using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTwoCheck : MonoBehaviour
{
    //도 개 걸 윷 모

    //도: 1 칸 개: 2칸 걸: 3칸 윷 :4칸 모 :5칸

    //도 1개가 뒤집어 졌을때, 개 2개가 뒤집어 졌을때,
    //걸 3개가 뒤집어 졌을때, 윷 4개가 뒤집어 졌을때,
    //모 0개가 뒤접어 졌을때

    // 뒤집어 졌다 안뒤집어 졌다.

    //총 4개의 윷이 있다.

    public int x = 4; //윷의갯수
    public float percentage;
    

    private void Update()
    {
        x = Random.Range(0, 5);
        percentage = 16 / x;

        if ( ) //모일때
        {
            Debug.Log("모 입니다");
        }
        else if (x == 1)
        {
            Debug.Log("도 입니다");
        }
        else if (x == 2)
        {
            Debug.Log("개 입니다");
        }
        else if (x == 3)
        {
            Debug.Log("걸 입니다");
        }
        else if (x == 4)
        {
            Debug.Log("윷 입니다");
        }
    }
}
