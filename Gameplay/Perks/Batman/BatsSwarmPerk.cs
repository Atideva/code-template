using System.Collections.Generic;
using Gameplay.Perks.Active.Content;
using Gameplay.Units.HeroComponents;
using Meta.Facade;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.AudioSystem;
using Utilities.Pools;
using static Utilities.Extensions.VectorExtensions;

namespace Gameplay.Perks.Batman
{
    public class BatsSwarmPerk : ActivePerk, IKnowTargetScanner
    {
        [FoldoutGroup("Prefab Setup")] [SerializeField] float dmgInterval = 0.3f;
        [FoldoutGroup("Prefab Setup")] [SerializeField] Transform container;
        [FoldoutGroup("Prefab Setup")] [SerializeField] BatsSwarm prefab;
        [FoldoutGroup("Prefab Setup")] [SerializeField] BatsSwarmPool pool;
        [FoldoutGroup("Prefab Setup")] [SerializeField] float swarmRadius = 5;
        [FoldoutGroup("Prefab Setup")] [SerializeField] SoundSO sound;
        
        [Space(20)]
        [ListDrawerSettings(Expanded = true, HideRemoveButton = true, HideAddButton = true, DraggableItems = false)]
        [ValidateInput(nameof(EqualMaxLevel), "COUNT != " + nameof(MaxLevel))]
        [SerializeField] List<BatSwarmStats> stats = new() {new(), new(), new(), new(), new()};

        bool EqualMaxLevel() => stats.Count == MaxLevel;
        public BatSwarmStats Stats => Level > 0 && Level <= stats.Count ? stats[Level - 1] : null;
        bool LevelError => Level <= 0 && Level > stats.Count;
        public float DmgInterval => dmgInterval;
        public float Damage => Stats.dps / dmgInterval;
        public float LifeTime => Stats.lifeTime;

        void Awake()
        {
            pool.SetPrefab(prefab, container);
        }

        void FixedUpdate()
        {
            if (LevelError) return;
            if (!Scanner) return;

            cooldown -= Time.fixedDeltaTime;
            if (cooldown > 0) return;

            Scanner.Scan();
            Scanner.OnScan -= Swarm;
            Scanner.OnScan += Swarm;
        }

        float Cooldown => Stats.cooldown * Multipliers.Cooldown;
   
        void Swarm()
        {
            Scanner.OnScan -= Swarm;
            if (Scanner.NoTargets) return;
            
            var pos = transform.position;
            var target = Scanner.GetClosestTarget();
 
            var dir = (target.position - pos).normalized;
            var dist = Vector2.Distance(target.position, pos);
          
            if (dist > swarmRadius) return;
            
            cooldown = Cooldown;

            for (int i = 0; i < Stats.count; i++)
            {
                var angle = GetAngle(dir);
               
                var swarm = pool.Get();
                swarm.SetAngle(angle);
                swarm.SetPerk(this);
                swarm.Enable();
            }
            
            Audio.Play(sound);
        }


        public TargetsScanner Scanner { get; private set; }
        public void SetScanner(TargetsScanner scanner) => Scanner = scanner;
    }
}