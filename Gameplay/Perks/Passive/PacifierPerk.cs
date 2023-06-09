using System.Collections.Generic;
using DamageNumbersPro;
using Gameplay.Animations;
using Gameplay.Perks.Active.Content;
using Gameplay.Perks.Passive.Content;
using Gameplay.Units.UnitComponents;
using Meta.Facade;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Perks.Passive
{
    public class PacifierPerk : PassivePerk, IKnowHitpoints
    {
        [FoldoutGroup("Prefab Setup", Expanded = false)]
        [FoldoutGroup("Prefab Setup")] [SerializeField] bool useVfx;
        [FoldoutGroup("Prefab Setup")] [SerializeField] [ShowIf(nameof(useVfx))] AttachedVfx healVfx;
        [FoldoutGroup("Prefab Setup")] [SerializeField] bool useText;
        [FoldoutGroup("Prefab Setup")] [SerializeField] [ShowIf(nameof(useText))] DamageNumber healText;
        [FoldoutGroup("Prefab Setup")] [SerializeField] [ShowIf(nameof(useText))] Vector2 textOffset;
        [FoldoutGroup("Prefab Setup")] [SerializeField, PropertyOrder(100)] [ReadOnly] float cooldown;


        [Space(20)]
        [ListDrawerSettings(Expanded = true, HideRemoveButton = true, HideAddButton = true, DraggableItems = false)]
        [ValidateInput(nameof(EqualMaxLevel), "COUNT != " + nameof(MaxLevel))]
        [SerializeField] List<PacifierStats> stats = new() {new(), new(), new(), new(), new()};

        public PacifierStats Stats => Level > 0 && Level <= stats.Count ? stats[Level - 1] : null;
        bool EqualMaxLevel() => stats.Count == MaxLevel;
        bool LevelError => Level <= 0 && Level > stats.Count;
 
        void FixedUpdate()
        {
            if (LevelError) return;
            if (!Hitpoints) return;

            cooldown -= Time.fixedDeltaTime;
            if (cooldown > 0) return;

            cooldown = Stats.healIntervalSec;
            Heal();
        }

        void Heal()
        {
            var heal = Hitpoints.Max * Stats.HealPercent;

            if (!Hitpoints.IsFull)
            {
                Hitpoints.Heal(heal);

                if (useText)
                {
                    var pos = (Vector2) transform.position + textOffset;
                    healText.Spawn(pos, heal);
                }
                
                
                if (useVfx)
                    VFX.PlayAttached(healVfx, transform);
            }

        }

        public Hitpoints Hitpoints { get; private set; }
        public void SetHitpoints(Hitpoints hitpoints) => Hitpoints = hitpoints;
    }
}