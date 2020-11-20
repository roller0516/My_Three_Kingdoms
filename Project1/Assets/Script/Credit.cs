using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credit : MonoBehaviour
{
    public GameObject creidt;
    public void CreaditOpen() 
    {
        creidt.SetActive(true);
    }
    public void CreaditClose()
    {
        creidt.SetActive(false);
    }
}
