using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class AudioHandler : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    private VolumeSettings audioManager;
    public void changeMusicVolume()
    {
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
        audioManager.SetMusicVolume();
    }
    
    public void changeSfxVolume()
    {
        PlayerPrefs.SetFloat("sfxVolume", sfxSlider.value);
        audioManager.SetSFXVolume();
    }

    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<VolumeSettings>();
        
    }
}
