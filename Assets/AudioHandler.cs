using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class AudioHandler : MonoBehaviour
{
    private VolumeSettings audioManager;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    public void changeMusicVolume(Slider slider)
    {
        print("value changing");
        PlayerPrefs.SetFloat("musicVolume", slider.value);
        audioManager.SetMusicVolume(slider.value);
        print("value changed");
    }
    
    public void changeSfxVolume(Slider slider)
    {
        PlayerPrefs.SetFloat("sfxVolume",slider.value);
        audioManager.SetSFXVolume(slider.value);
        print("value changed");
    }

    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<VolumeSettings>();
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
    }
}
