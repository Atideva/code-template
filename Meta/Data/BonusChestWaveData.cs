using Gameplay.BonusChests;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Meta.Data
{
    [System.Serializable]
    public class BonusChestWaveData
    {
        [Space(20)]
        [SerializeField] BonusChest prefab;
        [SerializeField] [Range(0,1)] float chance;
 
        [SerializeField] [Range(0,300)] int spawnTime;

 

        public float Chance => chance;
        public BonusChest Prefab => prefab;

        public float SpawnTime => spawnTime;
    }
}