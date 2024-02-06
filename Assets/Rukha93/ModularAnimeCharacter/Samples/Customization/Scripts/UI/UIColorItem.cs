using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rukha93.ModularAnimeCharacter.Customization.UI
{
    public class UIColorItem : MonoBehaviour
    {
        [SerializeField] private Button m_LeftButton;
        [SerializeField] private Image m_LeftImage;
        [SerializeField] private Text m_LeftText;

        [Space]
        [SerializeField] private Image m_RightImage;
        [SerializeField] private Button m_RightButton;
        [SerializeField] private Text m_RightText;

        public System.Action OnClickShade;
        public System.Action OnClickLight;

        private Color m_ShadeColor;
        public Color ShadeColor
        {
            get => m_ShadeColor;
            set
            {
                m_ShadeColor = value;
                m_LeftImage.color = new Color(value.r, value.g, value.b, 1);
            }
        }

        private Color m_LightColor;
        public Color LightColor
        {
            get => m_LightColor;
            set
            {
                m_LightColor = value;
                m_RightImage.color = new Color(value.r, value.g, value.b, 1);
            }
        }

        private void Awake()
        {
            m_LeftButton.onClick.AddListener(_OnClickShade);
            m_RightButton.onClick.AddListener(_OnClickLight);
        }

        private void _OnClickShade()
        {
            OnClickShade?.Invoke();
        }
        private void _OnClickLight()
        {
            OnClickLight?.Invoke();
        }

        public void SetSingleColor(bool value)
        {
            m_LeftImage.gameObject.SetActive(!value);
            m_LeftButton.gameObject.SetActive(!value);

            m_LeftText.gameObject.SetActive(!value);
            m_RightText.gameObject.SetActive(!value);
        }
    }
}
