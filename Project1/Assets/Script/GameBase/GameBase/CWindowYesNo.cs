using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine.UI;

public class CWindowYesNo : CWindow
{
    public Text m_title_text;
    public Text m_msg_text;
    public Button   m_button_yes;
    public Text     m_button_yes_text;
    public Button   m_button_no;    
    public Text     m_button_no_text;


    public void Show(string _title, string _msg, Action<string> _callback)
    {
        base.Show(null, _callback);

        m_title_text.text = _title;
        m_msg_text.text = _msg;

        m_button_yes.onClick.AddListener(OnYes);
        m_button_no.onClick.AddListener(OnNo);

        m_button_yes_text.text = CGameTable.Instance.Get_text(10002); //yes
        m_button_no_text.text = CGameTable.Instance.Get_text(10003); //no

    }
    //public override void Show(GameObject _root, Action<string> _callback)
    //{
    //    base.Show(_root, _callback);
    //}

    public void OnYes()
    {
        base.Close();

        if (callback_func != null)
            callback_func("yes");

        Destroy(this.gameObject);
    }
    public void OnNo()
    {
        base.Close();

        if (callback_func != null)
            callback_func("no");

        Destroy(this.gameObject);
    }
}
