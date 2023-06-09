using System.Collections.Generic;
using Gameplay.Perks.Active.Content;
using Gameplay.Perks.Passive.Content;
using Gameplay.Units.UnitComponents;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.CharacterStats;

namespace Gameplay.Perks.Passive
{
    public class HeartPerk : PassivePerk, IKnowHitpoints
    {
 
        [Space(20)]
        [ListDrawerSettings(Expanded = true, HideRemoveButton = true, HideAddButton = true, DraggableItems = false)]
        [ValidateInput(nameof(EqualMaxLevel), "COUNT != " + nameof(MaxLevel))]
        [SerializeField] List<HeartStats> stats = new() {new(), new(), new(), new(), new()};
        public HeartStats Stats => Level > 0 && Level <= stats.Count ? stats[Level - 1] : null;
        bool EqualMaxLevel() => stats.Count == MaxLevel;
        bool LevelError => Level <= 0 && Level > stats.Count;
        CharacterMod last;

        protected override void OnLevelUp()
        {
            if (!Hitpoints) return;

            var mult =  Hitpoints.MaxHitpoints;
            
            if (last != null)
                mult.RemoveModifier(last);

            var v = Stats.BonusHP;
            var m = new CharacterMod(v, StatModType.Percent);
            mult.AddModifier(m);
 
            last = m;
        }

        public Hitpoints Hitpoints { get; private set; }
        public void SetHitpoints(Hitpoints hitpoints) => Hitpoints = hitpoints;
    }
}
