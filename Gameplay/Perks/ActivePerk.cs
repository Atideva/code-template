using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.CharacterStats;

namespace Gameplay.Perks
{
    public abstract class ActivePerk : Perk
    {
        [FoldoutGroup("Prefab Setup", Expanded = false)] [SerializeField]
        [NaughtyAttributes.Tag] protected string targets = "Enemy";
        [FoldoutGroup("Prefab Setup")] [SerializeField, PropertyOrder(100)] [ReadOnly] protected float cooldown;
        public string Targets => targets;
    }
}