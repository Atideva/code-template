using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SO.ShopSO
{
    [CreateAssetMenu(fileName = "New Shop List", menuName = "Shop/List")]
    public class ShopListSO : BasicSO
    {
        [Space(30)]
        [SerializeField]  [ShowInInspector, InlineEditor(InlineEditorObjectFieldModes.Boxed)] List<ShopItemSO> items = new();
        public IReadOnlyList<ShopItemSO> Items => items;
    }
}