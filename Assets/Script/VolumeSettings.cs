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
            SetMusicVolume();
            SetSFXVolume();
        }
    }

    public void SetMusicVolume()
    {
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
        myMixer.SetFloat("music", MathF.Log10(PlayerPrefs.GetFloat("musicVolume")) * 20);
        Debug.Log(PlayerPrefs.GetFloat("musicVolume"));
    }

    public void SetSFXVolume()
    {
        PlayerPrefs.SetFloat("sfxVolume", sfxSlider.value);
        myMixer.SetFloat("sfx", MathF.Log10(PlayerPrefs.GetFloat("sfxVolume")) * 20);
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");

        SetMusicVolume();
        SetSFXVolume();
    }
}
