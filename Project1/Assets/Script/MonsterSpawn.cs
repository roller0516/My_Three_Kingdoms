using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public int MaxCount;
    public int MonsterCount;
    public Transform SpawnPoints;
    public GameObject[] Monster;
    
    public float SpawnTime;
    public float CurTime;
    public int RandomRange1;
    public int RandomRange2;
    public static MonsterSpawn _instance;
    public bool IsDie = false;

    private void Start()
    {
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
            StartCoroutine("Death");
        }
    }
    public void SpawnMonster(int num)
    {
        CurTime = 0;
        MonsterCount++;
        Instantiate(Monster[num],new Vector3(SpawnPoints.transform.position.x, SpawnPoints.transform.position.y,0),Quaternion.identity);
    }
    IEnumerator Death()
    {
        yield return new WaitForSeconds(0.1f);
        transform.position = new Vector2(transform.position.x + 2f, transform.position.y);
       
    }
}
