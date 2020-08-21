using UnityEngine;
using System.Collections;
using System;

public enum WINDOW_TYPE
{
    None = -1,
    Main,
    Modal
}

public class CWindow : MonoBehaviour {

    WINDOW_TYPE m_Type = WINDOW_TYPE.None;

    GameObject  m_Root;             // 부모객체.

    public Action<string> callback_func = null;

    bool        m_Active = false;   // 활성여부.
        
    //void Start()
    //{
    //}    
    //void Update()
    //{
    //}

    public virtual void Show(GameObject _root, Action<string> _callback)
    {
        m_Root = _root;
        callback_func = _callback;

        //CGameSnd.instance.PlaySound(eSound.popupon);
        m_Active = true;        
    }
    
    public virtual void Close()
    {
        //CGameSnd.instance.PlaySound(eSound.popupoff);
        m_Active = false;
    }
    
    public bool IsActive()
    {
        return m_Active;
    }

}

/*
    // 상속함수. 윈도우 메인 컴포넌트.
    public class ClassName : CWindow
    {
        public override void Show(GameObject _root, Action<string> _callback, string _param = "")
        {
            base.Show(_root, _callback, _param);
        }
    }

    //사용 법.
    System.Action<string> callback = delegate (string rt)
    {
        //Debug.Log(rt);
    };
    CGame.Instance.Window_show("Prefab/Terms", callback);

    //사용 법.
    CGame.Instance.Window_show(CGame.Instance.GetText(00000), rt => 
    {
        if (rt == "0") PlayEnd("socket_close");
    });

    // 범용함수.
    public void Window_show(string _prefab, GameObject _root, Action<string> _callback, string _param = "")
    {
        //GameObject Root_ui = GameObject.Find("UI Root (3D)"); //ngui
        GameObject go = GameObject.Instantiate(Resources.Load(_prefab), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        go.transform.parent = Root_ui.transform;
        go.transform.SetAsLastSibling();
        go.transform.localPosition = new Vector3(0, 0, 0);
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = new Vector3(1, 1, 1);
        

        CWindow w = go.GetComponent<CWindow>();
        w.Show(_root, _callback, _param);
    }

    // 윈도우 팝업 ---------------------------------------------------------------------------------------
    //CGame.Instance.Window_notice("213123 213123 ", rt => { if (rt == "0") print("notice");  });
    public void Window_notice(string _msg, Action<string> _callback)
    {
        //GameObject Root_ui = GameObject.Find("root_window)"); //ui attach
        GameObject go = GameObject.Instantiate(Resources.Load("prefabs/Window_notice"), Vector3.zero, Quaternion.identity) as GameObject;
        go.transform.parent = Root_ui.transform;
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;

        CWindowNotice w = go.GetComponent<CWindowNotice>();
        w.Show(_msg, _callback);
    }

    public void Window_yesno(string _title, string _msg, Action<string> _callback)
    {
        //GameObject Root_ui = GameObject.Find("root_window)"); //ui attach
        GameObject go = GameObject.Instantiate(Resources.Load("prefabs/Window_yesno"), Vector3.zero, Quaternion.identity) as GameObject;
        go.transform.parent = Root_ui.transform;
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;

        CWindowYesNo w = go.GetComponent<CWindowYesNo>();
        w.Show(_title, _msg, _callback);
    }
*/
