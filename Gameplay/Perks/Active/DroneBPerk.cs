using System.Collections.Generic;
using Gameplay.Perks.Active.Content;
using Gameplay.Units.HeroComponents;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.AudioSystem;
using Utilities.Extensions;

namespace Gameplay.Perks.Active
{
    public class DroneBPerk : ActivePerk, IKnowTargetScanner
    {
        [FoldoutGroup("Prefab Setup")] [SerializeField] DroneB prefab;
        [FoldoutGroup("Prefab Setup")] [SerializeField] float offset;
        [FoldoutGroup("Prefab Setup")] [SerializeField] float angle;
        [FoldoutGroup("Prefab Setup")] [SerializeField] [ReadOnly] List<DroneB> drones = new();
        [FoldoutGroup("Prefab Setup")] [SerializeField] float bulletSpeed;
        [FoldoutGroup("Prefab Setup")] [SerializeField] float burstInterval = 0.15f;
        [FoldoutGroup("Prefab Setup")] [SerializeField] SoundSO sound;
       
        [Space(20)]
        [ListDrawerSettings(Expanded = true, HideRemoveButton = true, HideAddButton = true, DraggableItems = false)]
        [ValidateInput(nameof(EqualMaxLevel), "COUNT != " + nameof(MaxLevel))]
        [SerializeField] List<DroneBStats> stats = new() {new(), new(), new(), new(), new()};

        public DroneBStats Stats => Level > 0 && Level <= stats.Count ? stats[Level - 1] : null;
        public float Damage => Stats.damage;
        public float BulletSpeed => bulletSpeed;

        protected override void OnLevelUp()
        {
            if (drones.Count < 1)
            {
                var dir = VectorExtensions.GetVector(angle).normalized;
              //  var pos = (Vector2) transform.position + offset * dir;

                var newDrone = Instantiate(prefab, transform);
                newDrone.SetPerk(this);
                newDrone.SetTarget(transform);
                drones.Add(newDrone);

                angle += angle * 0.5f;
            }
        }

        bool EqualMaxLevel() => stats.Count == MaxLevel;
        public bool LevelError => Level <= 0 && Level > stats.Count;

        public TargetsScanner Scanner { get; private set; }

        public SoundSO Sound => sound;

        public float BurstInterval => burstInterval;

        public void SetScanner(TargetsScanner scanner) => Scanner = scanner;
    }
}