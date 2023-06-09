using Meta.Data;
using Meta.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SO.ConfigsSO
{
    [CreateAssetMenu(fileName = "Equipment Tiers", menuName = "Config/Equipment Tiers")]
    public class EquipmentTiersSO : ScriptableObject
    {
       [HideLabel] [Space(35)] [SerializeField] EquipmentTierData tier1;
       [HideLabel]   [Space(35)] [SerializeField] EquipmentTierData tier2;
       [HideLabel]   [Space(35)] [SerializeField] EquipmentTierData tier3;
       [HideLabel]   [Space(35)] [SerializeField] EquipmentTierData tier4;
       [HideLabel]  [Space(35)] [SerializeField] EquipmentTierData tier5;
       [HideLabel]   [Space(35)] [SerializeField] EquipmentTierData tier6;

        public EquipmentTierData Get(TierEnum tier) =>
            tier switch
            {
                TierEnum.Tier1 => tier1,
                TierEnum.Tier2 => tier2,
                TierEnum.Tier3 => tier3,
                TierEnum.Tier4 => tier4,
                TierEnum.Tier5 => tier5,
                TierEnum.Tier6 => tier6,
                _ => null
            };
    }
}