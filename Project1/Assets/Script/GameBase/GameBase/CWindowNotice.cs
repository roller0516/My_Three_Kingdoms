using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine.UI;

public class CWindowNotice : CWindow
{
    public Text m_text;

    public void Show(string _msg, Action<string> _callback)
    {
        base.Show(null, _callback);

        m_text.text = _msg;

        StartCoroutine(CloseAfter());
    }

    IEnumerator CloseAfter()
    {
        //yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(3.0f);

        if (callback_func != null)  
            callback_func("0");
        Destroy(this.gameObject);
    }
}
