using System.Collections.Generic;
using Gameplay.Perks.Active.Content;
using Gameplay.Units.HeroComponents;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.AudioSystem;
 

namespace Gameplay.Perks.Active
{
    public class DroneAPerk : ActivePerk, IKnowTargetScanner
    {
        [FoldoutGroup("Prefab Setup")] [SerializeField] DroneA prefab;
        [FoldoutGroup("Prefab Setup")] [SerializeField] float offset;
        [FoldoutGroup("Prefab Setup")] [SerializeField] float angle;
        [FoldoutGroup("Prefab Setup")] [SerializeField] [ReadOnly] List<DroneA> drones = new();
        [FoldoutGroup("Prefab Setup")] [SerializeField] float bulletSpeed;
        [FoldoutGroup("Prefab Setup")] [SerializeField] SoundSO sound;
        [FoldoutGroup("Prefab Setup")] [SerializeField] float burstInterval = 0.15f;


        [Space(20)]
        [ListDrawerSettings(Expanded = true, HideRemoveButton = true, HideAddButton = true, DraggableItems = false)]
        [ValidateInput(nameof(EqualMaxLevel), "COUNT != " + nameof(MaxLevel))]
        [SerializeField] List<DroneAStats> stats = new() {new(), new(), new(), new(), new()};

        public DroneAStats Stats => Level > 0 && Level <= stats.Count ? stats[Level - 1] : null;
        public float Damage => Stats.damage * Multipliers.Damage;
        public float BulletSpeed => bulletSpeed;
        
        
        protected override void OnLevelUp()
        {
            if (drones.Count < 1)
            {
              //  var dir = VectorExtensions.GetVector(angle);
               // var pos = (Vector2) transform.position + offset * dir;

                var newDrone = Instantiate(prefab, transform.position, Quaternion.identity);
                newDrone.SetPerk(this);
                newDrone.SetTarget(transform);
                //  newDrone.SetPos(pos);
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