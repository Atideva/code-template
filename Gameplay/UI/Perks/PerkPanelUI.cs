using System.Collections.Generic;
using Meta.Data;
using UnityEngine;

namespace Gameplay.UI.Perks
{
    public class PerkPanelUI : MonoBehaviour
    {
        [SerializeField] List<PerkSlotUI> slots = new();

        public void Set(IReadOnlyList<PerkData> perks)
        {
            for (var i = 0; i < slots.Count; i++)
            {
                if (i < perks.Count)
                    slots[i].Set(perks[i]);
                else
                    slots[i].Empty();
            }
        }
    }
}