using GameManager;
using Gameplay.UI;
using Gameplay.Units.HeroComponents;
using Meta.Data;
using Sirenix.OdinInspector;
using SO.UnitsSO;
using UnityEngine;
using Utilities.CharacterStats;

namespace Gameplay.Units
{
    public class Hero : Unit
    {
        [Space(20)]
        [Header("Hero Components")]
        [SerializeField] [Required] HeroLevel heroLevel;
        [SerializeField] [Required] HeroPerks heroPerks;
        [SerializeField] [Required] HeroExperienceMagnet experienceMagnet;
        [SerializeField] [Required] TargetsScanner scanner;
        [SerializeField] [Required] HP_Bar hpBar;

        public HeroPerks Perks => heroPerks;
        public HeroLevel Level => heroLevel;

        public TargetsScanner Scanner => scanner;

        public HeroExperienceMagnet ExperienceMagnet => experienceMagnet;

        protected override void OnInit(UnitSO unit)
        {
            var levelTable = Game.Instance.Config.HeroLevelTable.Table;
            var expPerLvl = Game.Instance.Config.HeroLevelTable.ExpPerKill;
            var levelUI = GameplayUI.Instance.HeroLevel;

            heroLevel.Init(this, levelTable, expPerLvl, levelUI);
            experienceMagnet.Init(this);
            heroPerks.Init(this);
            hpBar.Init(Hitpoints);
        }

        public void SetBonuses(BonusStatData bonuses)
        {
            var hp = bonuses.hpBonus * 0.01f;
            var atk = bonuses.attackBonus * 0.01f;

            var maxHp = Hitpoints.MaxHitpoints;
            var h = new CharacterMod(hp, StatModType.Percent);
            maxHp.AddModifier(h);

            var d = new CharacterMod(atk, StatModType.Percent);
            Perks.Multipliers.DamageMultiplier.AddModifier(d);
        }
    }
}