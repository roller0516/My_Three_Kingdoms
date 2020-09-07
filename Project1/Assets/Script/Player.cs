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
    private SkeletonRenderer skeletonRenderer;
    private Rigidbody2D rig;

    public PlayerData Playerdata;
    public GameObject Monster;
    public AnimState _AniState;
    public float moveSpeed = 2;

    private void Awake() => rig = GetComponent<Rigidbody2D>();

    private void Start()
    {
        //컴포넌트
        ani = GetComponent<Animator>();
        Playerdata = GetComponent<PlayerData>();
        print(Playerdata);
        //애니메이션
        _AniState = AnimState.move;
    }

    private void Update()
    {
        transform.Translate(new Vector2(1f * moveSpeed * Time.deltaTime, 0)); //플레이어 이동
        SetCurrentAnimation(_AniState);
    }
    //-------------------------------------------------------Player움직임----------------------------------------------------------

     

    //-------------------------------------------------------Player물리------------------------------------------------------------
    private void FixedUpdate()
    {
       
    }


    //---------------------------------------------Player Ani----------------------------------------------------------------------
    //private void _AsncAnimation(AnimationReferenceAsset aniClip, bool loop, float timeScale)//해당애니메이션으로 변경한다
    //{
    //    if (aniClip.name.Equals(CurrentAnimation))//같은 애니메이션을 재생하려고 한다면 아래코드를 실행하지 않는다.
    //        return;
    //    skeletonAnimation.state.SetAnimation(0, aniClip, loop).TimeScale = timeScale;
    //    skeletonAnimation.loop = loop;
    //    skeletonAnimation.timeScale = timeScale;

    //    CurrentAnimation = aniClip.name;//현재 재생되고 있는 애니메이션 이름으로 변경
    //}
    private void SetCurrentAnimation(AnimState _state)
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
    //----------------------------------------------------------------플레이어 모션
    public void Attack() //공격 
    {
        Monster.GetComponent<EnemyTest>().TakeDamage(Playerdata.Damage);
        //Monster.GetComponent<EnemyTest>().TakeDamage(1);

        //Collider2D[] collier2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0); // 충돌처리
        //foreach (Collider2D collider in collier2Ds)
        //{
        //    if (collider.tag == "Monster")
        //    {
        //        collider.GetComponent<EnemyTest>().TakeDamage(1);
                
        //        if (collider.GetComponent<EnemyTest>().Hp <= 0)
        //        {
        //            isAttack = false;
        //            moveSpeed = 1;
        //            _AniState = AnimState.move;
                   
        //        }
                
        //    }
        //}
    }
   

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawCube(pos.position, boxSize);
    //}
}
