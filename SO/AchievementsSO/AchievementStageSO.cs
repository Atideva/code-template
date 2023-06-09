using System.Collections.Generic;
using Meta.Data;
using Meta.Enums;
using Sirenix.OdinInspector;
using SO.EquipmentSO;
using UnityEngine;
using Utilities.Odin;

namespace SO.AchievementsSO
{
    [CreateAssetMenu(fileName = "New Achievement Requirement", menuName = "Achievements/Achievement Requirement")]
    public class AchievementStageSO : ScriptableObject, ISerializeReferenceByAssetGuid
    {
        [Space(20)]
        [SerializeField] TierEnum tier;

        [Space(20)]
        [LabelText("Step Requirements")]
        [ListDrawerSettings(Expanded = true, HideAddButton = true, HideRemoveButton = true)]
        [SerializeField] List<float> steps = new() {0, 0, 0, 0, 0};

        [Space(20)]
        [HideLabel] [Header("Step Reward")]
     //   [SerializeField] BankCurrencyData stepReward;
        [SerializeField] int stepGemsReward;
        [Space(20)]
        [SerializeField] [InlineEditor] EquipSO finalReward;

        public IReadOnlyList<float> Steps => steps;

        public EquipSO FinalReward => finalReward;

        public TierEnum Tier => tier;

        public int StepGemsReward => stepGemsReward;
    }
}