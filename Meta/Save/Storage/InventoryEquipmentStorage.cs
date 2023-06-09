using System;
using System.Collections.Generic;
using Meta.Data;
using Meta.Facade;
using Meta.Save.SaveSystem;
using SO.ConfigsSO;
using SO.EquipmentSO;

namespace Meta.Save.Storage
{
    public class InventoryEquipmentStorage : Saveable<InventoryEquipmentData>
    {
        public event Action<EquipmentData> OnAdd = delegate { };
        public event Action<EquipmentData> OnRemove = delegate { };
       
        bool _init;

        public void Init(FirstLaunchSO config)
        {
            Load();
        }

        public void Add(EquipmentData equip)
        {
            AddSorted(equip);
            Save();
        }

        public void Remove(EquipmentData equip)
        {
            RemoveSorted(equip);
            Save();
        }

        public void AddMany(List<EquipmentData> equips)
        {
            foreach (var equip in equips)
                AddSorted(equip);
            Save();
        }

        public void RemoveMany(List<EquipmentData> equips)
        {
            foreach (var equip in equips)
                RemoveSorted(equip);
            Save();
        }

        void AddSorted(EquipmentData equip)
        {
            switch (equip.so)
            {
                case WeaponSO:
                    SaveableData.weapons.Add(equip);
                    break;
                case NecklaceSO:
                    SaveableData.necklaces.Add(equip);
                    break;
                case GlovesSO:
                    SaveableData.gloves.Add(equip);
                    break;
                case HelmSO:
                    SaveableData.helms.Add(equip);
                    break;
                case VestSO:
                    SaveableData.vests.Add(equip);
                    break;
                case BootsSO:
                    SaveableData.boots.Add(equip);
                    break;
                default:
                    Log.EquipMiss();
                    return;
            }

            OnAdd(equip);
            AddToList(equip);
        }

        void RemoveSorted(EquipmentData equip)
        {
            switch (equip.so)
            {
                case WeaponSO:
                    if (SaveableData.weapons.Contains(equip))
                        SaveableData.weapons.Remove(equip);
                    break;
                case NecklaceSO:
                    if (SaveableData.necklaces.Contains(equip))
                        SaveableData.necklaces.Remove(equip);
                    break;
                case GlovesSO:
                    if (SaveableData.gloves.Contains(equip))
                        SaveableData.gloves.Remove(equip);
                    break;
                case HelmSO:
                    if (SaveableData.helms.Contains(equip))
                        SaveableData.helms.Remove(equip);
                    break;
                case VestSO:
                    if (SaveableData.vests.Contains(equip))
                        SaveableData.vests.Remove(equip);
                    break;
                case BootsSO:
                    if (SaveableData.boots.Contains(equip))
                        SaveableData.boots.Remove(equip);
                    break;
                default:
                    Log.EquipMiss();
                    return;
            }

            OnRemove(equip);
            RemoveFromList(equip);
        }

        #region All Equip List

        public IEnumerable<EquipmentData> Equipment
        {
            get
            {
                if (_init) return _all;
                CreateEquipList();
                return _all;
            }
        }

        List<EquipmentData> _all = new();

        void CreateEquipList()
        {
            if (_init) return;
            _all = new List<EquipmentData>();
            _all.AddRange(SaveableData.weapons);
            _all.AddRange(SaveableData.necklaces);
            _all.AddRange(SaveableData.gloves);
            _all.AddRange(SaveableData.helms);
            _all.AddRange(SaveableData.vests);
            _all.AddRange(SaveableData.boots);
            _init = true;
        }

        void AddToList(EquipmentData equip)
            => _all.Add(equip);

        void RemoveFromList(EquipmentData equip)
        {
            if (_all.Contains(equip))
                _all.Remove(equip);
            else
                Log.Warning("You are trying to remove not existing equip from inventory list");
        }

        #endregion
    }
}