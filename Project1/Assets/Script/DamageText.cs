using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    public float speed; // 텍스트 이동속도
    public float alphaSpeed;//알파값 
    public int Damage;
    private float DestroyTime = 2f;
    TextMeshPro text;
    Color Color_;
    
    
    private void Start()
    {
        text = GetComponent<TextMeshPro>();
        Color_ = text.color;
        text.text = Damage.ToString();
        Invoke("DestroyText", DestroyTime);
        
    }
    private void Update()
    {
        transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        Color_.a = Mathf.Lerp(text.color.a, 0, Time.deltaTime * alphaSpeed);
        text.color = Color_;
    }

    private void DestroyText()
    {
        Destroy(this.gameObject);
    }
}
