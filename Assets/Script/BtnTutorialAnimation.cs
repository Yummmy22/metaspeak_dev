using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BtnTutorialAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Button Btn;
    Vector3 upScale = new Vector3(1.1f, 1.1f, 1);
    Vector3 originalScale;

    private void Awake()
    {
        Btn = gameObject.GetComponent<Button>();
        originalScale = transform.localScale; // Simpan skala asli tombol
        Btn.onClick.AddListener(Anim);
    }

    void Anim()
    {
        LeanTween.scale(gameObject, upScale, 0.1f);
        LeanTween.scale(gameObject, originalScale, 0.1f).setDelay(0.1f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Memastikan animasi hanya dipanggil jika tombol dapat diinteraksi
        if (Btn.interactable)
        {
            LeanTween.scale(gameObject, upScale, 0.1f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Kembalikan ke skala asli saat mouse keluar
        LeanTween.scale(gameObject, originalScale, 0.1f);
    }
}
