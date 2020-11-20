using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class instructionButton : MonoBehaviour
{
    public GameObject[] instruction;
    public Toggle[] instructionToggle;

    private void Update()
    {
        instructionCheck();
    }
    public void instructionCheck() 
    {
        for (int i = 0; i < instructionToggle.Length;i++) 
        {
            instruction[i].SetActive(instructionToggle[i].isOn);
        }
    }
}
