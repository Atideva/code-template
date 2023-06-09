using System.Collections.Generic;
using Gameplay.Perks.Active.Content;
using Gameplay.Units.UnitComponents.Move;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.AudioSystem;
using Utilities.MonoCache.System;
using Utilities.Pools;
using static Utilities.Extensions.VectorExtensions;

namespace Gameplay.Perks.Active
{
    public class FlamethrowerPerk : ActivePerk, IKnowMovement
    {
        [FoldoutGroup("Prefab Setup")] [SerializeField] float dmgInterval = 0.3f;
        [FoldoutGroup("Prefab Setup")] [SerializeField] Transform container;
        [FoldoutGroup("Prefab Setup")] [SerializeField] Flamethrower prefab;
        [FoldoutGroup("Prefab Setup")] [SerializeField] FlamethrowerPool pool;
        [FoldoutGroup("Prefab Setup")] [SerializeField] GameObject sound;
        [Space(20)]
        [ListDrawerSettings(Expanded = true, HideRemoveButton = true, HideAddButton = true, DraggableItems = false)]
        [ValidateInput(nameof(EqualMaxLevel), "COUNT != " + nameof(MaxLevel))]
        [SerializeField] List<FlamethrowerStats> stats = new(){new(), new(), new(), new(), new()};

        bool EqualMaxLevel() => stats.Count == MaxLevel;
        public FlamethrowerStats Stats => Level > 0 && Level <= stats.Count ? stats[Level - 1] : null;
        bool LevelError => Level <= 0 && Level > stats.Count;
        public float DmgInterval => dmgInterval;
        public float Damage => Stats.dps / dmgInterval;
        public float LifeTime => Stats.lifeTime* Multipliers.Duration;
        float Cooldown => Stats.cooldown * Multipliers.Cooldown;
          
        void Awake()
        {
            pool.SetPrefab(prefab, container);
            StopSound();
        }

        void Update()
        {
            var angle = GetAngle(Move.Direction);
            container.transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        void PlaySound() => sound.Enable();
        void StopSound() => sound.Disable();
        
        void FixedUpdate()
        {
            if (LevelError) return;
            if (!Move) return;

            cooldown -= Time.fixedDeltaTime;
            if (cooldown > 0) return;

            cooldown = Cooldown;;
            Flame();
        }

        protected override void OnLevelUp()
        {
        }

        void Flame()
        {
            var z = 0;
            var step = 360 / Stats.count;

            for (int i = 0; i < Stats.count; i++)
            {
                var fire = pool.Get();
                fire.SetAngle(z);
                fire.SetPerk(this);
                fire.Enable();
                z += step;
            }
            
            PlaySound();
            Invoke(nameof(StopSound), LifeTime + prefab.FirstDamageDelay);
        }

        public MoveEngine Move { get; private set; }
        public void SetMovement(MoveEngine move) => Move = move;
    }
}