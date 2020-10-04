using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTwoCheck : MonoBehaviour
{
    string[] subject = { "국어", "영어", "수학", "사회", "과학","음악","체육"};
    int[] Score = { 88, 72, 59, 99, 74, 66, 80 };
    //컴퓨터는 0부터 시작 0번째 배열 특징 -> 순차적으로
    //평균을 구하려면 합계부터 구해야한다.
    //상자가 7개 7개 88 72 59 99 74 66 80개의 사과가 각각 상자에 들어가있다고 생각하면된다.
    private void Start()
    {
        float sum = 0;//합계

        for (int i = 0; i< Score.Length; i++)
        {
            sum = sum + Score[i];
        }
        sum = sum / subject.Length;
      
        // 평균
        Debug.Log("평균값은"+sum+"점입니다.");

    }
}
