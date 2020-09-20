using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BMIcheck : MonoBehaviour
{
    public float BMI; // 신량질량지수
    public float KG; // 몸무게
    public float m;//신장 
    public int Year;//년도
    public int age;//나이
    public string name;//이름
    public string text1;
    public string text2;
    
    
    void Update()
    {
        //BMI
        BMI =  KG/(m * m);
        
        age = 2020 - (Year - 1);//현재나이

        text1 = "안녕하세요 제이름은 " + name + " 입니다";
        text2 = "저는" + Year + "년도 출생으로 현재" + age + "살 입니다";
    }
}
