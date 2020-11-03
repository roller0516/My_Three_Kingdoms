
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using Quaternion = UnityEngine.Quaternion;

public class Boss : MonoBehaviour
{
    public string BossName;
    public enum AnimState //몬스터 상태
    {
        Idle, move, hit,die
    }

    private SkeletonRenderer skeletonRenderer;
    private AnimState _AniState;
    private Rigidbody rig;
    private Animator ani;
    private Transform target;
    private int MaxhitCount = 1;

    public int hitCount = 0;
    public GameObject hit;
    public GameObject Crihit;
    public GameObject damageText;
    public GameObject CridamageText;
    
    public float knockbackPower = 1;
    public float moveSpeed = 0.5f;

    public BigInteger Hp;
    public BigInteger MaxHp;

    public Slider Hpbar;
    public Slider HpbarBasic;

    BigInteger goldreward;
    BigInteger knowledgereward;
    SpecialitemList sl;
    Camera cam = null;
    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }


    private void Start()
    {
        sl = GameObject.FindWithTag("Canvas").GetComponent<SpecialitemList>();
        ani = GetComponent<Animator>(); // 애니메이션
        skeletonRenderer = GetComponent<SkeletonRenderer>();//스파인
        MaxHp = MonsterSpawn.instance.BossHpCount;
        target = Player.Instance.transform; // 플레이어를 타겟으로 한다
        _AniState = AnimState.move;// 애니메이션 변경
        cam = Camera.main;
        // 무기변경 랜덤으로 변경
        Hp = MaxHp;
        Hpbar = Instantiate(HpbarBasic, this.gameObject.transform.position, Quaternion.identity) as Slider;
        Hpbar.transform.SetParent(GameObject.Find("Canvas").transform);
        Hpbar.transform.SetAsFirstSibling();
        knowledgereward = BigInteger.Divide(BigInteger.Multiply(MaxHp, 5), 100);
        goldreward = BigInteger.Divide(BigInteger.Multiply(MaxHp, 115), 100);
        hitCount = 0;
    }

    private void Update()
    {
        transform.Translate(new Vector2(-1f * moveSpeed * Time.deltaTime, 0));//왼쪽으로 전진 
        SetCurrentAnimation(_AniState); // 실시간으로 애니메이션을 받아온다.
        SetHpbar();
        Distance();
         //실시간으로 타겟과의 거리를 받아온다
    }

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
            case AnimState.hit:
                ani.SetTrigger("hit");
                break;
            case AnimState.die:
                ani.SetBool("Die", true);
                break;
        }
    }

    public void Distance()// 플레이어와의 거리를 계산한다.
    {
        float d = Vector2.Distance(target.position, transform.position);

        if (d > 3f && d < 4f) // 거리가 2보단크고 3보다 작을때 2~ 3.9
        {
            Player.Instance.Monster = this.gameObject;
            Player.Instance.moveSpeed = 3f;
            Player.Instance.moveSpeed = Mathf.Lerp(Player.Instance.moveSpeed, 0, Time.deltaTime);
            Player.Instance._AniState = Player.AnimState.moveSpeedup;
        }
        else if (d > 2f && d <= 3f) // 2.1 ~ 3f
        {
            Player.Instance._AniState = Player.AnimState.Idle;

            if (_AniState == AnimState.die) // 몬스터의 애니메이션이 Die면 속도가 0
                moveSpeed = 0f;
            else // 다른 애니메이션이면 move 속도 2
            {
                _AniState = AnimState.move;
                moveSpeed = 2f;
            }
        }
        else if (d <=2  && Hp > 0) // 2보다 크거나 같고 hp가 0보다 클때
        {
            if (hitCount == 0)
            {
                Player.Instance._AniState = Player.AnimState.Attack;
            }
            else if (hitCount == 1)
            {
                Player.Instance._AniState = Player.AnimState.Attack2;
            }
            else if (hitCount == 2)
            {
                Player.Instance._AniState = Player.AnimState.Attack3;
            }
            if (hitCount == 3)
                hitCount = 0;
            Player.Instance.moveSpeed = 0f;
            _AniState = AnimState.Idle;
            moveSpeed = 0;
        }
        else if (d > 4f) // 거리가 4보다 클때
        {
            Player.Instance._AniState = Player.AnimState.move;
            Player.Instance.moveSpeed = 2f;
            moveSpeed = 2f;
            _AniState = AnimState.move;
        }
    }
    public void TakeDamage(BigInteger damage) // 데미지 함수
    {
        _AniState = AnimState.hit;
        hitCount++;
        SoundManager.instance.HitSound();
        Instantiate(hit, new Vector3(this.transform.position.x, this.transform.position.y + 1f, -1), Quaternion.identity);// 데미지 텍스트 생성
        Instantiate(damageText, new Vector3(this.transform.position.x, this.transform.position.y+ 2f, -1), Quaternion.identity);// 데미지 텍스트 생성
        DamageText dam = FindObjectOfType<DamageText>();
        dam.Damage = damage;
        Hp -= damage;// hp 뺌
        HpCheck();
    }
    public void CriticalDamage(BigInteger damage) // 데미지 함수
    {
        hitCount++;
        Instantiate(Crihit, new Vector3(this.transform.position.x, this.transform.position.y + 1f, -1), Quaternion.identity);
        Instantiate(CridamageText, new Vector3(this.transform.position.x, this.transform.position.y + 2f, -1), Quaternion.identity);// 데미지 텍스트 생성
        DamageText dam = FindObjectOfType<DamageText>();
        dam.Damage = damage;
        Hp -= damage;// hp 뺌
        HpCheck();

    }
    public void CreatureDamage(BigInteger damage) // 데미지 함수
    {
        _AniState = AnimState.hit;
        SoundManager.instance.HitSound();
        Instantiate(hit, new Vector3(this.transform.position.x, this.transform.position.y + 1f, -1), Quaternion.identity);// 데미지 텍스트 생성
        Instantiate(damageText, new Vector3(this.transform.position.x, this.transform.position.y + 2f, -1), Quaternion.identity);// 데미지 텍스트 생성
        DamageText dam = FindObjectOfType<DamageText>();
        dam.Damage = damage;
        Hp -= damage;// hp 뺌
    }
    public BigInteger GetGoldReward()
    {
        return goldreward;
    }
    public BigInteger GetKnowledgeReward()
    {
        return knowledgereward;
    }
    public void SetGoldReward(BigInteger newGold)
    {
        goldreward = newGold;
    }
    public void SetKnowledgeReward(BigInteger newKnowledge)
    {
        knowledgereward = newKnowledge;
    }
    public void SetHpbar()
    {
        BigInteger num;
        num = BigInteger.Divide((BigInteger.Multiply(Hp, 100)), MaxHp);
        Hpbar.value = (float.Parse(num.ToString()) / 100);
        Hpbar.transform.position = cam.WorldToScreenPoint(this.transform.position + new Vector3(0, 2.5f, 0));
    }
    public void HpCheck() 
    {
        if (Hp <= 0)
        {
            _AniState = AnimState.die;
            int num;
            if (sl.Sp_item[2].itemCount == 10 && sl.Sp_item[3].itemCount == 10 && sl.Sp_item[14].itemCount == 10)
            {
                num = UIManager.GetInstance().Teasurecost_Nomal[1].goldByUpgrade + sl.Sp_item[2].AbilityCount + sl.Sp_item[3].AbilityCount + sl.Sp_item[14].AbilityCount;
                print(num);
            }
            else if (sl.Sp_item[2].itemCount == 10 && sl.Sp_item[14].itemCount == 10)
            {
                num = UIManager.GetInstance().Teasurecost_Nomal[1].goldByUpgrade + sl.Sp_item[2].AbilityCount + sl.Sp_item[14].AbilityCount;
                print(num);
            }
            else if (sl.Sp_item[2].itemCount == 10 && sl.Sp_item[3].itemCount == 10)
            {
                num = UIManager.GetInstance().Teasurecost_Nomal[1].goldByUpgrade + sl.Sp_item[2].AbilityCount + sl.Sp_item[3].AbilityCount;
                print(num);
            }
            else if (sl.Sp_item[3].itemCount == 10 && sl.Sp_item[14].itemCount == 10)
            {
                num = UIManager.GetInstance().Teasurecost_Nomal[1].goldByUpgrade + sl.Sp_item[3].AbilityCount + sl.Sp_item[14].AbilityCount;
                print(num);
            }
            else if (sl.Sp_item[2].itemCount == 10)
            {
                num = UIManager.GetInstance().Teasurecost_Nomal[1].goldByUpgrade + sl.Sp_item[2].AbilityCount;
                print(num);
            }
            else if (sl.Sp_item[3].itemCount == 10)
            {
                num = UIManager.GetInstance().Teasurecost_Nomal[1].goldByUpgrade + sl.Sp_item[3].AbilityCount;
                print(num);
            }
            else if (sl.Sp_item[14].itemCount == 10)
            {
                num = UIManager.GetInstance().Teasurecost_Nomal[1].goldByUpgrade + sl.Sp_item[14].AbilityCount;
                print(num);
            }
            else
            {
                num = UIManager.GetInstance().Teasurecost_Nomal[1].goldByUpgrade;
                print(num);
            }
            MonsterSpawn.instance.boss_IsDie = true;
            //SetKnowledgeReward(GetKnowledgeReward());
            SetGoldReward(GetGoldReward() + ((GetGoldReward() * num * 100) / 10000));
            DataController.GetInstance().AddGold(GetGoldReward());
            DataController.GetInstance().AddKnowledge(GetKnowledgeReward());
            Player.Instance._AniState = Player.AnimState.move;
            Player.Instance.moveSpeed = 2f;
            BossDictionary.GetInstance().ChangeSprite(BossName);
            DataController.GetInstance().AddTicket(1);
            Destroy(this.gameObject, 2f);
            Hpbar.gameObject.SetActive(false);
        }
    }
}


