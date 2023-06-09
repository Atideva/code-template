using System.Collections.Generic;
using Gameplay.Perks.Active.Content;
using Gameplay.Perks.Batman.Content;
using Gameplay.Units.HeroComponents;
using Meta.Facade;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.AudioSystem;
using Utilities.Extensions;
using Utilities.Pools;

namespace Gameplay.Perks.Batman
{
    public class BoomerangPerk : ActivePerk, IKnowTargetScanner
    {
        [FoldoutGroup("Prefab Setup")] [SerializeField] Boomerang prefab;
        [FoldoutGroup("Prefab Setup")] [SerializeField] BoomerangPool pool;
        [FoldoutGroup("Prefab Setup")] [SerializeField] float rotateSpeed = 20;
        [FoldoutGroup("Prefab Setup")] [SerializeField] float flightSpeed = 20;
        [FoldoutGroup("Prefab Setup")] [SerializeField] float parabolaOffset = 4;
        [FoldoutGroup("Prefab Setup")] [SerializeField] SoundSO sound;
        
        [Space(20)]
        [ListDrawerSettings(Expanded = true, HideRemoveButton = true, HideAddButton = true, DraggableItems = false)]
        [ValidateInput(nameof(EqualMaxLevel), "COUNT != " + nameof(MaxLevel))]
        [SerializeField] List<BoomerangStats> stats = new() {new(), new(), new(), new(), new()};

        public BoomerangStats Stats => Level > 0 && Level <= stats.Count ? stats[Level - 1] : null;
        public float ParabolaOffset => parabolaOffset;
        public float FlightSpeed => flightSpeed;

        public float RotateSpeed => rotateSpeed;

        bool EqualMaxLevel() => stats.Count == MaxLevel;
        bool LevelError => Level <= 0 && Level > stats.Count;
        float Cooldown => Stats.cooldown * Multipliers.Cooldown;
        
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
            Scanner.OnScan -= ThrowBoomerang;
            Scanner.OnScan += ThrowBoomerang;
        }

        void ThrowBoomerang()
        {
            Scanner.OnScan -= ThrowBoomerang;
            if (Scanner.NoTargets) return;
            
            cooldown = Cooldown;
            
            var pos = transform.position;
            for (int i = 0; i < Stats.count; i++)
            {
                var target = Scanner.GetRandomTarget();

                var boomerang = pool.Get();
                boomerang.transform.position = pos;

                var dir = (target.position - pos).normalized;
                var dist = Vector2.Distance(target.position, pos);
                var angle = VectorExtensions.GetAngle(dir);
                boomerang.Activate(this, angle, dist);
            }
            
            Audio.Play(sound);
        }

        public TargetsScanner Scanner { get; private set; }

        public SoundSO Sound => sound;

        public void SetScanner(TargetsScanner scanner) => Scanner = scanner;
    }
}