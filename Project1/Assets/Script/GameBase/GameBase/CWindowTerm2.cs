using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using UnityEngine.UI;

public class CWindowTerm2 : CWindow
{
    public Toggle m_toggle_1;
    public Toggle m_toggle_2;

    bool toggle_1_on = false;      
    bool toggle_2_on = false;      

    public Text toggle_1_t;     //Privacy policy //개인정보취급방침
    public Text toggle_2_t;     //Terms of service //서비스이용약관
    public Button toggle_1_b;
    public Button toggle_2_b;

    // Use this for initialization
    void Start ()
    {
        m_toggle_1.onValueChanged.AddListener((on) => { Toggle_1(on); });
        m_toggle_2.onValueChanged.AddListener((on) => { Toggle_2(on); });
        
        toggle_1_t.text = CGameTable.Instance.Get_text(1216);
        toggle_2_t.text = CGameTable.Instance.Get_text(1217);
    }
	
	// Update is called once per frame
	//void Update () {		
	//}

    public void Show(string _msg, Action<string> _callback)
    {
        base.Show(null, _callback);

        //m_text.text = _msg;
        //m_button.onClick.AddListener(OnOk);

    }

    public void Toggle_1(bool isToggle)
    {
        //if (isToggle) print("Toggle_1");
        toggle_1_on = isToggle;

        //if (toggle_1_on && toggle_2_on) Close();
    }
    public void Toggle_2(bool isToggle)
    {
        toggle_2_on = isToggle;

        //if (toggle_1_on && toggle_2_on) Close();
    }

    public override void Close()
    {
        //base.Close();
        if (callback_func != null) callback_func("ok");
        Destroy(this.gameObject);
    }

    public void OnClick_btn_1()
    {
        Application.OpenURL("http://nadiasoft.com/?page_id=473"); //개인정보처리방침
    }

    public void OnClick_btn_2()
    {
        Application.OpenURL("http://nadiasoft.com/?page_id=470"); //이용약관
    }

    public void OnClick_ok()
    {
        if (toggle_1_on && toggle_2_on)
            Close();
    }

}
