using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Perks.Active.Content;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.MonoCache.System;

namespace Gameplay.Perks.Active
{
    public class ElectricBallsPerk : ActivePerk
    {
        [FoldoutGroup("Prefab Setup")] [SerializeField] float rotateSpeed = 50;
        [FoldoutGroup("Prefab Setup")] [SerializeField] float ballAppearTime = 0.3f;
        [FoldoutGroup("Prefab Setup")] [SerializeField] float dmgInterval = 0.3f;

        [FoldoutGroup("Prefab Setup")] [SerializeField] Transform container;
        [FoldoutGroup("Prefab Setup")] [SerializeField] GameObject sound;
        [FoldoutGroup("Prefab Setup")] [SerializeField] ElectricBall prefab;

        [FoldoutGroup("Prefab Setup")] [SerializeField] [ReadOnly] float lifeTime;
        [FoldoutGroup("Prefab Setup")] [SerializeField] [ReadOnly] bool active;
        [FoldoutGroup("Prefab Setup")] [SerializeField] [ReadOnly]
        [ListDrawerSettings(Expanded = true)] List<ElectricBall> spawnedBalls = new();

        [Space(20)]
        [ListDrawerSettings(Expanded = true, HideRemoveButton = true, HideAddButton = true, DraggableItems = false)]
        [ValidateInput(nameof(EqualMaxLevel), "COUNT != " + nameof(MaxLevel))]
        [SerializeField] List<ElectricBallsStats> stats = new() {new(), new(), new(), new(), new()};
        bool EqualMaxLevel() => stats.Count == MaxLevel;


        public ElectricBallsStats Stats => Level > 0 && Level <= stats.Count ? stats[Level - 1] : null;
        bool LevelError => Level <= 0 && Level > stats.Count;
        public float Duration => Stats.lifeTime * Multipliers.Duration;
        float Cooldown => Stats.cooldown * Multipliers.Cooldown;

        void Awake()
        {
            DisableBalls();
        }

        protected override void OnLevelUp()
        {
            if (LevelError) return;

            RemoveExtraBalls(Stats.ballCount);
            SpawnNewBalls(Stats.ballCount);

            foreach (var ball in spawnedBalls.Where(ball => !ball.IsInit))
                ball.Init(this, dmgInterval);

            foreach (var ball in spawnedBalls)
                ball.SetBulletBlock(Stats.ballBlockBullets);
        }

        void Update()
        {
            if (active)
            {
                var z = container.transform.rotation.eulerAngles.z;
                z += rotateSpeed * Time.deltaTime;
                container.transform.rotation = Quaternion.Euler(0, 0, z);
            }
        }

        void FixedUpdate()
        {
            if (LevelError) return;

            if (active)
            {
                if (lifeTime > 0)
                    lifeTime -= Time.fixedDeltaTime;
                else
                    DisableBalls();
            }

            cooldown -= Time.fixedDeltaTime;
            if (cooldown > 0) return;
            if (active) return;

            cooldown = Cooldown;
            EnableBalls();
        }

        void EnableBalls()
        {
            active = true;
            lifeTime = Duration;

            var step = 360 / spawnedBalls.Count;
            var z = 0f;

            foreach (var ball in spawnedBalls)
            {
                z += step;
                ball.transform.rotation = Quaternion.Euler(0, 0, z);
                ball.Enable(ballAppearTime);
                ball.SetLifeTime(Duration, ballAppearTime);
            }
            
            sound.Enable();
        }

        void DisableBalls()
        {
            active = false;
            sound.Disable();
        }

        void SpawnNewBalls(int newCount)
        {
            for (int i = spawnedBalls.Count; i < newCount; i++)
            {
                var ball = Instantiate(prefab, container);
                ball.Init(this, dmgInterval);
                spawnedBalls.Add(ball);
            }
        }

        void RemoveExtraBalls(int newCount)
        {
            var destroy = new List<ElectricBall>();
            if (newCount < spawnedBalls.Count)
            {
                for (var i = newCount; i < spawnedBalls.Count; i++)
                    destroy.Add(spawnedBalls[i]);
            }

            foreach (var ball in destroy)
            {
                spawnedBalls.Remove(ball);
                Destroy(ball.gameObject);
            }
        }
    }
}