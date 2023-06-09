using System.Collections.Generic;
using Gameplay.Perks.Active.Content;
using Gameplay.Perks.Passive.Content;
using Gameplay.Units.HeroComponents;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.CharacterStats;

namespace Gameplay.Perks.Passive
{
    public class MagnetPerk : PassivePerk, IKnowExpMagnet
    {
        [Space(20)]
        [ListDrawerSettings(Expanded = true, HideRemoveButton = true, HideAddButton = true, DraggableItems = false)]
        [ValidateInput(nameof(EqualMaxLevel), "COUNT != " + nameof(MaxLevel))]
        [SerializeField] List<MagnetStats> stats = new() {new(), new(), new(), new(), new()};
     
        public MagnetStats Stats => Level > 0 && Level <= stats.Count ? stats[Level - 1] : null;
        bool EqualMaxLevel() => stats.Count == MaxLevel;
        bool LevelError => Level <= 0 && Level > stats.Count;
        CharacterMod _lastMod;

        protected override void OnLevelUp()
        {
            if (!ExpMagnet) return;

            var mult = ExpMagnet.RadiusMultiplier;
            
            if (_lastMod != null)
                mult.RemoveModifier(_lastMod);

            _lastMod = new CharacterMod(Stats.RadiusBonus, StatModType.Percent);
            mult.AddModifier(_lastMod);
        }

        public HeroExperienceMagnet ExpMagnet { get; private set; }
        public void SetExperienceMagnet(HeroExperienceMagnet expMagnet) => ExpMagnet = expMagnet;
    }
}