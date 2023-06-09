using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class ButtonSwitcherUI : MonoBehaviour
    {
        [SerializeField] Sprite enableSprite;
        [SerializeField] Sprite disableSprite;
        [SerializeField] Image icon;
        [SerializeField] Button button;
        bool _switcher;
        public event Action<bool> OnSwitch = delegate { };

        void Awake()
        {
            button.onClick.AddListener(Click);
        }

        void Click()
        {
            Toggle();
            OnSwitch(_switcher);
        }

        public void Toggle()
        {
            _switcher = !_switcher;
            Refresh(_switcher);
        }

        void Refresh(bool state)
        {
            icon.sprite = state ? enableSprite : disableSprite;
        }

        public void Enable()
        {
            _switcher = true;
            Refresh(_switcher);
        }

        public void Disable()
        {
            _switcher = false;
            Refresh(_switcher);
        }
    }
}