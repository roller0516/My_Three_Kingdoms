
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using Quaternion = UnityEngine.Quaternion;


public class MimicEnemy : MonoBehaviour
{
    public enum AnimState //몬스터 상태
    {
        Idle, Hit, die
    }

    //private SkeletonRenderer skeletonRenderer;
    private AnimState _AniState;
    private Rigidbody rig;
    private Animator ani;
    private Transform target;

    public GameObject hit;
    public GameObject Crihit;
    public GameObject damageText;
    public GameObject CridamageText;

    public float moveSpeed = 0f;

    public BigInteger Hp;
    public string MaxHp;
    int count = 0;
    public Slider Hpbar;
    public Slider HpbarBasic;

    public int HitCount = 0;
    BigInteger goldreward;
    BigInteger knowledgereward;

    Camera cam = null;
    SpecialitemList sl;
    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }


    private void Start()
    {
        sl = GameObject.FindWithTag("Canvas").GetComponent<SpecialitemList>();
        ani = GetComponent<Animator>(); // 애니메이션

        target = Player.Instance.transform; // 플레이어를 타겟으로 한다
        Hp = BigInteger.Parse( MaxHp);
        cam = Camera.main;
        Hpbar = Instantiate(HpbarBasic, this.gameObject.transform.position, Quaternion.identity) as Slider;
        Hpbar.transform.SetParent(GameObject.Find("Canvas").transform);
        Hpbar.transform.SetAsFirstSibling();
        HitCount = 0;
    }

    private void Update()
    {
        transform.Translate(new Vector2(-1f * moveSpeed * Time.deltaTime, 0));//왼쪽으로 전진 
        SetCurrentAnimation(_AniState); // 실시간으로 애니메이션을 받아온다.
        Distance();//실시간으로 타겟과의 거리를 받아온다
        SetHpbar();
    }

    private void SetCurrentAnimation(AnimState _state) //애니메이션 
    {
        switch (_state)
        {
            case AnimState.Idle:
                ani.SetInteger("AniState", (int)AnimState.Idle);
                break;
            case AnimState.Hit:
                ani.SetTrigger("hit");
                break;
            case AnimState.die:
                ani.SetBool("Die", true);
                break;
        }
    }

    private void Distance()// 플레이어와의 거리를 계산한다.
    {
        float d = Vector2.Distance(target.position, transform.position);

        if (d > 3f && d < 4f) // 거리가 2보단크고 3보다 작을때 2~ 3.9
        {
            FindObjectOfType<CreatureSummon>().GetComponent<CreatureSummon>().Skillbutton.interactable = false;
            Player.Instance.Monster = this.gameObject;
            Player.Instance.moveSpeed = 3f;
            Player.Instance.moveSpeed = Mathf.Lerp(Player.Instance.moveSpeed, 0, Time.deltaTime);
            Player.Instance._AniState = Player.AnimState.moveSpeedup;
            ani.speed = 0;
        }
        else if (d > 2f && d <= 3f) // 2.1 ~ 3f
        {
            FindObjectOfType<CreatureSummon>().GetComponent<CreatureSummon>().Skillbutton.interactable = false;
            ani.speed = 1;
            if (_AniState == AnimState.die) // 몬스터의 애니메이션이 Die면 속도가 0
                moveSpeed = 0f;
            else // 다른 애니메이션이면 move 속도 2
            {
                moveSpeed = 0f;
            }
        }
        else if (d <= 2f && Hp > 0) // 2보다 크거나 같고 hp가 0보다 클때
        {

            if (HitCount == 0)
            {
                Player.Instance._AniState = Player.AnimState.Attack;
            }
            else if (HitCount == 1)
            {
                Player.Instance._AniState = Player.AnimState.Attack2;
            }
            else if (HitCount == 2)
            {
                Player.Instance._AniState = Player.AnimState.Attack3;
            }
            if (HitCount == 3)
                HitCount = 0;
            Player.Instance.moveSpeed = 0f;
            _AniState = AnimState.Idle;
            moveSpeed = 0;
        }
        else if (d > 4f) // 거리가 4보다 클때
        {
            FindObjectOfType<CreatureSummon>().GetComponent<CreatureSummon>().Skillbutton.interactable = false;
            Player.Instance._AniState = Player.AnimState.move;
            Player.Instance.moveSpeed = 2f;
            moveSpeed = 0;
            ani.speed = 0;
        }
    }
    public void TakeDamage(BigInteger damage) // 데미지 함수
    {
        HitCount++;
        SoundManager.instance.MimicHit();
        Instantiate(hit, new Vector3(this.transform.position.x, this.transform.position.y + 1.0f, -1), Quaternion.identity);
        Instantiate(damageText, new Vector3(this.transform.position.x, this.transform.position.y + 1.5f, -1), Quaternion.identity);// 데미지 텍스트 생성
                                                                                                                                   //go.transform.parent = this.transform;
        _AniState = AnimState.Hit;
        DamageText dam = FindObjectOfType<DamageText>();
        dam.Damage = damage;
        Hp -= damage;// hp 뺌
        HpCheck();
    }
    public void CriticalDamage(BigInteger damage) // 데미지 함수
    {
        HitCount++;
        Instantiate(Crihit, new Vector3(this.transform.position.x, this.transform.position.y + 1.0f, -1), Quaternion.identity);
        Instantiate(CridamageText, new Vector3(this.transform.position.x, this.transform.position.y + 1.5f, -1), Quaternion.identity);// 데미지 텍스트 생성                                                                                                                           //go.transform.parent = this.transform;

        DamageText dam = FindObjectOfType<DamageText>();

        dam.Damage = damage;
        ani.SetTrigger("hit");// 애니메이션 변경
        _AniState = AnimState.Hit;

        Hp -= damage;// hp 뺌

        HpCheck();
    }
    public void CreatureDamage(BigInteger damage) // 데미지 함수
    {
        SoundManager.instance.MimicHit();
        Instantiate(hit, new Vector3(this.transform.position.x, this.transform.position.y + 1.0f, -1), Quaternion.identity);
        Instantiate(damageText, new Vector3(this.transform.position.x, this.transform.position.y + 1.5f, -1), Quaternion.identity);// 데미지 텍스트 생성
        DamageText dam = FindObjectOfType<DamageText>();
        dam.Damage = damage;
        Hp -= damage;// hp 뺌

        
    }

    public void SetHpbar()
    {
        BigInteger num;
        num = BigInteger.Divide((BigInteger.Multiply(Hp, 100)), BigInteger.Parse(MaxHp));
        Hpbar.value = (float.Parse(num.ToString()) / 100);
        Hpbar.transform.position = cam.WorldToScreenPoint(this.transform.position + new Vector3(0, 1.3f, 0));
    }
    public void HpCheck()
    {
        if (Hp <= 0)
        {
            if (count == 0)
            {
                Hpbar.gameObject.SetActive(false);
                _AniState = AnimState.die;
                MonsterSpawn.instance.MimicIsDie = true;
                Deth();
            }
            count++;
        }
        else if (count > 0)
        {
            return;
        }
    }
    public void Deth()
    {
        Hpbar.gameObject.SetActive(false);
        Destroy(this.gameObject,2f);
    }
}