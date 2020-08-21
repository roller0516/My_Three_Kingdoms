using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine.UI;

//using ExitGames.Client;
//using ExitGames.Client.Photon.LoadBalancing;
//using Hashtable = ExitGames.Client.Photon.Hashtable;

public class CWindowNickname : CWindow
{
    public Text         m_text;
    public InputField   m_input;
    public Button       m_button;
    public Text         m_button_text;

    string m_name = "";

    public void Show(string _msg, Action<string> _callback)
    {
        base.Show(null, _callback);

        m_text.text = _msg;

        m_button.onClick.AddListener(OnOk);

    }
    
    public void OnOk()
    {
        m_name = m_input.text;
        //MyServer.Instance.Rq_PlayerNameCheck(m_name, Callback_Rq_PlayerNameCheck);

    }

    public void Callback_Rq_PlayerNameCheck(Hashtable _data)
    {
        string _rt = (string)_data[0]; print("Callback_Rq_PlayerNameCheck : " + _rt);
        if (_rt == "ok")
        {
            base.Close();

            if (callback_func != null) callback_func(m_name);

            Destroy(this.gameObject);
        }
        else {
            m_text.text = CGameTable.Instance.Get_text(2053); //"사용할 수 없습니다. 다시 입력해주세요."
        }
    }

}
