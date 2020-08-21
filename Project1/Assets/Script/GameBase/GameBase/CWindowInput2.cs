using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine.UI;

public class CWindowInput2 : CWindow
{
    public Text         m_text;
    public InputField   m_input1;
    public InputField   m_input2;
    public Button       m_button_ok;
    public Button       m_button_close;
    public Text         m_button_text;

    public void Show(string _msg, Action<string> _callback)
    {
        base.Show(null, _callback);

        m_text.text = _msg;

        m_button_ok.onClick.AddListener(OnOk);
        m_button_close.onClick.AddListener(OnClose);

    }
    
    public void OnOk()
    {
        base.Close();
        if (callback_func != null) callback_func( m_input1.text + ":" + m_input2.text);
        Destroy(this.gameObject);
    }
    public void OnClose()
    {
        base.Close();
        if (callback_func != null) callback_func("close");
        Destroy(this.gameObject);
    }

}
