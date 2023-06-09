using System.Collections.Generic;
using System.Linq;
using Meta.Data;
using Meta.Facade;
using Meta.Save.SaveSystem;
using SO.ConfigsSO;
using SO.PerksSO;
using UnityEngine;

namespace Meta.Save.Storage
{
    public class UnlockedPerksStorage : Saveable<UnlockedPerksData>
    {
        public void Init(FirstLaunchSO config)
        {
            if (PlayerPrefs.HasKey(SaveKey))
            {
                Load();
            }
            else
            {
                SaveableData = config.UnlockedPerks;
                Save();
            }
        }

        public void Add(PerkSO so)
        {
            AddNew(so);
            Save();
        }

        void Remove(PerkSO so)
        {
            //TODO: NOT IMPLEMENTED
            //RemoveExist();
            //RemoveFromList(so);
            Save();
        }

        void AddNew(PerkSO so)
        {
            switch (so)
            {
                case ActivePerkSO a:
                    if (SaveableData.activePerks.All(d => d.so != a))
                    {
                        SaveableData.activePerks.Add(new ActivePerkData {so = a});
                        AddToList(a);
                    }

                    break;
                case PassivePerkSO p:
                    if (SaveableData.passivePerks.All(d => d.so != p))
                    {
                        SaveableData.passivePerks.Add(new PassivePerkData {so = p});
                        AddToList(p);
                    }

                    break;
                default:
                    Log.PerkMiss();
                    break;
            }
        }


        #region All Equip List

        bool _init;
        public IReadOnlyList<ActivePerkSO> ActivePerks => SaveableData.activePerks.Select(data => data.so).ToList();
        public IReadOnlyList<PassivePerkSO> PassivePerks => SaveableData.passivePerks.Select(data => data.so).ToList();

        public IEnumerable<PerkSO> Perks
        {
            get
            {
                if (_init) return _all;
                CreateEquipList();
                return _all;
            }
        }

        List<PerkSO> _all = new();

        void CreateEquipList()
        {
            if (_init) return;

            _all = new List<PerkSO>();

            foreach (var data in SaveableData.activePerks)
                _all.Add(data.so);
            foreach (var data in SaveableData.passivePerks)
                _all.Add(data.so);

            _init = true;
        }

        void AddToList(PerkSO so)
            => _all.Add(so);

        void RemoveFromList(PerkSO so)
        {
            if (_all.Contains(so))
                _all.Remove(so);
            else
                Log.Warning("You are trying to remove not existing perk from list");
        }

        #endregion
    }
}