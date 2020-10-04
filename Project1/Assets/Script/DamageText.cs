using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector3;

public class DamageText : MonoBehaviour
{
   
    private TextMeshPro text;
    private Color Color_;
    private Transform myTransform;
    private float DestroyTime = 2f;
    private float firingAngle = 45.0f;
    private float gravity = 9.8f;

    public float alphaSpeed;//알파값 
    public BigInteger Damage;
    public Transform Target;
    public Transform Projectile;
    public float VelocityPower;
    
    void Awake()
    {
        myTransform = transform;
    }

    private void Start()
    {
        StartCoroutine("SimulateProjectile");

        text = GetComponent<TextMeshPro>();

        Target = Player.Instance.transform;

        Color_ = text.color;

        Invoke("DestroyText", DestroyTime);
    }
    private void Update()
    {
        text.text = Text_Damage().ToString();
        //transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        Color_.a = Mathf.Lerp(text.color.a, 0, Time.deltaTime * alphaSpeed);
        text.color = Color_;
    }

    private void DestroyText()
    {
        Destroy(this.gameObject);
    }

    IEnumerator SimulateProjectile() // 포물선 공식
    {
        yield return new WaitForSeconds(0.1f);

        // Move projectile to the position of throwing object + add some offset if needed.
        Projectile.position = myTransform.position + new Vector3(0, 0.0f, 0);

        // Calculate distance to target
        float target_Distance = Vector2.Distance(Projectile.position, Target.transform.position);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        //float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(VelocityPower) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(VelocityPower) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

        // Rotate projectile to face the target.
        //Projectile.rotation = Quaternion.LookRotation(Target.transform.position - Projectile.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            Projectile.Translate(Vx * Time.deltaTime, (Vy - (gravity * elapse_time)) * Time.deltaTime, 0);

            elapse_time += Time.deltaTime;

            yield return null;
        }
    }
    private string Text_Damage()
    {
        int placeN = 4;
        BigInteger value = Damage;
        List<int> numlist = new List<int>();
        int p = (int)Mathf.Pow(10, placeN);

        do
        {
            numlist.Add((int)(value % p));
            value /= p;
        }
        while (value >= 1);

        int num = numlist.Count < 2 ? numlist[0] : numlist[numlist.Count - 1] * p + numlist[numlist.Count - 2];



        if (num < 10000)
            return num.ToString();

        float f = (num / (float)p);

        return f.ToString("N2") + GetUnitText(numlist.Count - 1);
    }

    private string GetUnitText(int index)
    {
        int idx = index - 1;
        if (idx < 0) return "";
        int repeatCount = (index / 26) + 1;
        string retstr = "";
        for (int i = 0; i < repeatCount; i++)
        {
            retstr += (char)(65 + index % 26);
        }
        return ((char)65).ToString();
    }


    
}
