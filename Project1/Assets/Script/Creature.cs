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
        if (MonsterSpawn.instance.stg.MonsterCount % MonsterSpawn.instance.stg.MaxStage == 0)
            Monster = FindObjectOfType<Boss>().gameObject;
        else
            Monster = FindObjectOfType<EnemyTest>().gameObject;
        
        ani = GetComponent<Animator>();
    }
    private void Update()
    {
        //SetCurrentAnimation(_AniState);
    }
    // Update is called once per frame
    //private void SetCurrentAnimation(AnimState _state) // 플레이어 애니메이션 
    //{
    //    switch (_state)
    //    {
    //        case AnimState.Move:
    //            ani.SetInteger("AniState", (int)AnimState.Move);
    //            ani.SetFloat("MoveSpeed", 1.0f);
    //            break;
    //        case AnimState.Attack:
    //            ani.SetInteger("AniState", (int)AnimState.Attack);
    //            break;
    //    }
    //}
    public void Attack() //공격 
    {
        if (Monster.tag == "Boss")
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
        if (hit <= Maxhitcount)
        {
            Monster.GetComponent<MimicEnemy>().TakeDamage(Player.Instance.my_PlayerDamage);
            yield return new WaitForSeconds(0.1f);
            hit++;
            StartCoroutine("Mimichitcount");
        }
        else
            hit = 0;
    }
    IEnumerator Monsterhitcount()
    {
        if (hit <= Maxhitcount)
        {
            Monster.GetComponent<EnemyTest>().TakeDamage(Player.Instance.my_PlayerDamage);
            yield return new WaitForSeconds(0.1f);
            hit++;
            StartCoroutine("Monsterhitcount");
        }
        else
            hit = 0;
    }
    IEnumerator Bosshitcount()
    {
        if (hit <= Maxhitcount)
        {
            Monster.GetComponent<Boss>().TakeDamage(Player.Instance.my_PlayerDamage);
            yield return new WaitForSeconds(0.1f);
            hit++;
            StartCoroutine("Bosshitcount");
        }
        else
            hit = 0;
    }
}
