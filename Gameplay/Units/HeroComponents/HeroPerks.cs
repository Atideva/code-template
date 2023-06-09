using System.Collections.Generic;
using System.Linq;
using Meta.Data;
using Sirenix.OdinInspector;
using SO.PerksSO;
using UnityEngine;

namespace Gameplay.Units.HeroComponents
{
    public class HeroPerks : MonoBehaviour
    {
        [SerializeField] HeroPerksSelect select;
        [SerializeField] HeroPerksContainer perks;

        [Header("Settings")]
        [SerializeField] int activeSlots = 6;
        [SerializeField] int passiveSlots = 6;
        [SerializeField] int selectCount = 3;

        [Space(10)]
        [SerializeField] [ReadOnly] PerksMultipliers multipliers = new();
        [SerializeField] [ReadOnly] HeroPerksData perksData = new();

        #region Access

        public IReadOnlyList<PerkData> Actives => perksData.activeOwned;
        public IReadOnlyList<PerkData> Passive => perksData.passiveOwned;

        public int ActiveSlots => activeSlots;

        public int PassiveSlots => passiveSlots;

        public int SelectCount => selectCount;

        public HeroPerksSelect Select => select;

        public HeroPerksData Data => perksData;

        public PerksMultipliers Multipliers => multipliers;

        public List<PerkData> ActiveOwned => perksData.activeOwned;

        public List<PerkData> PassiveOwned => perksData.passiveOwned;

        public List<ActivePerkSO> ActiveList => perksData.activeList;

        public List<PassivePerkSO> PassiveList => perksData.passiveList;

        #endregion

        public void Init(Hero hero)
        {
            perks.Init(hero);
            multipliers.Reset();
        }

        public void Set(
            IEnumerable<ActivePerkSO> active, IEnumerable<PassivePerkSO> passive,
            IEnumerable<ActivePerkSO> startingPerks)
        {
            perksData = new HeroPerksData();
            select.Init(this);
            perksData.activeList = active.ToList();
            perksData.passiveList = passive.ToList();
            foreach (var so in startingPerks)
            {
                perksData.activeList.Add(so);
                Add(new PerkData {so = so});
            }
        }

        public void Add(PerkData data)
        {
            if (!data.IsOwned)
            {
                perksData.Add(data);
                perks.Create(data);
            }

            if (!data.perk) return;
            data.perk.LevelUp();
        }
    }
}