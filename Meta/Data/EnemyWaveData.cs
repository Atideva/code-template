using Sirenix.OdinInspector;
using SO;
using SO.UnitsSO;
using UnityEngine;

namespace Meta.Data
{
    [System.Serializable]
    public class EnemyWaveData
    {
        [Space(20)]
        [SerializeField] EnemySO enemy;
        [SerializeField] float enemyPerSec;
        [MinMaxSlider(0, 300, true)]
        [SerializeField] Vector2 spawnTime;

        public float StartSpawnTime => spawnTime.x;
        public float EndSpawnTime => spawnTime.y;


        public float EnemyPerSec => enemyPerSec;
        public EnemySO Enemy => enemy;
    }
}