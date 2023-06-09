using System.Collections.Generic;
using Gameplay.Perks.Active.Content;
using Gameplay.Units.UnitComponents.Move;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.AudioSystem;
using Utilities.Extensions;
using Utilities.Pools;
using static Utilities.Extensions.VectorExtensions;

namespace Gameplay.Perks.Active
{
    public class CanisterPerk : ActivePerk, IKnowMovement
    {
        [FoldoutGroup("Prefab Setup")] [SerializeField] float dmgInterval = 0.2f;
        [FoldoutGroup("Prefab Setup")] [SerializeField] Vector2 throwDistance  ;
        [FoldoutGroup("Prefab Setup")] [SerializeField] Canister prefab;
        [FoldoutGroup("Prefab Setup")] [SerializeField] CanisterPool pool;
        [FoldoutGroup("Prefab Setup")] [SerializeField] Transform container;
        [FoldoutGroup("Prefab Setup")] [SerializeField] SoundSO sound;
        [FoldoutGroup("Prefab Setup")] [SerializeField] List<Canister> prewarmCanisters = new();
        [FoldoutGroup("Prefab Setup")] [SerializeField] List<Transform> canisterPositions = new();

        [Space(20)]
        [ListDrawerSettings(Expanded = true, HideRemoveButton = true, HideAddButton = true, DraggableItems = false)]
        [ValidateInput(nameof(EqualMaxLevel), "COUNT != " + nameof(MaxLevel))]
        [SerializeField] List<CanisterStats> stats = new() {new(), new(), new(), new(), new()};

        bool EqualMaxLevel() => stats.Count == MaxLevel;
        public CanisterStats Stats => Level > 0 && Level <= stats.Count ? stats[Level - 1] : null;
        bool LevelError => Level <= 0 && Level > stats.Count;
        public float Damage => Stats.dps / dmgInterval;
        public int Count => Stats.count;
        public float LifeTime => Stats.lifeTime * Multipliers.Duration;
        float Cooldown => Stats.cooldown * Multipliers.Cooldown;

        void Awake()
        {
            foreach (var canister in prewarmCanisters)
            {
                canister.transform.SetParent(null);
                canister.gameObject.SetActive(false);
            }

            pool.Prewarm(prewarmCanisters);
            pool.SetPrefab(prefab);
        }

        void Update()
        {
            var angle = GetAngle(Move.Direction);
            container.transform.rotation = Quaternion.Euler(0, 0, angle);
            container.transform.localPosition = Move.IsMoving ? new Vector3(Move.Speed, 0, 0) : Vector3.zero;
        }

        void FixedUpdate()
        {
            if (LevelError) return;
            if (!Move) return;

            cooldown -= Time.fixedDeltaTime;
            if (cooldown > 0) return;

            cooldown = Cooldown;

            ThrowCanisters();
        }

        void ThrowCanisters()
        {
            for (int i = 0; i < Count; i++)
            {
                var canister = pool.Get();

                var angle = Random.Range(0f, 360f);
                var dir = VectorExtensions.GetVector(angle);
                var dist = Random.Range(throwDistance.x, throwDistance.y);
                var pos = (Vector2)transform.position + dir * dist;
                
                // var pos = i < canisterPositions.Count
                //     ? canisterPositions[i]
                //     : canisterPositions[Random.Range(0, canisterPositions.Count)];
                canister.transform.position = pos;
                //canister.transform.rotation = pos.rotation;

                canister.SetPerk(this);
                canister.SetWidth(Stats.wallWidth);
                canister.SetInterval(dmgInterval);
                canister.Throw(transform.position, LifeTime);
            }
        }


        protected override void OnLevelUp()
        {
        }

        public MoveEngine Move { get; private set; }

        public SoundSO Sound => sound;

        public void SetMovement(MoveEngine move) => Move = move;
    }
}