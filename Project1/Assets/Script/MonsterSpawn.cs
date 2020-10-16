using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using Quaternion = UnityEngine.Quaternion;

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
    public bool IsDie = false;
    public bool boss_IsDie = false;

    public Transform SpawnPoints;
    public GameObject[] Monster;
    public GameObject[] BossMonster;

    private Vector3 startPosition;
    private Fadeinout fade;
    
    public BigInteger BossHpCount = 2850;
    public BigInteger MonsterHpCount = 570;

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
            RandomRange1 = Random.Range(0, Monster.Length);
            RandomRange2 = Random.Range(0, Monster.Length);
            SpawnMonster(RandomRange1);
        }
        if (MonsterCount == 0)
        {
            CurTime += Time.deltaTime;
        }
        if (IsDie == true)
        {
            IsDie = false;
            stg.MonsterCount++;
            StartCoroutine("Death");
            DataController.GetInstance().SaveStage(this);
        }
        if (boss_IsDie == true)
        {
            print(boss_IsDie);
            boss_IsDie = false;
            stg.MonsterCount++;
            StartCoroutine("BossDeath");
            stg.curStage++;
            MonsterHpCount = BigInteger.Divide((BigInteger.Multiply(MonsterHpCount, 115)), 100);
            BossHpCount = BigInteger.Multiply(MonsterHpCount,5);
            DataController.GetInstance().SaveStage(this);
        }
    }
    public void SpawnMonster(int num)
    {
        print(stg.curStage);
        if (stg.MonsterCount % stg.MaxStage == 0) //보스 스폰
        {
            SoundManager.instance.BossSound();
            CurTime = 0;
            MonsterCount++;

            for (int i =0; i<BossMonster.Length;i++)
            {
                if (i == stg.curStage - 1)
                {
                    Instantiate(BossMonster[i], new Vector3(SpawnPoints.transform.position.x, SpawnPoints.transform.position.y, 0), Quaternion.identity);
                    BossMonster[i].GetComponent<Boss>().BossName = BossMonster[i].name;
                    print(BossMonster[num].name);
                }
            }
            if (stg.curStage > BossMonster.Length)
            {
                Instantiate(BossMonster[num], new Vector3(SpawnPoints.transform.position.x, SpawnPoints.transform.position.y, 0), Quaternion.identity);
                BossMonster[num].GetComponent<Boss>().BossName = BossMonster[num].name;
            }
        }
        else 
        {
            CurTime = 0;
            MonsterCount++;
            Instantiate(Monster[num], new Vector3(SpawnPoints.transform.position.x, SpawnPoints.transform.position.y, 0), Quaternion.identity);
        }
    }
    IEnumerator Death()
    {
        yield return new WaitForSeconds(0.1f);
        transform.position = new Vector2(transform.position.x + 2f, transform.position.y);
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
}