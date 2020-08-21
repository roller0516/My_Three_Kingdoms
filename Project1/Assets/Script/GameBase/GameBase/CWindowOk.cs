using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine.UI;

public class CWindowOk : CWindow
{
    public Text     m_text;
    public Button   m_button_ok;
    public Text     m_button_ok_text;

    public void Show(string _msg, Action<string> _callback)
    {
        //yield return new WaitForEndOfFrame();

        base.Show(null, _callback);

        m_text.text = _msg;
        m_button_ok.onClick.AddListener(OnOk);

        string ss = CGameTable.Instance.Get_text(10001); //ok
        if (ss != "") m_button_ok_text.text = ss;
    }

    public void OnOk()
    {
        base.Close();
        if (callback_func != null) callback_func("0");
        Destroy(this.gameObject);
    }

    public void OnClose()
    {
        base.Close();
        if (callback_func != null) callback_func("1");
        Destroy(this.gameObject);
    }
}

/*
    //CGame.Instance.Window_notice("213123 213123 ", rt => { if (rt == "0") print("notice");  });
    public void Window_notice(string _msq, Action<string> _callback )
    {
        //GameObject Root_ui = GameObject.Find("root_window)"); //ui attach
        GameObject go = GameObject.Instantiate(Resources.Load("prefab/Window_notice"), Vector3.zero, Quaternion.identity) as GameObject;        
        go.transform.parent = Root_ui.transform;
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;

        CWindowNotice w = go.GetComponent<CWindowNotice>();
        w.Show(_msq, _callback);
    }
*/
