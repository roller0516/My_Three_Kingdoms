using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using Spine.Unity;
using System.Numerics;

public class Creature : MonoBehaviour
{
    private static Creature s_instance = null;

    public static Creature Instance
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = FindObjectOfType(typeof(Creature)) as Creature;
            }
            return s_instance;
        }
    }
    public enum AnimState //몬스터 상태
    {
        Move, Attack
    }
    private Animator ani;
    public GameObject Monster;
    public GameObject BossFX;
    public float moveSpeed = 0.5f;
    public int Maxhitcount;
    int hit = 0;
    // Start is called before the first frame update
    void Start()
    {
        
        Monster = Player.Instance.Monster;
        SoundManager.instance.creturesound();
        ani = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Monster == null || Monster.activeSelf == false)
            Destroy(this.gameObject, 1);
            
    }
    public void Attack() //공격 
    {
        if (Monster == null || Monster.activeSelf == false)
        {
            Destroy(this.gameObject, 1);
            return;
        }
        else if (Monster.tag == "Boss")
        {
            StartCoroutine("Bosshitcount");
        }
        else if (Monster.tag == "Monster")
        {
            StartCoroutine("Monsterhitcount");
        }
        else if (Monster.tag == "Monster1")
        {
            StartCoroutine("Mimichitcount");
        }

    }
    IEnumerator Mimichitcount() 
    {
        if (Monster == null || Monster.activeSelf == false)
        {
            Instantiate(BossFX,gameObject.transform);
            SoundManager.instance.creturesound();
            Destroy(this.gameObject,0.5f);
            yield break;
        }
        else
        {
            if (hit <= Maxhitcount)
            {
                Monster.GetComponent<MimicEnemy>().CreatureDamage(Player.Instance.my_PlayerDamage);
                yield return new WaitForSeconds(0.1f);
                hit++;
                StartCoroutine("Mimichitcount");
            }
            else
            {
                Destroy(this.gameObject, 0.5f);
                yield return new WaitForSeconds(0.1f);
                Instantiate(BossFX, gameObject.transform);
                yield return new WaitForSeconds(0.3f);
                SoundManager.instance.creturesound();
                hit = 0;
            }
        }
    }
    IEnumerator Monsterhitcount()
    {
        if (Monster == null || Monster.activeSelf == false)
        {
            Instantiate(BossFX, gameObject.transform);
            SoundManager.instance.creturesound();
            Destroy(this.gameObject, 0.5f);
            yield break;
        }
        else
        {
            if (hit <= Maxhitcount)
            {
                Monster.GetComponent<EnemyTest>().CreatureDamage(Player.Instance.my_PlayerDamage);
                yield return new WaitForSeconds(0.1f);
                hit++;
                StartCoroutine("Monsterhitcount");
            }
            else
            {
                Destroy(this.gameObject, 0.5f);
                yield return new WaitForSeconds(0.1f);
                Instantiate(BossFX, gameObject.transform);
                yield return new WaitForSeconds(0.3f);
                SoundManager.instance.creturesound();
                hit = 0;
            }
        }
        
           
    }
    IEnumerator Bosshitcount()
    {
        if (Monster == null || Monster.activeSelf == false)
        {
            SoundManager.instance.creturesound();
            Instantiate(BossFX, gameObject.transform);
            Destroy(this.gameObject, 0.5f);
            yield break;
        }
        else
        {
            if (hit <= Maxhitcount)
            {
                Monster.GetComponent<Boss>().CreatureDamage(Player.Instance.my_PlayerDamage);
                yield return new WaitForSeconds(0.1f);
                hit++;
                StartCoroutine("Bosshitcount");
            }
            else
            {
                Destroy(this.gameObject, 0.5f);
                yield return new WaitForSeconds(0.1f);
                Instantiate(BossFX, gameObject.transform);
                yield return new WaitForSeconds(0.3f);
                SoundManager.instance.creturesound();
                hit = 0;
            }
        }

    }
}
