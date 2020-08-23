using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public int MaxCount;
    public int MonsterCount;
    public Transform[] SpawnPoints;
    public GameObject[] Monster;

    public float SpawnTime;
    public float CurTime;

    public static MonsterSpawn _instance;
    private void Start()
    {
        _instance = this;
    }

    private void Update()
    {
        if (CurTime >= SpawnTime && MonsterCount<MaxCount)
        {
            int x = Random.Range(0, Monster.Length);
            int y = Random.Range(0, Monster.Length);
            int z = Random.Range(0, Monster.Length);
            
            //int y = Random.Range(0, SpawnPoints.Length);
            SpawnMonster(x, 0);
            
        }
        CurTime +=Time.deltaTime;
    }
    public void SpawnMonster(int num_1,int num_2)
    {
        CurTime = 0;
        MonsterCount++;
        Instantiate(Monster[num_1], SpawnPoints[num_2]);
        
    }

}
