using System;
using System.Collections.Generic;
using Meta.Enums;
using Meta.UI.Buttons;
using UnityEngine;

namespace Meta.UI.Popups
{
    public class OptionsPopupUI : PopupUI
    {
        [SerializeField] List<OptionsToggleUI>  toggles;
        public event Action<OptionToggleEnum, bool> OnToggle = delegate { };

        public IEnumerable<OptionsToggleUI> Toggles => toggles;

        void Start()
        {
            foreach (var toggle in toggles)
            {
                toggle.OnToggle += Toggle;
            }
        }

        void OnDestroy()
        {
            foreach (var toggle in toggles)
            {
                toggle.OnToggle -= Toggle;
            }
        }

        void Toggle(OptionToggleEnum type, bool toggle)
            => OnToggle(type, toggle);
    }
}
