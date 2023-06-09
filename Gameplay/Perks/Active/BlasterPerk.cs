using System.Collections.Generic;
using Gameplay.Perks.Active.Content;
using Gameplay.Units.HeroComponents;
using Gameplay.Units.UnitWeapons;
using Meta.Facade;
using NaughtyAttributes;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.AudioSystem;
using Utilities.Extensions;


namespace Gameplay.Perks.Active
{
    public class BlasterPerk : ActivePerk, IKnowTargetScanner
    {
        /*
        
        [FoldoutGroup("Prefab Setup")] [SerializeField] Blaster prefab;
        [FoldoutGroup("Prefab Setup")] [SerializeField] BlasterPool pool;
        [FoldoutGroup("Prefab Setup")] [SerializeField] [ReadOnly] List<Blaster> blasters = new();

        [Space(20)]
        [ListDrawerSettings(Expanded = true, HideRemoveButton = true, HideAddButton = true, DraggableItems = false)]
        [ValidateInput(nameof(EqualMaxLevel), "COUNT != " + nameof(MaxLevel))]
        [SerializeField] List<BlasterStats> stats = new() {new(), new(), new(), new(), new()};

        public BlasterStats Stats => Level > 0 && Level <= stats.Count ? stats[Level - 1] : null;
        bool EqualMaxLevel() => stats.Count == MaxLevel;
        bool LevelError => Level <= 0 && Level > stats.Count;


        void Awake()
        {
            pool.SetPrefab(prefab);
        }


        protected override void OnLevelUp()
        {
            if (blasters.Count < stats.Count)
            {
                var newBlaster = pool.Get();
                newBlaster.SetPerk(this);
                newBlaster.Disable();
                blasters.Add(newBlaster);
            }
        }

        void FixedUpdate()
        {
            if (LevelError) return;
            if (!Scanner) return;

            if (blasters.All(b => b.HasTarget)) return;

            Scanner.Scan();
            Scanner.OnScan -= Blast;
            Scanner.OnScan += Blast;
        }
      //  float Cooldown => Stats.cooldown * Multipliers.Cooldown;
        void Blast()
        {
            Scanner.OnScan -= Blast;
            if (Scanner.NoTargets) return;
            
            foreach (var blaster in blasters)
            {
                if (blaster.HasTarget) continue;
                var target = Scanner.GetClosestTarget();
                var unit = Scene.Instance.Units.Get(targets, target);
                if(unit)
                 blaster.Activate(unit);
            }
        }

        public TargetsScanner Scanner { get; private set; }
        public void SetScanner(TargetsScanner scanner) => Scanner = scanner;
        
        */

        [FoldoutGroup("Prefab Setup")] [SerializeField] Bullet bulletPrefab;
        [FoldoutGroup("Prefab Setup")] [SerializeField] [Tag] string bulletTeam;
        [FoldoutGroup("Prefab Setup")] [SerializeField] [Layer] string bulletsLayer;
        [FoldoutGroup("Prefab Setup")] [SerializeField] float bulletSpeed;
        [FoldoutGroup("Prefab Setup")] [SerializeField] float burstInterval = 0.1f;
        [FoldoutGroup("Prefab Setup")] [SerializeField] SoundSO sound;
        [FoldoutGroup("Prefab Setup")] [SerializeField] List<Color> bulletColors = new();

        [Space(20)]
        [ListDrawerSettings(Expanded = true, HideRemoveButton = true, HideAddButton = true, DraggableItems = false)]
        [Sirenix.OdinInspector.ValidateInput(nameof(EqualMaxLevel), "COUNT != " + nameof(MaxLevel))]
        [SerializeField] List<BlasterStats> stats = new() {new(), new(), new(), new(), new()};

        public BlasterStats Stats => Level > 0 && Level <= stats.Count ? stats[Level - 1] : null;
        bool EqualMaxLevel() => stats.Count == MaxLevel;
        bool LevelError => Level <= 0 && Level > stats.Count;


        Bullet Spawn() => Scene.Instance.Bullets.Spawn(bulletPrefab, bulletTeam, targets, bulletsLayer);

        public float Damage => Stats.damage * Multipliers.Damage;

        public float Cooldown => Stats.cooldown * Multipliers.Cooldown;
        public int Count => Stats.count + Multipliers.CountAdd;

        Color RandomColor => bulletColors[Random.Range(0, bulletColors.Count)];
        
        float _burstTimer;
        int _burstCount;
        bool _oneMoreShoot;
        bool isScaning;
        
        void FixedUpdate()
        {
            if (LevelError) return;
            if (!Scanner) return;

            cooldown -= Time.fixedDeltaTime;
            if (cooldown > 0) return;

            if (!_oneMoreShoot)
            {
                _oneMoreShoot = true;
                _burstCount = Count;
            }
            else
            {
                if (_burstTimer > 0)
                {
                    _burstTimer -= Time.fixedDeltaTime;
                }
                else
                {
                    if (!isScaning)
                    {
                        Scanner.Scan();
                        Scanner.OnScan -= Shoot;
                        Scanner.OnScan += Shoot;
                        isScaning = true;
                    }
                }
            }
        }

    

        void Shoot()
        {
            Scanner.OnScan -= Shoot;
            isScaning = false;

            if (Scanner.NoTargets) return;

            var target = Scanner.GetRandomTarget();
            var dir = (target.position - transform.position).normalized;
            var angle = VectorExtensions.GetAngle(dir);

            var bullet = Spawn();
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            bullet.SetDamage(Damage);
            bullet.SetMoveSpeed(bulletSpeed);
            bullet.SetLifeTime(25 / bulletSpeed);
            bullet.SetColor(RandomColor);

            Audio.Play(sound);

            _burstCount--;
            _burstTimer = burstInterval;
            if (_burstCount <= 0)
            {
                _oneMoreShoot = false;
                cooldown = Cooldown;
            }
        }


        public TargetsScanner Scanner { get; private set; }
        public void SetScanner(TargetsScanner scanner) => Scanner = scanner;
    }
}