using System.Collections.Generic;
using DG.Tweening;
using Gameplay.Perks.Active.Content;
using Gameplay.Units.UnitComponents;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Utilities.MonoCache.System;

namespace Gameplay.Perks.Active
{
    public class BarrierPerk : PassivePerk, IKnowHitpoints
    {
        [FoldoutGroup("Prefab Setup")] [SerializeField] Transform container;
        [FoldoutGroup("Prefab Setup")] [SerializeField] float scaleTime;
        [FoldoutGroup("Prefab Setup")] [SerializeField] BarrierCollider barrierCollider;
        [FoldoutGroup("Prefab Setup")] [SerializeField] Canvas canvas;
        [FoldoutGroup("Prefab Setup")] [SerializeField] Slider slider;
        [FoldoutGroup("Prefab Setup")] [SerializeField, PropertyOrder(100)] [ReadOnly] float cooldown;
        [FoldoutGroup("Prefab Setup", Expanded = false)] [SerializeField]
        [NaughtyAttributes.Tag] public string targets = "Enemy";
        
        [Space(20)]
        [ListDrawerSettings(Expanded = true, HideRemoveButton = true, HideAddButton = true, DraggableItems = false)]
        [ValidateInput(nameof(EqualMaxLevel), "COUNT != " + nameof(MaxLevel))]
        [SerializeField] List<BarrierStats> stats = new() {new(), new(), new(), new(), new()};

        public BarrierStats Stats => Level > 0 && Level <= stats.Count ? stats[Level - 1] : null;
        bool LevelError => Level <= 0 && Level > stats.Count;

        float capacity;
        float MaxCapacity => Stats.capacity;
        bool IsShield;
        float outOfCombatTime;

        void Awake()
        {
            barrierCollider.SetPerk(this);
            barrierCollider.OnAbsorb += Absorb;
            canvas.worldCamera = Camera.main;
        }

        bool EqualMaxLevel() => stats.Count == MaxLevel;

        protected override void OnLevelUp()
        {
            capacity = MaxCapacity;
        }


        void FixedUpdate()
        {
            if (IsShield)
            {
                outOfCombatTime += Time.fixedDeltaTime;
                if (outOfCombatTime >= Stats.capacityRestoreDelay)
                {
                    if (capacity < MaxCapacity)
                        capacity += Stats.capacityRestorePerSec * Time.fixedDeltaTime;
                }

                RefreshUI();
                return;
            }

            if (LevelError) return;
            if (!Hitpoints) return;

            cooldown -= Time.fixedDeltaTime;
            if (cooldown > 0) return;

            CreateShield();
        }


        void CreateShield()
        {
            IsShield = true;
            capacity = MaxCapacity;
            barrierCollider.Enable();

            container.localScale = Vector3.zero;
            container
                .DOScale(1.2f, scaleTime * 0.8f)
                .OnComplete(()
                    => container.DOScale(1, scaleTime * 0.2f));

            Hitpoints.SetImmune(true);
            Hitpoints.OnImmuneDamage += Absorb;

            canvas.enabled = true;
            RefreshUI();
        }

        void Absorb(float damage)
        {
            outOfCombatTime = 0;
            capacity -= damage;

            if (capacity <= 0)
            {
                DestroyShield();
            }

            RefreshUI();
        }

        void DestroyShield()
        {
            IsShield = false;
            canvas.enabled = false;

            Hitpoints.SetImmune(false);
            Hitpoints.OnImmuneDamage -= Absorb;

            barrierCollider.Disable();
            container
                .DOScale(0, scaleTime * 0.5f)
                .OnComplete(()
                    => container.Disable());

            cooldown = Stats.cooldown * Multipliers.Cooldown;
        }

        void RefreshUI()
            => slider.value = capacity / MaxCapacity;

        public Hitpoints Hitpoints { get; private set; }
        public void SetHitpoints(Hitpoints hitpoints) => Hitpoints = hitpoints;
    }
}