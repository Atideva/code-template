using System.Collections;
using System.Collections.Generic;
using Gameplay.Perks.Active.Content;
using Gameplay.Units.HeroComponents;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.AudioSystem;
using Utilities.Pools;
using static Utilities.Extensions.VectorExtensions;

namespace Gameplay.Perks.Active
{
    public class RpgPerk : ActivePerk, IKnowTargetScanner
    {
        [FoldoutGroup("Prefab Setup")] [SerializeField] RpgRocket prefab;
        [FoldoutGroup("Prefab Setup")] [SerializeField] RpgRocketPool pool;
        [FoldoutGroup("Prefab Setup")] [SerializeField] SoundSO sound;
        
        [Space(20)]
        [ListDrawerSettings(Expanded = true, HideRemoveButton = true, HideAddButton = true, DraggableItems = false)]
        [ValidateInput(nameof(EqualMaxLevel), "COUNT != " + nameof(MaxLevel))]
        [SerializeField] List<RpgStats> stats = new() {new(), new(), new(), new(), new()};

        public RpgStats Stats => Level > 0 && Level <= stats.Count ? stats[Level - 1] : null;

        bool EqualMaxLevel() => stats.Count == MaxLevel;
        bool LevelError => Level <= 0 && Level > stats.Count;

        public float Damage => Stats.damage;
        float Cooldown => Stats.cooldown * Multipliers.Cooldown;
        float Interval => Stats.countInterval * Multipliers.Duration;

        void Awake()
        {
            pool.SetPrefab(prefab);
        }

        void FixedUpdate()
        {
            if (LevelError) return;
            if (!Scanner) return;

            cooldown -= Time.fixedDeltaTime;
            if (cooldown > 0) return;

            Scanner.Scan();
            Scanner.OnScan -= ScanTargets;
            Scanner.OnScan += ScanTargets;
        }

        void ScanTargets()
        {
            Scanner.OnScan -= ScanTargets;
            if (Scanner.NoTargets) return;

            cooldown = Cooldown;
            StartCoroutine(RocketBarrage());
        }


        IEnumerator RocketBarrage()
        {
            List<Transform> scanTargets = new();
            for (int i = 0; i < Stats.count; i++)
                scanTargets.Add(Scanner.GetRandomTarget());

            for (int i = 0; i < Stats.count; i++)
            {
                var pos = transform.position;

                var target = scanTargets[i];
                if (!target) yield break;

                var dir = (target.position - pos).normalized;
                var dist = Vector2.Distance(target.position, pos);
                var angle = GetAngle(dir);

                var rocket = pool.Get();
                rocket.transform.position = pos;

                rocket.Set(this);
                rocket.SetAngle(angle);
                rocket.SetFlyDistance(dist);
                rocket.SetExplosionRadius(Stats.explosionRadius);
                rocket.Launch();

                yield return new WaitForSeconds(Interval);
            }
        }

        public TargetsScanner Scanner { get; private set; }

        public SoundSO Sound => sound;

        public void SetScanner(TargetsScanner scanner) => Scanner = scanner;
    }
}