using GameManager;
using Meta.UI.Buttons;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Meta.UI.Popups
{
    public class LanguagePopupUI : PopupUI
    {
 
        [SerializeField] Transform container; 
        [SerializeField] [ReadOnly] LanguageSelectButtonUI[] languages;

        void Start()
        {
            languages = container.GetComponentsInChildren<LanguageSelectButtonUI>();
            foreach (var button in languages)
            {
                if(button.IsCurrentLanguage)
                    button.Highlight();
                else
                    button.Normal();
            }
        }

        protected override void OnShowUI()
        {
            foreach (var button in languages)
            {
                button.OnClick += Click;
            }
        }

        protected override void OnHideUI()
        {
            foreach (var button in languages)
            {
                button.OnClick -= Click;
            }
        }

        void Click(LanguageSelectButtonUI clicked)
        {
            foreach (var button in languages)
            {
                if (button == clicked)
                {
                    button.Highlight();
                    button.ApplyLanguage();
                    EventsUI.Instance.RefreshLocalization();
                }
                else
                    button.Normal();
            }
        }

 
    }
}
