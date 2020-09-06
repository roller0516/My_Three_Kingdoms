using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int Damage;
    public float Critical = 0;
    public float CriticalDamage = 0;
    public float AttackSpeed = 1;



    public float GetAttackSpeed()
    {
        return AttackSpeed;
    }
    public int GetDamage()
    {
        return Damage;
    }
    public float GetCritical()
    {
        return Critical;
    }
    public float GetCriticalDamage()
    {
        return CriticalDamage;
    }


}

