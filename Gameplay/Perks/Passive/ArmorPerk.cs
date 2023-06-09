using System.Collections.Generic;
using Gameplay.Perks.Active.Content;
using Gameplay.Perks.Passive.Content;
using Gameplay.Units.UnitComponents;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.CharacterStats;

namespace Gameplay.Perks.Passive
{
    public class ArmorPerk : PassivePerk, IKnowHitpoints
    {
        [Space(20)]
        [ListDrawerSettings(Expanded = true, HideRemoveButton = true, HideAddButton = true, DraggableItems = false)]
        [ValidateInput(nameof(EqualMaxLevel), "COUNT != " + nameof(MaxLevel))]
        [SerializeField] List<ArmorStats> stats = new() {new(), new(), new(), new(), new()};
        public ArmorStats Stats => Level > 0 && Level <= stats.Count ? stats[Level - 1] : null;
        bool EqualMaxLevel() => stats.Count == MaxLevel;
        CharacterMod _lastMod;

        protected override void OnLevelUp()
        {
            if (!Hitpoints) return;

            var mult = Hitpoints.DamageMultiplier;
            
            if (_lastMod != null)
                mult.RemoveModifier(_lastMod);

            var value = Stats.DamageReduction;
            _lastMod= new CharacterMod(-value, StatModType.Percent);
          
            mult.AddModifier(_lastMod);
        }

        public Hitpoints Hitpoints { get; private set; }
        public void SetHitpoints(Hitpoints hitpoints) => Hitpoints = hitpoints;
    }
}