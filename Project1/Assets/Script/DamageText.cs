using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    private TextMeshPro text;
    private Color Color_;
    private Transform myTransform;
    private float DestroyTime = 2f;
    private float firingAngle = 45.0f;
    private float gravity = 9.8f;

    public float alphaSpeed;//알파값 
    public int Damage;
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
        text.text = Damage.ToString();
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
}
