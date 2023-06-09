using System.Collections.Generic;
using Gameplay.Perks.Active.Content;
using Gameplay.Units.HeroComponents;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.AudioSystem;
using Utilities.Pools;

namespace Gameplay.Perks.Active
{
    public class LightningStrikePerk : ActivePerk, IKnowTargetScanner
    {
        [FoldoutGroup("Prefab Setup")] [SerializeField] LightningStrike prefab;
        [FoldoutGroup("Prefab Setup")] [SerializeField] LightningStrikePool pool;
        [FoldoutGroup("Prefab Setup")] [SerializeField] SoundSO sound;

        [Space(20)]
        [ListDrawerSettings(Expanded = true, HideRemoveButton = true, HideAddButton = true, DraggableItems = false)]
        [ValidateInput(nameof(EqualMaxLevel), "COUNT != " + nameof(MaxLevel))]
        [SerializeField] List<LightningStrikeStats> stats = new() {new(), new(), new(), new(), new()};

        public LightningStrikeStats Stats => Level > 0 && Level <= stats.Count ? stats[Level - 1] : null;


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
            Scanner.OnScan -= SpawnLightning;
            Scanner.OnScan += SpawnLightning;
        }

        void SpawnLightning()
        {
            Scanner.OnScan -= SpawnLightning;
            if (Scanner.NoTargets) return;

            for (int i = 0; i < Stats.count; i++)
            {
                var target = Scanner.GetRandomTarget();
                var lightning = pool.Get();
                lightning.transform.position = target.position;
                lightning.Activate(this);
            }

            cooldown = Cooldown;
        }

        public TargetsScanner Scanner { get; private set; }

        public SoundSO Sound => sound;

        public void SetScanner(TargetsScanner scanner) => Scanner = scanner;
    }
}