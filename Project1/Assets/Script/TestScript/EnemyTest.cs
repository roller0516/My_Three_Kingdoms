using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class EnemyTest : MonoBehaviour
{
    public enum AnimState //몬스터 상태
    {
        Idle, move, Hit, die
    }

    private SkeletonRenderer skeletonRenderer;
    private AnimState _AniState;
    private Rigidbody rig;
    private Animator ani;
    public Transform target;
    public GameObject obj;
    public float knockbackPower = 1;
    public float moveSpeed = 0.5f;
    public int Hp;
    

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
        
    }


    private void Start()
    {
        moveSpeed = 0f;
        ani = GetComponent<Animator>();
        skeletonRenderer = GetComponent<SkeletonRenderer>();
        target = Player.Instance.transform;
        _AniState = AnimState.move;
        
        SetAttechment(MonsterSpawn._instance.RandomRange2 + 1);
    }

    private void Update()
    {
        transform.Translate(new Vector2(-1f * moveSpeed * Time.deltaTime, 0));
        SetCurrentAnimation(_AniState);
        Distance();
        //print(Vector2.Distance(target.position, transform.position));
    }
    //-------------------------------------------------------애니메이션
    private void SetCurrentAnimation(AnimState _state) //애니메이션 
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
            case AnimState.die:
                ani.SetBool("Die",true);
                break;
        }
    }
    //-----------------------------------------이벤트 효과
    public void Distance()
    {
        float d = Vector2.Distance(target.position, transform.position);

        if (d >3f && d < 4f) // 거리가 2보단크고 3보다 작을때 2~ 3.9
        {
           Player.Instance.Monster = this.gameObject;
           Player.Instance.moveSpeed = 3f;
           Player.Instance.moveSpeed = Mathf.Lerp(Player.Instance.moveSpeed, 0, Time.deltaTime);
           Player.Instance._AniState = Player.AnimState.moveSpeedup;
            print("여기");
        }
        else if (d > 2f && d <= 3f) // 2.1 ~ 3f
        {
            Player.Instance._AniState = Player.AnimState.Idle;

            if (_AniState == AnimState.die)
                moveSpeed = 0f;
            else
            {
                _AniState = AnimState.move;
                moveSpeed = 2f;
            }
        }
        else if (d <= 2f&& Hp>0) // 2보다 작거나 같을때 ex) 1.9f
        {
            Player.Instance._AniState = Player.AnimState.Attack;
            Player.Instance.moveSpeed = 0f;
            _AniState = AnimState.Idle;
            moveSpeed = 0;
        }
        else if (d > 4f)
        {
            Player.Instance._AniState = Player.AnimState.move;
            Player.Instance.moveSpeed = 2f;
            moveSpeed = 2f;
            _AniState = AnimState.move;
        }
    }
    public void TakeDamage(int damage)
    {
        GameObject G = Instantiate(obj, new Vector3(this.transform.position.x, this.transform.position.y + 1f, 0), Quaternion.identity);
        G.transform.parent = this.transform;
        G.GetComponent<DamageText>().Damage = damage;
        
        ani.SetTrigger("hit");

        Hp -= damage;

        KnockBack();

        if (Hp <= 0)
        {
            _AniState = AnimState.die;
            MonsterSpawn._instance.MonsterCount--;
            MonsterSpawn._instance.IsDie = true;
            Destroy(this.gameObject,2f);
        }
    }
    public void KnockBack()
    {
        rig.AddForce(new Vector3(2.5f, 2, 0) * knockbackPower, ForceMode.Impulse);
    }
    //-----------------------------------------------어태치먼트
    public void SetAttechment(int num)
    {
        skeletonRenderer.skeleton.SetAttachment("weapon 1", "weapon "+num);
    }
    //----------------------------------------------------------코루틴
    
}
