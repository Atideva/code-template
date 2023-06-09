using System;
using System.Collections.Generic;
using System.Linq;
using GameManager;
using Meta.Data;
using Meta.Save.Storage;
using Meta.Static;
using SO.EquipmentSO;

namespace Meta.Facade
{
    public static class Inventory
    {
        static InventoryEquipmentStorage _inventory;
        public static IEnumerable<EquipmentData> Equipment => _inventory.Equipment;
        
        static bool Null
        {
            get
            {
                if (_inventory) return false;
                Log.InitError(nameof(Inventory));
                return true;
            }
        }
        public static void Init(InventoryEquipmentStorage inventory)
        {
            _inventory = inventory;
            _inventory.OnAdd += OnAddCall;
            _inventory.OnRemove += OnRemoveCall;
        }
        
        static void OnAddCall(EquipmentData d) => OnAdd(d);
        static void OnRemoveCall(EquipmentData d) => OnRemove(d);

        public static event Action<EquipmentData> OnAdd = delegate { };
        public static event Action<EquipmentData> OnRemove = delegate { };

        public static void AddNew(EquipSO so)
        {
            var equip = Equip.NewData(so);
            Add(equip);
        }

        public static void Add(EquipmentData equip)
        {
            if (Null) return;
            if (Equip.IsNull(equip)) return;
            Log.InventoryAddItem(Equip.Name(equip));
            _inventory.Add(equip);
        }

        public static void AddMany(List<EquipmentData> equips)
        {
            if (Null) return;
            var notNullEquip = equips.Where(equip => !Equip.IsNull(equip)).ToList();
            foreach (var equip in notNullEquip)
            {
                Log.InventoryAddItem(Equip.Name(equip));
            }

            _inventory.AddMany(notNullEquip);
        }

        public static void Remove(EquipmentData equip)
        {
            if (Null) return;
            if (Equip.IsNull(equip)) return;
            Log.InventoryRemoveItem(Equip.Name(equip));
            _inventory.Remove(equip);
        }

        public static void RemoveMany(List<EquipmentData> equips)
        {
            if (Null) return;
            foreach (var equip in equips)
            {
                Log.InventoryRemoveItem(Equip.Name(equip));
            }

            _inventory.RemoveMany(equips);
        }
        
    }
}