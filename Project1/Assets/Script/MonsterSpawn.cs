using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using Quaternion = UnityEngine.Quaternion;
[System.Serializable]
public class MonsterSpawnStage
{
    public GameObject[] Mosnter;
}
public class MonsterSpawn : MonoBehaviour
{
    public static MonsterSpawn instance;
    public static MonsterSpawn GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<MonsterSpawn>();
            if (instance == null)
            {
                GameObject container = new GameObject("MosterSpwan");
                instance = container.AddComponent<MonsterSpawn>();
            }
        }
        return instance;
    }

    public float SpawnTime;
    public float CurTime;
    public int MaxCount;
    public int MonsterCount;
    public int RandomRange1;
    public int RandomRange2;
    public int BossRandomRange;
    public bool IsDie = false;
    public bool boss_IsDie = false;
    public bool MimicIsDie = false;
    public bool BossSummonON;
    public bool BossFail;
    public GameObject PrevMonster;
    public Transform SpawnPoints;
    [SerializeField]
    public MonsterSpawnStage[] MonsterByStage;
    public GameObject[] BossMonster;
    public GameObject MimicMonster;

    public Vector3 startPosition;
    public Fadeinout fade;
    
    public BigInteger BossHpCount = 1000000000;
    public BigInteger MonsterHpCount = 100;

    public StageManager stg;
    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }
    private void Start()
    {
        fade = FindObjectOfType<Fadeinout>();
        stg = FindObjectOfType<StageManager>();
        startPosition = this.transform.position;
        DataController.GetInstance().LoadStage(this);
    }
    private void Update()
    {

        if (CurTime >= SpawnTime && MonsterCount<MaxCount)
        {
            if (stg.curStage >= 150)
            {
                BossRandomRange = Random.Range(0, 16);
            }
            else if (stg.curStage >= 100)
            {
                RandomRange1 = Random.Range(0, 6);
                if (RandomRange1 < 3)
                    RandomRange2 = Random.Range(0, 2);
                else
                    RandomRange2 = 0;
                SpawnMonster(2, RandomRange1);
            }
            
            else if (stg.curStage >= 50)
            {
                RandomRange1 = Random.Range(0, 6);
                if (RandomRange1 < 3)
                    RandomRange2 = Random.Range(0, 2);
                else
                    RandomRange2 = 0;
                SpawnMonster(1, RandomRange1);
            }
            else if (stg.curStage < 50)
            {
                RandomRange1 = Random.Range(0, 3);
                RandomRange2 = Random.Range(0, 3);
                SpawnMonster(0, RandomRange1);
            }
        }
        if (MonsterCount == 0)
        {
            CurTime += Time.deltaTime;
        }
        if (IsDie == true)
        {
            if (stg.MonsterCount % stg.MaxStage == 0)
            {
                MonsterCount = 0;
                IsDie = false;
                stg.MonsterCount++;
                stg.curStage++;
                MonsterHpCount = BigInteger.Divide((BigInteger.Multiply(MonsterHpCount, 100)), 100);
                StartCoroutine("MaxMonsterDie");
                DataController.GetInstance().SaveStage(this);
            }
            else
            {
                MonsterCount = 0;
                IsDie = false;
                stg.MonsterCount++;
                DataController.GetInstance().SaveStage(this);
            }
        }
        if (boss_IsDie == true)
        {
            BossSummonON = false;
            boss_IsDie = false;
            stg.MonsterCount++;
            StartCoroutine("BossDeath");
            stg.curStage++;
            MonsterHpCount = BigInteger.Divide((BigInteger.Multiply(MonsterHpCount, 100)), 100);
            BossHpCount = BigInteger.Multiply(MonsterHpCount,5);
            DataController.GetInstance().SaveStage(this);
        }
        if (MimicIsDie == true)
        {
            MimicIsDie = false;
            PopUpSystem.GetInstance().EnterDeongun = false;
            StartCoroutine("MimicDeath");
        }

    }
    public void SpawnMonster(int num,int num2)
    {
        if (PopUpSystem.GetInstance().gameObject.activeSelf && PopUpSystem.GetInstance().EnterDeongun)
        {
            UIManager.GetInstance().Starttime = 30;
            UIManager.GetInstance().Currenttime = 30;
            if (PrevMonster != null)
                Destroy(PrevMonster.gameObject);
            MimicHp(UIManager.GetInstance().SearchName);
            MonsterCount++;
            Instantiate(MimicMonster, new Vector3(SpawnPoints.transform.position.x, SpawnPoints.transform.position.y, 0), Quaternion.identity);
        }
        else if (stg.MonsterCount % stg.BossStage == 0) //보스 스폰
        {
            int bossStage = 0;
            UIManager.GetInstance().Starttime = 60;
            UIManager.GetInstance().Currenttime = 60;
            BossSummonON = true;
            SoundManager.instance.BossSound();
            CurTime = 0;
            MonsterCount++;

            if (stg.curStage > BossMonster.Length) // 랜덤으로 생성
            {
                GameObject go = Instantiate(BossMonster[BossRandomRange], new Vector3(SpawnPoints.transform.position.x, SpawnPoints.transform.position.y, 0), Quaternion.identity); ;
                PrevMonster = go;
            }
            else
            {
                bossStage = (int)(((stg.curStage / 10) - 1));
                GameObject go = Instantiate(BossMonster[bossStage], new Vector3(SpawnPoints.transform.position.x, SpawnPoints.transform.position.y, 0), Quaternion.identity);
                PrevMonster = go;
            }
        }
        
        else
        {
            if (stg.MonsterCount % stg.MaxStage == 0)// 5마리 마다 스테이지 변환
            {
                GameObject go = null;
                go = Instantiate(MonsterByStage[num].Mosnter[num2], new Vector3(SpawnPoints.transform.position.x, SpawnPoints.transform.position.y, 0), Quaternion.identity);
                CurTime = 0;
                MonsterCount++;
                PrevMonster = go;
            }
            else
            {
                GameObject go = null;
                CurTime = 0;
                MonsterCount++;
                go = Instantiate(MonsterByStage[num].Mosnter[num2], new Vector3(SpawnPoints.transform.position.x, SpawnPoints.transform.position.y, 0), Quaternion.identity);
                PrevMonster = go;
                StartCoroutine("Death");
            }
        }
    }
    IEnumerator Death()
    {
        yield return new WaitForSeconds(0.1f);
        transform.position = new Vector2(transform.position.x + 3f, transform.position.y);
    }
    IEnumerator BossDeath()
    {
        transform.position = startPosition;
        MonsterCount = 1;
        yield return new WaitForSeconds(0.3f);
        fade.Fade();
        yield return new WaitForSeconds(0.5f);
        MonsterCount = 0;
        Player.Instance.transform.position = Player.Instance.startPosition;
    }
    IEnumerator MimicDeath()
    {
        transform.position = startPosition;
        MonsterCount = 1;
        fade.SearchReward();
        yield return new WaitForSeconds(0.3f);
        fade.Fade();
        yield return new WaitForSeconds(0.5f);
        MonsterCount = 0;
        Player.Instance.transform.position = Player.Instance.startPosition;
    }
    IEnumerator MaxMonsterDie()
    {
        transform.position = startPosition;
        MonsterCount = 1;
        yield return new WaitForSeconds(0.3f);
        fade.Fade();
        yield return new WaitForSeconds(0.5f);
        MonsterCount = 0;
        Player.Instance.transform.position = Player.Instance.startPosition;
    }
    void MimicHp(string Name) 
    {
        switch (Name)
        {
            case "하북":
                MimicMonster.GetComponent<MimicEnemy>().MaxHp = "100000";
                break;
            case "청서":
                MimicMonster.GetComponent<MimicEnemy>().MaxHp = "2000000";
                break;
            case "중원":
                MimicMonster.GetComponent<MimicEnemy>().MaxHp = "3000000";
                break;
            case "강동":
                MimicMonster.GetComponent<MimicEnemy>().MaxHp = "4000000";
                break;
            case "관중":
                MimicMonster.GetComponent<MimicEnemy>().MaxHp = "5000000";
                break;
            case "형북":
                MimicMonster.GetComponent<MimicEnemy>().MaxHp = "60000000";
                break;
            case "형남":
                MimicMonster.GetComponent<MimicEnemy>().MaxHp = "700000000";
                break;
            case "파촉":
                MimicMonster.GetComponent<MimicEnemy>().MaxHp = "8000000000";
                break;
        }

    }
}