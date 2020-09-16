using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Player : MonoBehaviour
{
    public enum AnimState
    {
        Idle, move, Attack, moveSpeedup
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
    private ItemList itemlist;

    public SkeletonRenderer skeletonRenderer;
   
   
    public PlayerData Playerdata = new PlayerData();
    public GameObject Monster;
    public AnimState _AniState;
    public float moveSpeed = 2;

    public int my_PlayerDamage;


    private void Start()
    {
        
        
        //컴포넌트
        ani = GetComponent<Animator>();
        skeletonRenderer = GetComponent<SkeletonRenderer>();
        itemlist = FindObjectOfType<ItemList>();
        _AniState = AnimState.move;

        
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
            case AnimState.moveSpeedup:
                ani.SetFloat("MoveSpeed",2.0f);
                break;
        }
    }

    public void Attack() //공격 
    {

        if (Monster.tag == "Boss")
            Monster.GetComponent<Boss>().TakeDamage(my_PlayerDamage);
        else 
        {
            Monster.GetComponent<EnemyTest>().TakeDamage(my_PlayerDamage);
        }
    }
}
