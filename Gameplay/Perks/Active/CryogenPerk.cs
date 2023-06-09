using System.Collections.Generic;
using Gameplay.Perks.Active.Content;
using Gameplay.Units.HeroComponents;
using Gameplay.Units.UnitComponents.Move;
using Meta.Facade;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.AudioSystem;
using Utilities.Pools;
using static Utilities.Extensions.VectorExtensions;

namespace Gameplay.Perks.Active
{
    public class CryogenPerk : ActivePerk, IKnowTargetScanner //, IKnowMovement
    {
        [FoldoutGroup("Prefab Setup")] [SerializeField] Transform container;
        [FoldoutGroup("Prefab Setup")] [SerializeField] Cryogen prefab;
        [FoldoutGroup("Prefab Setup")] [SerializeField] CryogenPool pool;
        [FoldoutGroup("Prefab Setup")] [SerializeField] List<Cryogen> prewarm = new();
        [FoldoutGroup("Prefab Setup")] [SerializeField] List<Transform> positions = new();
        [FoldoutGroup("Prefab Setup")] [SerializeField] SoundSO sound;
        
        [Space(20)]
        [ListDrawerSettings(Expanded = true, HideRemoveButton = true, HideAddButton = true, DraggableItems = false)]
        [ValidateInput(nameof(EqualMaxLevel), "COUNT != " + nameof(MaxLevel))]
        [SerializeField] List<CryogenStats> stats = new() {new(), new(), new(), new(), new()};

        bool EqualMaxLevel() => stats.Count == MaxLevel;
        public CryogenStats Stats => Level > 0 && Level <= stats.Count ? stats[Level - 1] : null;
        bool LevelError => Level <= 0 && Level > stats.Count;

        public float MovementSlow => Stats.slow;
        public float SlowDuration => Stats.slowDuration * Multipliers.Duration;
        float Cooldown => Stats.cooldown * Multipliers.Cooldown;


        void Awake()
        {
            foreach (var c in prewarm)
            {
                c.transform.SetParent(null);
                c.gameObject.SetActive(false);
            }

            pool.Prewarm(prewarm);
            pool.SetPrefab(prefab);
        }

        // void Update()
        // {
        //     var angle = GetAngle(Move.Direction);
        //     container.transform.rotation = Quaternion.Euler(0, 0, angle);
        //     // container.transform.localPosition = Move.IsMoving ? new Vector3(Move.Speed, 0, 0) : Vector3.zero;
        // }

        void FixedUpdate()
        {
            if (LevelError) return;
            if (!Scanner) return;
            //  if (!Move) return;

            cooldown -= Time.fixedDeltaTime;
            if (cooldown > 0) return;

            Scanner.Scan();
            Scanner.OnScan -= Freeze;
            Scanner.OnScan += Freeze;
        }

        void Freeze()
        {
            Scanner.OnScan -= Freeze;
            if (Scanner.NoTargets) return;

            for (int i = 0; i < Stats.count; i++)
            {
                var target = Scanner.GetRandomTarget();
                var blast = pool.Get();
                blast.transform.position = target.position;
                blast.Activate(this);
            }

            cooldown = Cooldown;
            Audio.Play(sound);
        }

        // void Freeze()
        // {
        //     for (int i = 0; i < Stats.count; i++)
        //     {
        //         var blast = pool.Get();
        //
        //         var pos = i < positions.Count
        //             ? positions[i]
        //             : positions[Random.Range(0, positions.Count)];
        //         blast.transform.position = pos.position;
        //         //  blast.transform.rotation = pos.rotation;
        //
        //         blast.Activate(this);
        //     }
        // }

        // public MoveEngine Move { get; private set; }
        // public void SetMovement(MoveEngine move) => Move = move;
        public TargetsScanner Scanner { get; private set; }
        public void SetScanner(TargetsScanner scanner) => Scanner = scanner;
    }
}