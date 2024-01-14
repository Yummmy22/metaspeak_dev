using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Esc_Pause : MonoBehaviour
{
    public KeyCode triggerKey = KeyCode.Space; // Tombol yang dapat diatur di Inspector
    public UnityEvent onPressed;

    void Update()
    {
        // Mendeteksi penekanan tombol yang diatur di Inspector
        if (Input.GetKeyDown(triggerKey))
        {
            // Menjalankan Unity Event saat tombol ditekan
            onPressed.Invoke();
        }
    }
}
