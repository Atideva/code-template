using System.Collections.Generic;
using System.Linq;
using Meta.Data;
using SO.PerksSO;
using UnityEngine;

namespace Gameplay.Units.HeroComponents
{
    public class HeroPerksSelect : MonoBehaviour
    {
        HeroPerks _perks;
        public void Init(HeroPerks perks) => _perks = perks;

        #region Accessors

        public List<PerkData> ActiveOwned => _perks.ActiveOwned;
        public List<PerkData> PassiveOwned => _perks.PassiveOwned;
        public List<ActivePerkSO> ActiveList => _perks.ActiveList;
        public List<PassivePerkSO> PassiveList => _perks.PassiveList;
        int ActiveMax => _perks.ActiveSlots;
        int PassiveMax => _perks.PassiveSlots;
        int SelectCount => _perks.SelectCount;

        #endregion

        #region Bools
        public bool AllMaxed => ActiveMaxed && PassiveMaxed;
        public bool NoFreeSlots => !FreeActiveSlots && !FreePassiveSlots;

        bool FreeActiveSlots => ActiveOwned.Count < ActiveMax;
        bool FreePassiveSlots => PassiveOwned.Count < PassiveMax;

        bool ActiveMaxed => ActiveOwned.All(p => p.IsMaxLevel);
        bool PassiveMaxed => PassiveOwned.All(p => p.IsMaxLevel);

        bool ActiveToUpgrade
            => ActiveOwned.Count > 0 && !ActiveMaxed;

        bool PassiveUpgrade
            => PassiveOwned.Count > 0 && !PassiveMaxed;

        bool NewPassiveAvailable
            => FreePassiveSlots && PassiveList.Count > 0;

        #endregion

        public List<PerkData> GetRandomPerks()
        {
            var activeList = ActiveList.ToList();
            var passiveList = PassiveList.ToList();

            var list = new List<PerkData>();
            if (AllMaxed && NoFreeSlots) return list;

            if (ActiveToUpgrade)
            {
                var data = GetUpgradable(ActiveOwned);
                Add(data, list);
            }
            else if (activeList.Count > 0 && FreeActiveSlots)
            {
                AddRandom(activeList, list);
            }

            if (PassiveUpgrade && Random.value > 0.5f)
            {
                var data = GetUpgradable(PassiveOwned);
                Add(data, list);
            }
            else if (NewPassiveAvailable)
            {
                var data = NewPassive();
                Add(data, list);
            }

            for (var i = 0; i < 3; i++)
            {
                if (list.Count >= 3) break;
                if (activeList.Count > 0 && FreeActiveSlots)
                {
                    AddRandom(activeList, list);
                }
                else if (passiveList.Count > 0 && FreePassiveSlots)
                {
                    AddRandom(passiveList, list);
                }
            }


            return list;
        }
        
        PerkData GetUpgradable(List<PerkData> data)
        {
            var list = UpgradableOnly(data);
            return list[Random.Range(0, list.Count)];
        }
    

        PerkData NewPassive()
        {
            PassivePerkSO so = null;
            var list = PassiveList;
            for (int i = 0; i < 100; i++)
            {
                var r = Random.Range(0, list.Count);
                if (PassiveOwned.Exists(d => d.so == list[r])) continue;
                so = list[r];
                break;
            }
            return new PerkData {so = so};
        }

        static List<PerkData> UpgradableOnly(IEnumerable<PerkData> perks)
            => perks.Where(data => !data.IsMaxLevel).ToList();


        void AddRandom(List<ActivePerkSO> perks, List<PerkData> show)
        {
            ActivePerkSO so = null;
            for (int i = 0; i < 100; i++)
            {
                var r = Random.Range(0, perks.Count);
                var perk = perks[r];
                if (ActiveOwned.Exists(d => d.so == perk)) continue;
                so = perk;
                break;
            }

            if (!so) return;
            var data = new PerkData {so = so};
            Add(data, show);
            perks.Remove(so);
        }

        void AddRandom(List<PassivePerkSO> perks, List<PerkData> list)
        {
            PassivePerkSO so = null;
            for (int i = 0; i < 100; i++)
            {
                var r = Random.Range(0, perks.Count);
                var perk = perks[r];
                if (ActiveOwned.Exists(d => d.so == perk)) continue;
                so = perk;
                break;
            }

            if (!so) return;
            var data = new PerkData {so = so};
            Add(data, list);
            perks.Remove(so);
        }

        void Add(PerkData data, List<PerkData> list)
        {
            if (list.Exists(p => p.so == data.so)) return;
            list.Add(data);
        }
    }
}