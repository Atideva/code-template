using System.Collections.Generic;
using Gameplay.Achievements;
using I2.Loc;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.Odin;

namespace SO.AchievementsSO
{
    [CreateAssetMenu(fileName = "New Achievement", menuName = "Achievements/Achievement ")]
    public class AchievementSO : BasicSO, ISerializeReferenceByAssetGuid
    {
        [SerializeField] LocalizedString slotInfo;
        [Space(20)]
        [LabelText("[Achievement Listener]")] [Required]
        [SerializeField] AchievementListener prefab;
        [SerializeField] [PreviewField] Sprite fullSlot;
        [SerializeField] [PreviewField] Sprite emptySlot;

        [Space(20)]
        [ListDrawerSettings(Expanded = true)]
        [InlineEditor] [ValidateInput(nameof(NotEmpty),"PLEASE ADD SOME STAGES")]
        [SerializeField] List<AchievementStageSO> stages = new();

        bool NotEmpty() => stages.Count > 0;

        public AchievementListener Prefab => prefab;

        public IReadOnlyList<AchievementStageSO> Stages => stages;

        public Sprite FullSlotSprite => fullSlot;

        public Sprite EmptySlotSprite => emptySlot;

        public LocalizedString SlotInfoLocalize => slotInfo;
    }
}