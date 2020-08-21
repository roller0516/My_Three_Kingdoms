using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine.UI;

public class CWindowInput : CWindow
{
    public Text         m_text;
    public InputField   m_input;
    public Button       m_button;
    public Text         m_button_text;

    public void Show(string _msg, Action<string> _callback)
    {
        base.Show(null, _callback);

        m_text.text = _msg;

        m_button.onClick.AddListener(OnOk);
        //m_button_text.text = CGameTable.Instance.Get_text(1004); //"확인"

    }
    
    public void OnOk()
    {
        base.Close();
        if (callback_func != null) callback_func( m_input.text );
        Destroy(this.gameObject);
    }

}
