using System;
using Meta.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Meta.UI.Buttons
{
    public class OptionsToggleUI : MonoBehaviour
    {
        [LabelText("Type")] [SerializeField] OptionToggleEnum @enum;
        [SerializeField] ToggleButtonUI toggleButton;
        public event Action<OptionToggleEnum, bool> OnToggle = delegate { };

        public OptionToggleEnum Enum => @enum;

        public void SetToggle(bool isToggled)
        {
            if (toggleButton.IsToggled == isToggled) return;
            toggleButton.Toggle();
        }

        void Awake()
        {
            toggleButton.OnToggle += Toggle;
        }

        void OnDisable()
        {
            toggleButton.OnToggle -= Toggle;
        }

        void Toggle(bool toggle)
            => OnToggle(@enum, toggle);
    }
}