using Sirenix.OdinInspector;
using Utilities.CharacterStats;

// ReSharper disable InconsistentNaming

namespace Gameplay.Units.HeroComponents
{
    [System.Serializable]
    public class PerksMultipliers
    {
        [ReadOnly] public CharacterStat RadiusMultiplier;
        [ReadOnly] public CharacterStat CooldownMultiplier;
        [ReadOnly] public CharacterStat DamageMultiplier;
        [ReadOnly] public CharacterStat DurationMultiplier;
        [ReadOnly] public CharacterStat BonusCount;

        public float Radius => RadiusMultiplier.Value;
        public float Cooldown => CooldownMultiplier.Value;
        public float Damage => DamageMultiplier.Value;
        public float Duration => DurationMultiplier.Value;
        public int CountAdd => (int) BonusCount.Value;

        public void Reset()
        {
            RadiusMultiplier.baseValue = 1;
            CooldownMultiplier.baseValue = 1;
            DamageMultiplier.baseValue = 1;
            DurationMultiplier.baseValue = 1;
            BonusCount.baseValue = 0;
        }
    }
}