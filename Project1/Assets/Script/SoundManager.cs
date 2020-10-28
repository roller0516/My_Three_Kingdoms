using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    AudioSource myAudio;
    public AudioClip Weapon1Sound;
    public AudioClip Weapon2Sound;
    public AudioClip Weapon3Sound;
    public AudioClip ButtonClickSound;
    public AudioClip Boss_s;
    public AudioClip Tap_s;
    public AudioClip Critical_s;
    public AudioClip HIT;


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
        myAudio = GetComponent<AudioSource>();
    }

    public void WeaponSound(int num)
    {
        switch (num)
        {
            case 1:
                myAudio.PlayOneShot(Weapon1Sound);
                break;
            case 2:
                myAudio.PlayOneShot(Weapon2Sound);
                break;
            case 3:
                myAudio.PlayOneShot(Weapon3Sound);
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
}

