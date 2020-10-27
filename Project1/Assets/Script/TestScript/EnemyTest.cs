
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using Quaternion = UnityEngine.Quaternion;

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
    private Transform target;

    public int HitCount = 0;
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
        skeletonRenderer = GetComponent<SkeletonRenderer>();//스파인

        target = Player.Instance.transform; // 플레이어를 타겟으로 한다

        _AniState = AnimState.move;// 애니메이션 변경

        SetAttechment(MonsterSpawn.instance.RandomRange2 + 1);

        MaxHp = MonsterSpawn.instance.MonsterHpCount; // 무기변경 랜덤으로 변경
        Hp = MaxHp;
        cam = Camera.main;
        Hpbar = Instantiate(HpbarBasic , this.gameObject.transform.position,Quaternion.identity) as Slider;
        Hpbar.transform.SetParent(GameObject.Find("Canvas").transform);
        Hpbar.transform.SetAsFirstSibling();
        HitCount = 0;
        goldreward = BigInteger.Divide(BigInteger.Multiply(MaxHp, 115), 100);
        knowledgereward = BigInteger.Divide(BigInteger.Multiply(MaxHp, 5), 100);
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
            case AnimState.move:
                ani.SetInteger("AniState", (int)AnimState.move);
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
            Player.Instance.Monster = this.gameObject;
            Player.Instance.moveSpeed = 3f;
            Player.Instance.moveSpeed = Mathf.Lerp(Player.Instance.moveSpeed, 0, Time.deltaTime);
            Player.Instance._AniState = Player.AnimState.moveSpeedup;
        }
        else if (d > 2f && d <= 3f) // 2.1 ~ 3f
        {
            //Player.Instance._AniState = Player.AnimState.Idle;

            if (_AniState == AnimState.die) // 몬스터의 애니메이션이 Die면 속도가 0
                moveSpeed = 0f;
            else // 다른 애니메이션이면 move 속도 2
            {
                _AniState = AnimState.move;
                moveSpeed = 2f;
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
            else if(HitCount == 2) 
            {
                Player.Instance._AniState = Player.AnimState.Attack3;
            }
            if(HitCount == 3)
                HitCount = 0;
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
        ani.SetTrigger("hit");
        HitCount++;
        SoundManager.instance.HitSound();
        Instantiate(hit, new Vector3(this.transform.position.x, this.transform.position.y+1.0f, -1), Quaternion.identity);
        Instantiate(damageText, new Vector3(this.transform.position.x, this.transform.position.y+1.5f , -1), Quaternion.identity);// 데미지 텍스트 생성
        //go.transform.parent = this.transform;
        
        DamageText dam = FindObjectOfType<DamageText>();
        dam.Damage = damage;

        // 애니메이션 변경

        //KnockBack();
       
        Hp -= damage;// hp 뺌
        
        if (Hp <= 0)
        {
            _AniState = AnimState.die;
            int num;
            if (sl.Sp_item[2].itemCount == 10 && sl.Sp_item[3].itemCount == 10&& sl.Sp_item[14].itemCount == 10)
            {
                num = UIManager.GetInstance().Teasurecost_Nomal[1].goldByUpgrade + sl.Sp_item[2].AbilityCount + sl.Sp_item[3].AbilityCount+ sl.Sp_item[14].AbilityCount;
                print(num);
            }
            else if (sl.Sp_item[2].itemCount == 10 && sl.Sp_item[14].itemCount == 10)
            {
                num = UIManager.GetInstance().Teasurecost_Nomal[1].goldByUpgrade + sl.Sp_item[2].AbilityCount + sl.Sp_item[14].AbilityCount;
                print(num);
            }
            else if (sl.Sp_item[2].itemCount == 10 && sl.Sp_item[3].itemCount == 10)
            {
                num = UIManager.GetInstance().Teasurecost_Nomal[1].goldByUpgrade + sl.Sp_item[2].AbilityCount+ sl.Sp_item[3].AbilityCount;
                print(num);
            }
            else if (sl.Sp_item[3].itemCount == 10&& sl.Sp_item[14].itemCount == 10)
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

            }
            Hpbar.gameObject.SetActive(false);
            SetGoldReward(GetGoldReward()+ ((GetGoldReward() * num * 100) / 10000));
            //SetKnowledgeReward(GetKnowledgeReward());
            DataController.GetInstance().AddGold(GetGoldReward());
            DataController.GetInstance().AddKnowledge(GetKnowledgeReward());
            MonsterSpawn.instance.MonsterCount--;
            MonsterSpawn.instance.IsDie = true;
            Destroy(this.gameObject, 2f);
            Hpbar.gameObject.SetActive(false);
        }
    }
    public void CriticalDamage(BigInteger damage) // 데미지 함수
    {
        ani.SetTrigger("hit");
        HitCount++;
        Instantiate(Crihit, new Vector3(this.transform.position.x, this.transform.position.y + 1.0f, -1), Quaternion.identity);
        Instantiate(CridamageText, new Vector3(this.transform.position.x, this.transform.position.y + 1.5f, -1), Quaternion.identity);// 데미지 텍스트 생성                                                                                                                           //go.transform.parent = this.transform;

        DamageText dam = FindObjectOfType<DamageText>();

        dam.Damage = damage;
        // 애니메이션 변경

        //KnockBack();

        Hp -= damage;// hp 뺌
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
            Hpbar.gameObject.SetActive(false);
            SetGoldReward(GetGoldReward()+((GetGoldReward()* num * 100) / 10000));
            //SetKnowledgeReward(GetKnowledgeReward());
            DataController.GetInstance().AddGold(GetGoldReward());
            DataController.GetInstance().AddKnowledge(GetKnowledgeReward());
            MonsterSpawn.instance.MonsterCount--;
            MonsterSpawn.instance.IsDie = true;
            Destroy(this.gameObject, 2f);
            Hpbar.gameObject.SetActive(false);
        }
    }
    private void KnockBack()// 넉백
    {
        rig.AddForce(new Vector3(2.5f, 2, 0) * knockbackPower, ForceMode.Impulse);
    }

    private void SetAttechment(int num) // 무기변경
    {
        skeletonRenderer.skeleton.SetAttachment("weapon 1", "weapon " + num);
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
        Hpbar.value = (float.Parse(num.ToString())/ 100);
        Hpbar.transform.position = cam.WorldToScreenPoint(this.transform.position + new Vector3(0,1.3f,0));
    }
}
