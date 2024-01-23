using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zetcil;

public class SwipeController : MonoBehaviour
{
    [SerializeField] int maxPage;
    int currentPage;
    Vector3 targetPos;
    [SerializeField] Vector3 pagestep;
    [SerializeField] RectTransform PageRect;

    [Header("Tween Setting")]
    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;

    [Header("Navigation Setting")]
    [SerializeField] GameObject[] barObject;

    [Header("Button Arrow Setting")]
    [SerializeField] Button PrevBtn;
    [SerializeField] Button NextBtn;

    [Header("Left Button")]
    [SerializeField] Sprite NormalLeftImg;
    [SerializeField] Sprite DisableLeftImg;

    [Header("Right Button")]
    [SerializeField] Sprite NormalRightImg;
    [SerializeField] Sprite DisableRightImg;


    private void Awake()
    {
        currentPage = 1;
        targetPos = PageRect.localPosition;
        updateBar();
        UpdateButton();
    }

    public void Next()
    {
        if (currentPage < maxPage)
        {
            currentPage++;
            targetPos += pagestep;
            movePage();
        }
    }

    public void Previous()
    {
        if (currentPage > 1) 
        {
            currentPage--;
            targetPos -= pagestep;
            movePage();
        }
    }

    void movePage()
    {
        PageRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
        updateBar();
        UpdateButton();
    }

    void updateBar()
    {
        // Menonaktifkan semua GameObject
        foreach (var obj in barObject)
        {
            obj.SetActive(false);
        }

        // Mengaktifkan GameObject yang sesuai
        if (currentPage >= 1 && currentPage <= barObject.Length)
        {
            barObject[currentPage - 1].SetActive(true);
        }
    }

    void UpdateButton()
    {
        NextBtn.interactable = true;
        PrevBtn.interactable = true;

        if (currentPage == 1)
        {
            PrevBtn.interactable = false;
            SetButtonSprite(PrevBtn, DisableLeftImg);
        }
        else if (currentPage == maxPage)
        {
            NextBtn.interactable = false;
            SetButtonSprite(NextBtn, DisableRightImg);
        }
        else
        {
            SetButtonSprite(PrevBtn, NormalLeftImg);
            SetButtonSprite(NextBtn, NormalRightImg);
        }
    }

    void SetButtonSprite(Button button, Sprite sprite)
    {
        if (button != null && button.image != null)
        {
            button.image.sprite = sprite;
        }
        // Jika Anda menggunakan UI Text atau elemen lain pada button, sesuaikan kode ini
    }
}
