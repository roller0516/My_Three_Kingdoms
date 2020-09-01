using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Player : MonoBehaviour
{
    public enum AnimState
    {
        Idle, move, Attack
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
    private float x;

    public bool isAttack = false;
    public bool monsterhit = false;
    public Transform pos;
    public Vector2 boxSize;
    public float moveSpeed = 2;
    public GameObject Monster;
    
    public AnimState _AniState;
    

    private void Awake() => rig = GetComponent<Rigidbody2D>();

    private void Start()
    {
        ani = GetComponent<Animator>();
        _AniState = AnimState.move;
    }

    private void Update()
    {
        transform.Translate(new Vector2(1f * moveSpeed * Time.deltaTime, 0)); //플레이어 이동
        SetCurrentAnimation(_AniState);

        //if(isAttack == true)
        //{
        //    Curtime += Time.deltaTime;
        //    if (Curtime >= coolTime)
        //    {
        //        monsterhit = true;
        //        Attack();
        //    }
        //}
        
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
                break;
            case AnimState.Attack:
                ani.SetInteger("AniState", (int)AnimState.Attack);
                break;
        }
    }
    //----------------------------------------------------------------플레이어 모션
    public void Attack() //공격 
    {
        //Curtime = 0;
        
        Monster.GetComponent<EnemyTest>().TakeDamage(1);
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
   
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(pos.position, boxSize);
    }
}
