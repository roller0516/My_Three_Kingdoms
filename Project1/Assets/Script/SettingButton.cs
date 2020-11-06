using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingButton : MonoBehaviour
{
    public Slider BgmSlider;
    public Slider effectSoundSlider;
    public Button Credit;
    public GameObject SettingPanel;
    public Toggle BgmMute;
    public Toggle effectMute;
    void Start()
    {
        effectSoundSlider.value = 1;
        BgmSlider.value = 1;
        SettingPanel.gameObject.SetActive(false);
    }
    public void ButtonOn()
    {
        SettingPanel.gameObject.SetActive(true);
    }
    public void CloseButton()
    {
        SettingPanel.gameObject.SetActive(false);
    }
    
    void Update()
    {
        SoundManager.instance.myAudio.volume = effectSoundSlider.value;
        SoundManager.instance.BgmSource.volume = BgmSlider.value;
        SoundManager.instance.myAudio.mute = effectMute.isOn;
        SoundManager.instance.BgmSource.mute = BgmMute.isOn;
    }
}
