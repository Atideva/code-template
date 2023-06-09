using System.Collections.Generic;
using Gameplay.Perks.Active.Content;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.Pools;

namespace Gameplay.Perks.Active
{
    public class NukePerk : ActivePerk
    {
        [InfoBox("TODO: Add multi bombs logic please", InfoMessageType.Warning)]
        [FoldoutGroup("Prefab Setup")] [SerializeField] Content.Nuke prefab;
        [FoldoutGroup("Prefab Setup")] [SerializeField] NukePool pool;

        [Space(20)]
        [ListDrawerSettings(Expanded = true, HideRemoveButton = true, HideAddButton = true, DraggableItems = false)]
        [ValidateInput(nameof(EqualMaxLevel), "COUNT != " + nameof(MaxLevel))]
        [SerializeField] List<NukeStats> stats = new() {new(), new(), new(), new(), new()};

        public NukeStats Stats => Level > 0 && Level <= stats.Count ? stats[Level - 1] : null;
        bool EqualMaxLevel() => stats.Count == MaxLevel;
        bool LevelError => Level <= 0 && Level > stats.Count;
        public float Cooldown => Stats.cooldown * Multipliers.Cooldown;
        void Awake()
        {
            pool.SetPrefab(prefab);
        }

        void FixedUpdate()
        {
            if (LevelError) return;

            cooldown -= Time.fixedDeltaTime;
            if (cooldown > 0) return;

            cooldown = Cooldown;
            BigBoom();
        }

        void BigBoom()
        {
            var nuke = pool.Get();
            nuke.transform.position = transform.position;
            nuke.Activate(this);
        }
 
    }
}