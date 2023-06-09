using System.Collections.Generic;
using Gameplay.UI;
using Meta.Data;
using NaughtyAttributes;
using Sirenix.OdinInspector;
using SO.ScenesSO;
using SO.UnitsSO;
using UnityEngine;

namespace Gameplay.Spawn
{
    public class EnemyWaves : MonoBehaviour
    {
        public SceneSO sceneConfig;
        public EnemySpawner spawner;


        [SerializeField] Transform bossesContainer;
        [SerializeField] Transform wavesContainer;
        [SerializeField, Tag] string enemies;
        [Header("DEBUG")]
        [SerializeField] [DisableInInlineEditors]
        List<EnemyBossSpawn> bosses = new();
        [SerializeField] [DisableInInlineEditors]
        List<EnemyWaveSpawn> waves = new();
        [SerializeField, Sirenix.OdinInspector.ReadOnly] bool bossFight;
        PlayTime _playTime;
        SceneUnits _units;

        public void Init(PlayTime playTime, SceneUnits units)
        {
            _units = units;
            _playTime = playTime;
            CreateWaveSpawns(sceneConfig);
            CreateBossSpawns(sceneConfig);
        }

        void CreateBossSpawns(SceneSO scene)
        {
            if (!scene.Bosses) return;

            if (scene.Bosses.At5minute)
                CreateBoss(scene.Bosses.At5minute, "Boss at 5 min");

            if (scene.Bosses.At10minute)
                CreateBoss(scene.Bosses.At10minute, "Boss at 10 min");

            if (scene.Bosses.At15minute)
                CreateBoss(scene.Bosses.At15minute, "Boss at 15 min");
        }

        void CreateBoss(BossSO bossSO, string naming)
        {
            var cont = new GameObject {name = naming};
            cont.transform.SetParent(bossesContainer);

            var bossSpawn = cont.AddComponent<EnemyBossSpawn>();
            var hp = GameplayUI.Instance.BossHitpoints;
            var lvl = GameplayUI.Instance.HeroLevel;
            bossSpawn.Set(bossSO, spawner, hp, lvl);

            bosses.Add(bossSpawn);
        }


        void CreateWaveSpawns(SceneSO scene)
        {
            if (!scene.Creeps) return;
            CreateWaves(scene.Creeps.From0To5min, "Waves from 0 to 5 min", 0);
            CreateWaves(scene.Creeps.From5To10min, "Waves from 5 to 10 min", 300);
            CreateWaves(scene.Creeps.From10To15min, "Waves from 10 to 15 min", 600);
        }

        void CreateWaves(SceneEnemyWaveSO so, string naming, float delay)
        {
            if (!so) return;

            var cont = new GameObject {name = naming};
            cont.transform.SetParent(wavesContainer);

            for (var i = 0; i < so.waves.Count; i++)
            {
                var wave = so.waves[i];
                NewWave(wave, _playTime, "Wave - " + (i + 1), cont.transform, delay);
            }
        }

        void NewWave(EnemyWaveData waveSO, PlayTime playTime, string naming, Transform container, float delay)
        {
            var cont = new GameObject {name = naming};
            cont.transform.SetParent(container);

            var wave = cont.AddComponent<EnemyWaveSpawn>();
            wave.Set(waveSO, spawner, playTime, delay);

            waves.Add(wave);
        }

        void FixedUpdate()
        {
            if (bossFight) return;
            var sec = _playTime.TotalSeconds;

            if (sec >= 300 && bosses.Count > 0 && bosses[0].NotSpawned)
            {
                _units.RemoveAll(enemies);
                bosses[0].Spawn();
                bosses[0].OnBossKill += OnBossKill;
                bossFight = true;
            }

            if (sec >= 600 && bosses.Count > 1 && bosses[1].NotSpawned)
            {
                _units.RemoveAll(enemies);
                bosses[1].Spawn();
                bosses[1].OnBossKill += OnBossKill;
                bossFight = true;
            }

            if (sec >= 900 && bosses.Count > 2 && bosses[2].NotSpawned)
            {
                _units.RemoveAll(enemies);
                bosses[2].Spawn();
                bosses[2].OnBossKill += OnBossKill;
                bossFight = true;
            }
        }

        void OnBossKill()
        {
            bossFight = false;
            foreach (var bossSpawn in bosses)
                bossSpawn.OnBossKill -= OnBossKill;
        }
    }
}