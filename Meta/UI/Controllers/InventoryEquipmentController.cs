using System;
using System.Collections.Generic;
using Meta.Data;
using Meta.Facade;
using Meta.Static;
using Utilities.Pools;
using static Meta.Static.Equip;


namespace Meta.UI.Controllers
{
    public class InventoryEquipmentController : WithPool<ItemUIPool, ItemUI>
    {
        readonly Dictionary<EquipmentData, ItemUI> _dict = new();

        void Start()
        {
            _dict.Clear();
            LoadItems();

            Inventory.OnAdd += CreateItem;
            Inventory.OnRemove += DeleteItem;
        }

        void OnDisable()
        {
            Inventory.OnAdd -= CreateItem;
            Inventory.OnRemove -= DeleteItem;
        }

        void LoadItems()
        {
            foreach (var equip in Inventory.Equipment)
                CreateItem(equip);
        }

        void DeleteItem(EquipmentData equip)
        {
            var val = _dict[equip];
            _dict.Remove(equip);
            val.ReturnToPool();
        }

        void CreateItem(EquipmentData equip)
        {
            if (equip == null)
            {
                Log.Error("Trying to add empty item to inventory");
                return;
            }

            if (_dict.ContainsKey(equip))
            {
                Log.Error("Same equip already in inventory");
                return;
            }

            if (IsNull(equip))
            {
                Log.Warning("Trying to add empty item to inventory");
            }

            var item = Pool.Get();
            var itemData = Item.NewUIData(equip);
            item.Refresh(itemData);


#if UNITY_EDITOR
            var itemName = Name(equip) != string.Empty
                ? Name(equip)
                : equip.so.name;
            item.gameObject.name = "[Inventory] Item - " + itemName;
#endif


            item.SetLevelUpAble(false);

            _dict.Add(equip, item);
        }
    }
}