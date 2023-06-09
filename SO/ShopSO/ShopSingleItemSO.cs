using Meta.Data;
using Meta.Enums;
using Sirenix.OdinInspector;
using SO.EquipmentSO;
using UnityEngine;

namespace SO.ShopSO
{
    public enum ShopSingleItemEnum
    {
        BankCurrency,
        Equipment
    }

    [CreateAssetMenu(fileName = "New Item", menuName = "Shop/Item")]
    public class ShopSingleItemSO : ShopItemSO
    {
        [Space(50)]
        [SerializeField] ShopSingleItemEnum type;

        [Space(20)]
        [ShowIf("type", ShopSingleItemEnum.BankCurrency)] [HideLabel] 
        [SerializeField] BankCurrencyData currency;
 

        [Space(20)]
        [ShowIf("type", ShopSingleItemEnum.Equipment)]
        [ShowInInspector, InlineEditor(InlineEditorObjectFieldModes.Boxed)] [SerializeField] EquipSO equip;
    }
}