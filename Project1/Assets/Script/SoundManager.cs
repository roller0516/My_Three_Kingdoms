using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource myAudio;
    public AudioSource BgmSource;
    public AudioClip[] WeaponSound_array;
    public AudioClip ButtonClickSound;
    public AudioClip Boss_s;
    public AudioClip Tap_s;
    public AudioClip Purchase_s;
    public AudioClip Critical_s;
    public AudioClip HIT;
    public AudioClip Mimichit;
    public AudioClip EquipSuond;
    public AudioClip TreasureFail;
    public AudioClip TreasuerSuccess;
    public AudioClip weaponbuttonsound;
    public AudioClip[] Bgm;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        BgmSound(4);
    }

    public void WeaponSound(int num)
    {
        switch (num)
        {
            case 1:
                myAudio.PlayOneShot(WeaponSound_array[0]);
                break;
            case 2:
                myAudio.PlayOneShot(WeaponSound_array[1]);
                break;
            case 3:
                myAudio.PlayOneShot(WeaponSound_array[2]);
                break;
            case 4:
                myAudio.PlayOneShot(Critical_s);
                break;
        }
    }
    public void ButtonSound()
    {
        myAudio.PlayOneShot(ButtonClickSound);
    }
    public void BossSound() 
    {
        myAudio.PlayOneShot(Boss_s);
    }
    public void TapSound() 
    {
        myAudio.PlayOneShot(Tap_s);
    }
    public void HitSound() 
    {
        myAudio.PlayOneShot(HIT);
    }
    public void EquipSound() 
    {
        myAudio.PlayOneShot(EquipSuond);
    }
    public void BgmSound(int num) 
    {
        BgmSource.clip = Bgm[num];
        BgmSource.Play();
    }
    public void Purchase()
    {
        myAudio.PlayOneShot(Purchase_s);
    }
    public void MimicHit()
    {
        myAudio.PlayOneShot(Mimichit);
    }
    public void Treasurefail()
    {
        myAudio.PlayOneShot(TreasureFail);
    }
    public void TreasureSuccess()
    {
        myAudio.PlayOneShot(TreasuerSuccess);
    }

    public void WeaponButtonSound()
    {
        myAudio.PlayOneShot(weaponbuttonsound);
    }
}

