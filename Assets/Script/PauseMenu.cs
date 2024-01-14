using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseButton;
    public bool isPaused;

    [Header("Pause Menu Settings")]
    // Menambahkan referensi untuk animasi pada game object
    public Animator menuAnimator;
    public string pauseAnimationTrigger = "open"; // Nama trigger animasi
    public string resumeAnimationTrigger = "close"; // Nama trigger animasi

    [Header("Pause Button Settings")]
    // Menambahkan referensi untuk animasi pada tombol pause
    public Animator ButtonAnimator;
    public string pauseButtonAnimationTrigger = "Pressed"; // Nama trigger animasi tombol pause
    public string resumeButtonAnimationTrigger = "Selected"; // Nama trigger animasi tombol pause

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;

        // Menyetel trigger untuk memulai animasi pada menu
        if (menuAnimator != null)
        {
            menuAnimator.SetTrigger(pauseAnimationTrigger);
        }

        // Menyetel trigger untuk animasi tombol pause (jika diperlukan)
        if (ButtonAnimator != null)
        {
            ButtonAnimator.SetTrigger(pauseButtonAnimationTrigger);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;

        // Menyetel trigger untuk memulai animasi pada menu saat resume
        if (menuAnimator != null)
        {
            menuAnimator.SetTrigger(resumeAnimationTrigger);
        }

        // Menyetel trigger untuk animasi tombol pause saat resume (jika diperlukan)
        if (ButtonAnimator != null)
        {
            ButtonAnimator.SetTrigger(resumeButtonAnimationTrigger);
        }
    }
}
