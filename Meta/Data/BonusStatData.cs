using UnityEngine;

namespace Meta.Data
{
    [System.Serializable]
    public class BonusStatData
    {
        [Header("HP Bonus %")] public float hpBonus;
        [Header("ATK Bonus %")] public float attackBonus;
    }
}