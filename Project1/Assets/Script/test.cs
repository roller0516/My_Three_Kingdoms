using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public int front; // 앞면
    public int Back; //뒷면
    public bool Play = false;
    private void Update() //void 비어있다 어떠한 자료형이와도 받을수있다.
    {
        //윷 4개야
        if (Play == true )
        {
            int temp = Random.Range(0, 5);

            Back = temp;

            if (Back == 0)//0개가 뒷면 front = 4 모
            {
                Debug.Log("모입니다");
            }
            else if (Back == 1)//1개가 뒷면 도 
            {
                Debug.Log("도입니다");
            }
            else if (Back == 2)//1개가 뒷면 개
            {
                Debug.Log("개입니다");
            }
            else if (Back == 3)//1개가 뒷면 걸
            {
                Debug.Log("걸입니다");
            }
            else if (Back == 4)//1개가 뒷면 윷
            {
                Debug.Log("윷입니다");
            }
        }
        
    }
}
