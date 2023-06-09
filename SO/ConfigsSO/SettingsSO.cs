using System.Collections.Generic;
using Meta.Data;
using NaughtyAttributes;
using UnityEngine;

namespace SO.ConfigsSO
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Config/Settings")]
    public class SettingsSO : ScriptableObject
    {
        [SerializeField] float enemiesBaseAttackSpeed = 1f;
        [SerializeField] int magnetSpawnTrigger = 100;
        [SerializeField] float magnetSpawnDistanceFromHero = 20;
        [SerializeField] int bombSpawnTrigger = 100;
        [SerializeField] float bombSpawnCooldown = 100;
        [SerializeField] float bombSpawnDistanceFromHero = 20;
        [SerializeField] [Tag] string playerTeam;
        [SerializeField] [Tag] string enemyTeam;

        [Space(20)]
        [SerializeField] List<PriceData> heroLevelUpPrice = new();

        public float EnemiesBaseAttackSpeed => enemiesBaseAttackSpeed;
        public string EnemyTeam => enemyTeam;
        public string PlayerTeam => playerTeam;
        public int MagnetSpawnTrigger => magnetSpawnTrigger;
        public float MagnetSpawnDistanceFromHero => magnetSpawnDistanceFromHero;
        public int BombSpawnTrigger => bombSpawnTrigger;
        public float BombSpawnCooldown => bombSpawnCooldown;
        public float BombSpawnDistanceFromHero => bombSpawnDistanceFromHero;

        public PriceData GetHeroLevelUpPrice(int lvl)
        {
            var id = lvl - 1;
            if (id < 0) id = 0;
            if (id >= heroLevelUpPrice.Count) id = heroLevelUpPrice.Count - 1;
            return heroLevelUpPrice[id];
        }
    }
}