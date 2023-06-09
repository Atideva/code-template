using System;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.UI.Buttons
{
    public class LanguageSelectButtonUI : MonoBehaviour
    {
        [Header("Button")]
        [SerializeField] Button clickButton;
        [SerializeField] SetLanguage setLanguage;

        [Header("Background")]
        [SerializeField] Sprite selectSprite;
        [SerializeField] Sprite disableSprite;
        [SerializeField] Image back;


        [Header("Icon")]
        [SerializeField] Image iconHighlight;
        [SerializeField] Color selectColor;
        [SerializeField] Color disableColor;

        public event Action<LanguageSelectButtonUI> OnClick = delegate { };
 
        public bool IsCurrentLanguage => setLanguage.IsCurrent;
        void Awake() => clickButton.onClick.AddListener(Click);
        void Click() => OnClick(this);

        public void ApplyLanguage()
        {
            setLanguage.ApplyLanguage();
        }

        public void Highlight()
        {
            back.sprite = selectSprite;
            iconHighlight.color = selectColor;
        }

        public void Normal()
        {
            back.sprite = disableSprite;
            iconHighlight.color = disableColor;
        }
    }
}