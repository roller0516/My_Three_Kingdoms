using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public float SpawnTime;
    public float CurTime;
    public int MaxCount;
    public int MonsterCount;
    public int RandomRange1;
    public int RandomRange2;
    public bool IsDie = false;
    public bool boss_IsDie = false;

    private Vector3 startPosition;

    public Transform SpawnPoints;
    public GameObject[] Monster;
    public GameObject[] BossMonster;
    private Fadeinout fade;
    public static MonsterSpawn _instance;
    
    public StageManager stg;
    private void Start()
    {
        fade = FindObjectOfType<Fadeinout>();
        stg = FindObjectOfType<StageManager>();
        startPosition = this.transform.position;
        _instance = this;
       
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
        }
        if (boss_IsDie == true)
        {
            boss_IsDie = false;
            stg.MonsterCount++;
            StartCoroutine("BossDeath");
            stg.curStage++;
           
        }
    }
    public void SpawnMonster(int num)
    {
        if (stg.MonsterCount % stg.MaxStage == 0)
        {
            CurTime = 0;
            MonsterCount++;

            for (int i =0; i<BossMonster.Length;i++)
            {
                if (i == stg.curStage - 1)
                    Instantiate(BossMonster[i], new Vector3(SpawnPoints.transform.position.x, SpawnPoints.transform.position.y, 0), Quaternion.identity);
                
            }
            if(stg.curStage>3)
                Instantiate(BossMonster[num], new Vector3(SpawnPoints.transform.position.x, SpawnPoints.transform.position.y, 0), Quaternion.identity);

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