using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class EnemyTest : MonoBehaviour
{
    public enum AnimState
    {
        Idle, move, Hit
    }
   

    private Animator ani;
    bool isIdle;
    public Transform target;
    public float knockbackPower = 1;
    public float moveSpeed = 0.5f;
    public int Hp = 3;
    private SkeletonRenderer skeletonRenderer;
    private AnimState _AniState;
    //현재 애니메이션이 재생되고 있는지에 대한 변수
    //private string CurrentAnimation; 
    //움직임
    private Rigidbody2D rig;


    private float x;


    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    } 

    private void Start()
    {
        target = Player.Instance.transform;
        ani = GetComponent<Animator>();
        Player.Instance.Monster = this.gameObject;
        _AniState = AnimState.move;
    }

    private void Update()
    {
        transform.Translate(new Vector2(-1f * moveSpeed * Time.deltaTime, 0));
        Distance();
        SetCurrentAnimation(_AniState);
    }
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
            case AnimState.Hit:
                ani.SetTrigger("hit");
                break;
        }
    }
    //private void _AsncAnimation(AnimationReferenceAsset aniClip, bool loop, float timeScale)//해당애니메이션으로 변경한다
    //{
    //    if (aniClip.name.Equals(CurrentAnimation))//같은 애니메이션을 재생하려고 한다면 아래코드를 실행하지 않는다.
    //        return;
    //    skeletonAnimation.state.SetAnimation(0, aniClip, loop).TimeScale = timeScale;
    //    skeletonAnimation.loop = loop;
    //    skeletonAnimation.timeScale = timeScale;

    //    CurrentAnimation = aniClip.name;//현재 재생되고 있는 애니메이션 이름으로 변경
    //}
    public void Distance()
    {
        if (Vector2.Distance(target.position, transform.position) < 2f)
        {
            _AniState = AnimState.Idle;
            moveSpeed = 0;
            Player.Instance.moveSpeed = 0;
            Player.Instance._AniState = Player.AnimState.Attack;
            
        }
        else
        {
            Player.Instance._AniState = Player.AnimState.move;
            Player.Instance.moveSpeed = 1;
            _AniState = AnimState.move;
            moveSpeed = 1f;
        }
    }
    public void TakeDamage(int damage)
    {
        ani.SetTrigger("hit");
        Hp -= damage;
        if (Hp == 0)
        {
            MonsterSpawn._instance.MonsterCount--;
            Destroy(this.gameObject);
        }
    }
    void KnockBack(Vector2 pos)
    {
        // 반작용. 플레이어의 포지션 x값에서  충돌체의 포지션 x값을 뺀 값이  0보다 크다면 1 작다면 -1
        // 즉 부딪힌 목표물보다 왼쪽에서 맞았으면 왼쪽에서 튕겨나감. 반대는 오른쪽
        int reaction = transform.position.x - pos.x > 0 ? -2 : 2;
        rig.AddForce(new Vector2(reaction, 1) * knockbackPower, ForceMode2D.Impulse);
    }
}
