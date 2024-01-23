using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private AudioHandler audioHandler;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume") || PlayerPrefs.HasKey("sfxVolume"))
        {
            LoadVolume();
        }
        else
        {
            audioHandler.changeMusicVolume(musicSlider);
            audioHandler.changeSfxVolume(sfxSlider);
        }
    }

    public void SetMusicVolume(float sliderValue)
    {
        PlayerPrefs.SetFloat("musicVolume",sliderValue);
        myMixer.SetFloat("music", MathF.Log10(PlayerPrefs.GetFloat("musicVolume")) * 20);
    }

    public void SetSFXVolume(float sliderValue)
    {
        PlayerPrefs.SetFloat("sfxVolume", sliderValue);
        myMixer.SetFloat("sfx", MathF.Log10(PlayerPrefs.GetFloat("sfxVolume")) * 20);
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");

    }
}
