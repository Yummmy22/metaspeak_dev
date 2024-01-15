using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class AudioHandler : MonoBehaviour
{
    private VolumeSettings audioManager;
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
        
    }
}
