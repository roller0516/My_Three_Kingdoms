using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text GoldDisPlayer;
   

    private void Update()
    {
        GoldDisPlayer.text = "" + DataController.GetInstance().GetGold();
    }
}
