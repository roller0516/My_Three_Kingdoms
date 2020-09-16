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

    public Transform SpawnPoints;
    public GameObject[] Monster;
    public GameObject[] BossMonster;

    public static MonsterSpawn _instance;

    private StageManager stg;
    private void Start()
    {
        _instance = this;
        stg = FindObjectOfType<StageManager>();
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
            stg.curStage++;
            StartCoroutine("Death");
        }
    }
    public void SpawnMonster(int num)
    {
        if (stg.curStage % stg.MaxStage == 0)
        {
            CurTime = 0;
            MonsterCount++;
            Instantiate(BossMonster[0], new Vector3(SpawnPoints.transform.position.x, SpawnPoints.transform.position.y, 0), Quaternion.identity);
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
}