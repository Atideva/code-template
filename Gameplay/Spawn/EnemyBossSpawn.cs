using System;
using GameManager;
using Gameplay.UI;
using Gameplay.Units;
using Sirenix.OdinInspector;
using SO.UnitsSO;
using UnityEngine;

namespace Gameplay.Spawn
{
    public class EnemyBossSpawn : MonoBehaviour
    {
        [SerializeField] BossSO bossSO;
        [SerializeField, ReadOnly] Unit boss;

        EnemySpawner _spawner;
        BossHitpointsUI _hpUI;
        HeroLevelUI _levelUI;
   

        public bool NotSpawned => !IsSpawn;
        public bool IsSpawn { get; private set; }
        public event Action OnBossKill = delegate { };

        public void Set(BossSO so, EnemySpawner spawner,  BossHitpointsUI ui, HeroLevelUI levelUI)
        {
            _levelUI = levelUI;
            _hpUI = ui;
            _spawner = spawner;
            bossSO = so;
        }

        public void Spawn()
        {
            IsSpawn = true;
            boss = _spawner.Spawn(bossSO);
            boss.Hitpoints.OnDeath += Dead;
            boss.Hitpoints.OnDamage += Damage;
            boss.Hitpoints.OnHeal += Heal;

            _hpUI.Show();
            _hpUI.Refresh(boss.Hitpoints.Float);
            _levelUI.Hide();

            GameplayEvents.Instance.BossSpawn(boss);
        }

        void Heal(float heal)
        {
            _hpUI.Refresh(boss.Hitpoints.Float);
        }

        void Damage(float dmg)
        {
            _hpUI.Refresh(boss.Hitpoints.Float);
        }

        void Dead()
        {
            boss.Hitpoints.OnDeath -= Dead;
            boss.Hitpoints.OnDamage -= Damage;
            boss.Hitpoints.OnHeal -= Heal;

            OnBossKill();
            Scene.Instance.BlocksSpawner.RemoveBox();
            GameplayEvents.Instance.BossDeath(boss);

            _hpUI.Hide();
            _levelUI.Show();
        }
    }
}