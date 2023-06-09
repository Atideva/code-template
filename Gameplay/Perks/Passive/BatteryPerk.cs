using System.Collections.Generic;
using Gameplay.Perks.Passive.Content;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.CharacterStats;

namespace Gameplay.Perks.Passive
{
    public class BatteryPerk : PassivePerk
    {
        [Space(20)]
        [ListDrawerSettings(Expanded = true, HideRemoveButton = true, HideAddButton = true, DraggableItems = false)]
        [ValidateInput(nameof(EqualMaxLevel), "COUNT != " + nameof(MaxLevel))]
        [SerializeField] List<BatteryStats> stats = new() {new(), new(), new(), new(), new()};
     
        public BatteryStats Stats => Level > 0 && Level <= stats.Count ? stats[Level - 1] : null;
        bool EqualMaxLevel() => stats.Count == MaxLevel;
        CharacterMod _lastMod;

        protected override void OnLevelUp()
        {
            if (_lastMod != null)
                Multipliers.DurationMultiplier.RemoveModifier(_lastMod);

            var value = Stats.DurationBonus;
            _lastMod= new CharacterMod(value, StatModType.Percent);
          
            Multipliers.DurationMultiplier.AddModifier(_lastMod);
        }
    }
}
