using Gameplay.Perks;
using SO.PerksSO;
using UnityEngine;

namespace Meta.Data
{
    [System.Serializable]
    public class PerkData
    {
        public PerkSO so;
        public Perk perk;
        public bool IsOwned=> perk;
        public int Level=> perk? perk.Level: 0;
        public bool IsMaxLevel => Level >= so.MaxLevel;
    }
}