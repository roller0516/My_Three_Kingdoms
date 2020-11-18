using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using UnityEngine.UI;

public class numTest : MonoBehaviour
{
    BigInteger mgold = 1234556677777777777;

    public Text txt;
    private void Start()
    {
        Getnum();
    }
    private void Update()
    {
        txt.text = mgold.ToString();
    }
    public void AddGold(int Gold)
    {
        mgold *= 1000;
        Setnum();
    }

    public  void Setnum()
    {
        PlayerPrefs.SetString("numtest", mgold.ToString());
        print(mgold.ToString());
    }
    public void Getnum()
    {
        string key;
        key = mgold.ToString();
        key = PlayerPrefs.GetString("numtest",key);
        mgold = BigInteger.Parse(key);
    }
}
