using Meta.Enums;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.Odin;


namespace SO.EquipmentSO
{
    public class EquipSO : BasicSO, ISerializeReferenceByAssetGuid
    {
        [SerializeField] [Space(20)] [HideLabel]
        [EnumToggleButtons, GUIColor(1f, 1, 0f)]
        TierEnum tier;
      
        public TierEnum Tier => tier;
    }
}