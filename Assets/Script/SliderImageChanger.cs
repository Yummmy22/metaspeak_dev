using UnityEngine;
using UnityEngine.UI;

public class SliderImageChanger : MonoBehaviour
{
    public Slider slider;
    public Image handleImage;
    public Sprite originalImage;
    public Sprite newImage; // Gambar yang akan diatur saat nilai slider menyentuh 0

    void Start()
    {
        if (slider == null || handleImage == null)
        {
            Debug.LogError("Slider atau Image tidak diatur di Inspector.");
            return;
        }

        // Tambahkan listener untuk mendeteksi perubahan nilai slider
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    void OnSliderValueChanged(float value)
    {
        // Jika nilai slider sama dengan 0, ganti gambar handle dengan gambar yang baru
        if (Mathf.Approximately(value, 0f))
        {
            handleImage.sprite = newImage;
        }
        else
        {
            // Jika nilai tidak 0, kembalikan gambar handle ke gambar asli atau sesuai kebutuhan
            handleImage.sprite = originalImage;
        }
    }
}
