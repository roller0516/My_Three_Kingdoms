using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;

public class Player : MonoBehaviour
{
    public enum AnimState
    {
        Idle, move, Attack, moveSpeedup,Attack2,Attack3 , Groggy
    }

    private static Player s_instance = null;

    public static Player Instance
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = FindObjectOfType(typeof(Player)) as Player;
            }
            return s_instance;
        }
    }
    private Animator ani;
    public ItemList itemlist;
    public Vector3 startPosition;
    public SkeletonRenderer skeletonRenderer;
    public SkeletonMecanim skeletonAni;
    public PlayerData Playerdata = new PlayerData();
    public GameObject Monster;
    public AnimState _AniState;
    public float moveSpeed = 2;
    public BigInteger my_PlayerDamage;
    public int CriticalPer; //크피
    public int Critical;//크리확률
    int Count = 0;
    int crt;
    private void Awake()
    {
        skeletonAni = GetComponent<SkeletonMecanim>();
        
        
    }
    private void Start()
    {
        //컴포넌트
        ani = GetComponent<Animator>();
        skeletonRenderer = GetComponent<SkeletonRenderer>();
        itemlist = FindObjectOfType<ItemList>();
        _AniState = AnimState.move;
        startPosition = this.transform.position;
        DataController.GetInstance().LoadPlayer(this);
    }

    private void Update()
    {
        transform.Translate(new Vector2(1f * moveSpeed * Time.deltaTime, 0));//플레이어 이동
        my_PlayerDamage = Playerdata.Damage + itemlist.item_Attack;

        SetCurrentAnimation(_AniState);
    }
    
    private void SetCurrentAnimation(AnimState _state) // 플레이어 애니메이션 
    {
        switch (_state)
        {
            case AnimState.Idle: 
                ani.SetInteger("AniState", (int)AnimState.Idle);
                break;
            case AnimState.move:
                ani.SetInteger("AniState", (int)AnimState.move);
                ani.SetFloat("MoveSpeed", 1.0f);
                break;
            case AnimState.Attack:
                ani.SetInteger("AniState", (int)AnimState.Attack);
                ani.SetFloat("AttackSpeed", Playerdata.AttackSpeed);
                break;
            case AnimState.Attack2:
                ani.SetInteger("AniState", (int)AnimState.Attack2);
                ani.SetFloat("AttackSpeed", Playerdata.AttackSpeed);
                break;
            case AnimState.Attack3:
                ani.SetInteger("AniState", (int)AnimState.Attack3);
                ani.SetFloat("AttackSpeed", Playerdata.AttackSpeed);
                break;
            case AnimState.moveSpeedup:
                ani.SetFloat("MoveSpeed",2.0f);
                break;
            case AnimState.Groggy:
                ani.SetInteger("AniState", (int)AnimState.Groggy);
                break;
        }
    }
  
    public void Attack() //공격 
    {
        crt = Random.Range(1, 101);
        
        if (crt <= Critical)
        {
            if (Monster == null || Monster.activeSelf == false)
            {
                print("Bug");
                return;
            }
            else if (Monster.tag == "Monster") 
            {
                AttackSound(4);
                Monster.GetComponent<EnemyTest>().CriticalDamage(BigInteger.Add(my_PlayerDamage, (BigInteger.Multiply(my_PlayerDamage, (CriticalPer / 100)))));
            }
            else if (Monster.tag == "Boss")
            {
                AttackSound(4);
                Monster.GetComponent<Boss>().CriticalDamage(BigInteger.Add(my_PlayerDamage, (BigInteger.Multiply(my_PlayerDamage, (CriticalPer / 100)))));
            }
            else if (Monster.tag == "Monster1")
            {
                AttackSound(4);
                Monster.GetComponent<MimicEnemy>().CriticalDamage(BigInteger.Add(my_PlayerDamage, (BigInteger.Multiply(my_PlayerDamage, (CriticalPer / 100)))));
            }
        }
        else
        {
            if (Monster == null || Monster.activeSelf == false)
            {
                print("Bug");
                return;
            }
            else if (Monster.tag == "Monster")
            {
                AttackSound(Monster.GetComponent<EnemyTest>().HitCount + 1);
                Monster.GetComponent<EnemyTest>().TakeDamage(my_PlayerDamage);
            }
            else if (Monster.tag == "Boss")
            {
                AttackSound(Monster.GetComponent<Boss>().hitCount + 1);
                Monster.GetComponent<Boss>().TakeDamage(my_PlayerDamage);
            }
            else if (Monster.tag == "Monster1")
            {
                AttackSound(Monster.GetComponent<MimicEnemy>().HitCount + 1);
                Monster.GetComponent<MimicEnemy>().TakeDamage(my_PlayerDamage);
            }
        }
    }
    public void AttackSound(int num) 
    {
        SoundManager.instance.WeaponSound(num);
    }
}
