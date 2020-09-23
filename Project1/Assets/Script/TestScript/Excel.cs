using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Excel : MonoBehaviour
{
    public weaponData wepondata;
    public TextMeshProUGUI text;
    private void Start()
    {
        text.text = wepondata.dataArray[1].Atk.ToString();
    }
        
}
