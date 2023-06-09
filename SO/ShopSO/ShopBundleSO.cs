using System.Collections.Generic;
using Meta.Data;
using Sirenix.OdinInspector;
using SO.EquipmentSO;
using UnityEngine;

namespace SO.ShopSO
{
    [CreateAssetMenu(fileName = "New Bundle", menuName = "Shop/Bundle")]
    public class ShopBundleSO : ShopItemSO
    {
        [Space(30)]
        [SerializeField] List<ShopBundleData> currencies = new();
        [SerializeField]  [ShowInInspector, InlineEditor(InlineEditorObjectFieldModes.Boxed)] List<EquipSO> equipment;

        public IReadOnlyList<ShopBundleData> Currencies => currencies;
        public IReadOnlyList<EquipSO> Equipment => equipment;
    }
}