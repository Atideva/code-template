using System.Collections.Generic;
using Gameplay.Perks.Passive.Content;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.CharacterStats;

namespace Gameplay.Perks.Passive
{
    public class SandWatchPerk : PassivePerk
    {
        [Space(20)]
        [ListDrawerSettings(Expanded = true, HideRemoveButton = true, HideAddButton = true, DraggableItems = false)]
        [ValidateInput(nameof(EqualMaxLevel), "COUNT != " + nameof(MaxLevel))]
        [SerializeField] List<SandWatchStats> stats = new() {new(), new(), new(), new(), new()};
       
        public SandWatchStats Stats => Level > 0 && Level <= stats.Count ? stats[Level - 1] : null;
        bool EqualMaxLevel() => stats.Count == MaxLevel;
        CharacterMod _lastMod;

        protected override void OnLevelUp()
        {
            var mult = Multipliers.CooldownMultiplier;
            
            if (_lastMod != null)
                mult.RemoveModifier(_lastMod);

            var value = Stats.CooldownReduction;
            _lastMod= new CharacterMod(-value, StatModType.Percent);
          
            mult.AddModifier(_lastMod);
        }
 
    }
}
