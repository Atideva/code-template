using System.Collections.Generic;
using Meta.Interface;
using Sirenix.OdinInspector;
using SO.ShopSO;
using UnityEngine;

namespace SO.DatabasesSO
{
 
    public class ShopDatabaseSO : DatabaseSO
    {
        [Space(15)]
        [ShowInInspector, InlineEditor(InlineEditorObjectFieldModes.Boxed)] [SerializeField] List<ShopListSO> lists = new();
        [Space(15)]
        [ShowInInspector, InlineEditor(InlineEditorObjectFieldModes.Boxed)] [SerializeField] List<ShopBundleSO> bundles = new();
        [ShowInInspector, InlineEditor(InlineEditorObjectFieldModes.Boxed)] [SerializeField] List<ShopChestSO> chests = new();
        [ShowInInspector, InlineEditor(InlineEditorObjectFieldModes.Boxed)] [SerializeField] List<ShopSingleItemSO> items = new();
  

        public override IReadOnlyList<IDatabaseItem> Items { get; }

        public override void LoadItems()
        {
            // lists = DatabaseSearcher.Find<ShopListSO>();
            //
            // bundles = DatabaseSearcher.Find<ShopBundleSO>();
            // chests = DatabaseSearcher.Find<ShopChestSO>();
            // items = DatabaseSearcher.Find<ShopSingleItemSO>();
        }

        public override void MapItems()
        {
            Debug.Log("Shop database don't map items");
        }
    }
}