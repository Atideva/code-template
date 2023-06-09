using Gameplay.Perks;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.Odin;

namespace SO.PerksSO
{
    public class PerkSO : BasicSO, ISerializeReferenceByAssetGuid
    {
        [Space(20)]
        [SerializeField] [Required] [InlineEditor(Expanded = true)] [LabelText("[Perk]")] Perk perkPrefab;
        public int MaxLevel => perkPrefab.MaxLevel;

        public Perk PerkPrefab => perkPrefab;
    }
}