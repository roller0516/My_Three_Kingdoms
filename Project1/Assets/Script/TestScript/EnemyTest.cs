using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class EnemyTest : MonoBehaviour
{
    public enum AnimState
    {
        Idle, move, Hit, die
    }


    private Animator ani;
    public Transform target;
    public float knockbackPower = 1;
    public float moveSpeed = 0.5f;
    public int Hp;
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
        SetCurrentAnimation(_AniState);
        Distance();

        print(Vector2.Distance(target.position, transform.position));

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
        float d = Vector2.Distance(target.position, transform.position);

        if (d > 2.8f && d < 3f) // 거리가 2보단크고 5보다 작을때 ex 2.5f
        {
            Player.Instance.moveSpeed = 3;
        }
        else if (d <= 2f) // 2보다 작거나 같을때 ex) 1.9f
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
            moveSpeed = 2f;
            _AniState = AnimState.move;
        }
    }
    public void TakeDamage(int damage)
    {
        ani.SetTrigger("hit");
        KnockBack();
        Hp -= damage;
        if (Hp == 0)
        {
            MonsterSpawn._instance.MonsterCount--;
            Destroy(this.gameObject);
        }
    }
    public void KnockBack()
    {
        //int reaction = transform.position.x - pos.x > 0 ? 1 : -1;
        rig.AddForce(new Vector2(3, 2) * knockbackPower, ForceMode2D.Impulse);
    }
}
