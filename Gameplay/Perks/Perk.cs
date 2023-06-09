using Gameplay.Units.HeroComponents;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Perks
{
    public abstract class Perk : MonoBehaviour
    {
        public PerksMultipliers Multipliers { get; private set; }
        [SerializeField] [ReadOnly] int level;
        [SerializeField] int maxLevel;

        public int MaxLevel => maxLevel;
        public int Level => level;
        public bool IsMaxLevel => level >= maxLevel;

        public void SetMultipliers(PerksMultipliers mult)
        {
            Multipliers = mult;
        }

        public void LevelUp()
        {
            if (IsMaxLevel) return;
            level++;
            OnLevelUp();
        }

        protected virtual void OnLevelUp(){}
    }
}