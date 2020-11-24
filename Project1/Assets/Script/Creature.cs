using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using System.Numerics;

public class Creature : MonoBehaviour
{
    private static Creature s_instance = null;

    public static Creature Instance
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = FindObjectOfType(typeof(Creature)) as Creature;
            }
            return s_instance;
        }
    }
    public enum AnimState //몬스터 상태
    {
        Move, Attack
    }
    private AnimState _AniState;
    private Animator ani;
    private Transform target;
    public GameObject Monster;
    public float moveSpeed = 0.5f;
    public int Maxhitcount;
    int hit = 0;
    BigInteger AttackDamage;    
    // Start is called before the first frame update
    void Start()
    {
        Monster = Player.Instance.Monster;
        SoundManager.instance.creturesound();
        ani = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Monster == null || Monster.activeSelf == false)
            Destroy(this.gameObject, 1);
            
    }
    public void Attack() //공격 
    {
        if (Monster == null || Monster.activeSelf == false)
        {
            Destroy(this.gameObject, 1);
            return;
        }
        else if (Monster.tag == "Boss")
        {
            StartCoroutine("Bosshitcount");
        }
        else if (Monster.tag == "Monster")
        {
            StartCoroutine("Monsterhitcount");
        }
        else if (Monster.tag == "Monster1")
        {
            StartCoroutine("Mimichitcount");
        }

    }
    IEnumerator Mimichitcount() 
    {
        if (Monster == null || Monster.activeSelf == false)
        {
            SoundManager.instance.creturesound();
            Destroy(this.gameObject, 1);
            yield break;
        }
        else
        {
            if (hit <= Maxhitcount)
            {
                Monster.GetComponent<MimicEnemy>().CreatureDamage(Player.Instance.my_PlayerDamage);
                yield return new WaitForSeconds(0.1f);
                hit++;
                StartCoroutine("Mimichitcount");
            }
            else
            {
                SoundManager.instance.creturesound();
                Destroy(this.gameObject, 1);
                hit = 0;
            }
        }
    }
    IEnumerator Monsterhitcount()
    {
        if (Monster == null || Monster.activeSelf == false)
        {
            SoundManager.instance.creturesound();
            Destroy(this.gameObject, 1);
            yield break;
        }
        else
        {
            if (hit <= Maxhitcount)
            {
                Monster.GetComponent<EnemyTest>().CreatureDamage(Player.Instance.my_PlayerDamage);
                yield return new WaitForSeconds(0.1f);
                hit++;
                StartCoroutine("Monsterhitcount");
            }
            else
            {
                SoundManager.instance.creturesound();
                Destroy(this.gameObject, 1);
                hit = 0;
            }
        }
        
           
    }
    IEnumerator Bosshitcount()
    {
        if (Monster == null || Monster.activeSelf == false)
        {
            SoundManager.instance.creturesound();
            Destroy(this.gameObject, 1);
            yield break;
        }
        else
        {
            if (hit <= Maxhitcount)
            {
                Monster.GetComponent<Boss>().CreatureDamage(Player.Instance.my_PlayerDamage);
                yield return new WaitForSeconds(0.1f);
                hit++;
                StartCoroutine("Bosshitcount");
            }
            else
            {
                SoundManager.instance.creturesound();
                Destroy(this.gameObject, 1);
                hit = 0;
            }
        }

    }
}
