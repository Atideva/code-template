using Meta.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.MonoCache;

namespace Gameplay.Spawn
{
    public class EnemyWaveSpawn : MonoCache
    {
        [SerializeField] EnemyWaveData waveSO;
        [SerializeField] [ReadOnly] float spawnTimer;
        [SerializeField] [ReadOnly] EnemySpawner spawner;
        PlayTime _playTime;
        [SerializeField, ReadOnly] float startFromSeconds;
        [SerializeField, ReadOnly] float endAtSeconds;
        public bool NotSpawn => !IsSpawn;
        public bool IsSpawn { get; private set; }
        bool HasSpawn;

        public void Set(EnemyWaveData so, EnemySpawner spawn, PlayTime playTime, float delay)
        {
            _playTime = playTime;
            spawner = spawn;
            waveSO = so;

            startFromSeconds = waveSO.StartSpawnTime + delay;
            endAtSeconds = waveSO.EndSpawnTime + delay;
            //
            // Invoke(nameof(StartWave), waveSO.StartSpawnTime + delay);
            // Invoke(nameof(EndWave), waveSO.EndSpawnTime + delay);
        }

        void StartWave()
        {
            HasSpawn = true;
            IsSpawn = true;
            spawnTimer = 1 / waveSO.EnemyPerSec;
        }

        void EndWave()
        {
            IsSpawn = false;
        }

        protected override void OnFixedUpdate()
        {
            if (!HasSpawn && _playTime.TotalSeconds >= startFromSeconds)
            {
                StartWave();
            }

            if (HasSpawn && _playTime.TotalSeconds >= endAtSeconds)
            {
                EndWave();
            }

            if (_playTime.Stop) return;
            if (NotSpawn) return;

            spawnTimer -= Time.fixedDeltaTime;
            if (spawnTimer > 0) return;

            spawnTimer = 1 / waveSO.EnemyPerSec;
            spawner.Spawn(waveSO.Enemy);
        }
    }
}