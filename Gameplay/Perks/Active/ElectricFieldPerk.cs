using System.Collections.Generic;
using Gameplay.Perks.Active.Content;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.MonoCache.System;

namespace Gameplay.Perks.Active
{
    public class ElectricFieldPerk : ActivePerk
    {
        [FoldoutGroup("Prefab Setup")] [SerializeField] float dmgInterval = 0.3f;
        [FoldoutGroup("Prefab Setup")] [SerializeField] Transform scaleContainer;
        [FoldoutGroup("Prefab Setup")] [SerializeField] Transform lightningVfx;
        [FoldoutGroup("Prefab Setup")] [SerializeField] Transform areaVfx;
        [FoldoutGroup("Prefab Setup")] [SerializeField] Transform circleVfx;
        [FoldoutGroup("Prefab Setup")] [SerializeField] [ReadOnly] float lifeTime;
        [FoldoutGroup("Prefab Setup")] [SerializeField] [ReadOnly] bool active;
        [Space(20)]
        [ListDrawerSettings(Expanded = true, HideRemoveButton = true, HideAddButton = true, DraggableItems = false)]
        [ValidateInput(nameof(EqualMaxLevel), "COUNT != " + nameof(MaxLevel))]
        [SerializeField] List<ElectricFieldStats> stats = new() {new(), new(), new(), new(), new()};
        public ElectricFieldStats Stats => Level > 0 && Level <= stats.Count ? stats[Level - 1] : null;
        bool LevelError => Level <= 0 && Level > stats.Count;
        bool EqualMaxLevel() => stats.Count == MaxLevel;
        public float Dps => stats[Level - 1].dps;
        public float Interval => dmgInterval;
        float Cooldown => Stats.cooldown * Multipliers.Cooldown;
        public float Duration => Stats.lifeTime * Multipliers.Duration;

        void FixedUpdate()
        {
            if (LevelError) return;

            if (active)
            {
                if (lifeTime > 0)
                    lifeTime -= Time.fixedDeltaTime;
                else
                    Deactivate();
            }

            cooldown -= Time.fixedDeltaTime;
            if (cooldown > 0) return;

            if (!active)
                Activate();
        }

        void Deactivate()
        {
            active = false;
            scaleContainer.Disable();
            cooldown = Cooldown;
        }

        void Activate()
        {
            active = true;
            lifeTime = Duration;

            scaleContainer.Enable();
            lightningVfx.Enable();
            areaVfx.Disable();
            circleVfx.Disable();

            Invoke(nameof(EnableAreaVfx), 0.0f);
            Invoke(nameof(EnableCircleVfx), 1.0f);
            Invoke(nameof(DisableCircleVfx), lifeTime - 0.5f);
        }

        void EnableAreaVfx() => areaVfx.Enable();
        void EnableCircleVfx() => circleVfx.Enable();
        void DisableCircleVfx() => circleVfx.Disable();

        protected override void OnLevelUp()
        {
            if (Level > stats.Count) return;

            var size = stats[Level - 1].radius;
            scaleContainer.localScale = new Vector3(size, size, size);
        }
    }
}