using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transitionAnimator;

    private bool isTransitioning = false;

    public void LoadNextLevel(string sceneName)
    {
        isTransitioning = true;
        StartCoroutine(LoadLevel(sceneName));
    }

    IEnumerator LoadLevel(string sceneName)
    {
        // Menjalankan animasi transisi
        if (transitionAnimator != null)
        {
            transitionAnimator.SetTrigger("Start");
        }

        // Menunggu sampai animasi selesai
        while (isTransitioning)
        {
            yield return null;
        }

        // Memuat level baru berdasarkan nama scene
        SceneManager.LoadScene(sceneName);
    }

    // Dipanggil oleh Animator sebagai event saat animasi selesai
    public void OnTransitionComplete()
    {
        isTransitioning = false;
    }
}
