using System.Collections.Generic;
using System.Linq;
using Gameplay.Units;
using Meta.Data;
using Meta.Enums;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.Odin;

namespace SO.UnitsSO
{
    [CreateAssetMenu(fileName = "New Hero", menuName = "Gameplay/Units/Hero")]
    public class HeroSO : UnitSO, ISerializeReferenceByAssetGuid
    {
        [LabelText("[CharacterView]"), Space(20), Required]
        [SerializeField] UnitModel characterModel;
        [SerializeField] int baseDamage=10;
        
        [Space(20)]
        [SerializeField] List<HeroPerkData> perksData = new();

        [Space(20)]
        [SerializeField] List<BonusStatData> bonusPerLevel = new();

        [Space(20)]
        [SerializeField] [HideLabel]PriceData buyPrice;

        public BonusStatData GetBonus(int lvl)
        {
            var id = lvl - 1;
            if (id < 0) id = 0;
            if (id >= bonusPerLevel.Count) id = bonusPerLevel.Count - 1;
            return bonusPerLevel[id];
        }

        public IReadOnlyList<HeroPerkData> PerksData => perksData;
        public UnitModel CharacterView => characterModel;

        public PriceData BuyPrice => buyPrice;

        public int BaseDamage => baseDamage;
    }
}