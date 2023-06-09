using System;
using System.Collections.Generic;
using GameManager;
using MenuTabsUI;
using Meta.Data;

namespace Meta.UI.StatusSignals
{
    public class StatusSignalTabEquipment : StatusSignal
    {
        public Tab equipmentTab;

        protected override void OnInit()
        {
            equipmentTab.OnTabEnabled += OnTabEnabled;
            EventsUI.Instance.OnShowNewItems += OnNewItems;
        }

        void OnDisable()
        {
            equipmentTab.OnTabEnabled -= OnTabEnabled;
            EventsUI.Instance.OnShowNewItems -= OnNewItems;
        }

        void OnNewItems(List<EquipmentData> equip)
        {
            if (SignalPassive)
                EnableSignal();
        }

        void OnTabEnabled()
        {
            if (SignalActive)
                DisableSignal();
        }
    }
}