using System;
using System.Collections.Generic;
using Gameplay.Units.UnitWeapons;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.Pools;

namespace Gameplay
{
    public class SceneBullets : MonoBehaviour
    {
        [InfoBox("TODO: subscribe to 'Change Team' event and refresh dict", InfoMessageType.Warning )]
        [SerializeField] SceneBulletsPools pool;
        public readonly Dictionary<string, Dictionary<Transform, Bullet>> Bullets = new();
        
        public Bullet Spawn(Bullet bulletPrefab, string ownerTeam,string enemyTeam,string layer)
        {
            var bullet = pool.Get(bulletPrefab, ownerTeam,enemyTeam,layer);
            Register(bullet);
            return bullet;
        }

        public Bullet Get(string team, Transform t)
        {
            if (!Bullets.ContainsKey(team)) return null;
            var units = Bullets[team];
            return units.ContainsKey(t) ? units[t] : null;
        }

        void Register(Bullet b)
        {
            var team = b.Team;
            if (Bullets.ContainsKey(team))
                Bullets[team].Add(b.transform, b);
            else
            {
                var teamDict = new Dictionary<Transform, Bullet> {{b.transform, b}};
                Bullets.Add(team, teamDict);
            }

            b.OnReturnToPool += Dispose;
        }

        void Dispose(PoolObject poolObject)
        {
            poolObject.OnReturnToPool -= Dispose;
            if (poolObject is Bullet b) Unregister(b);
        }

        void Unregister(Bullet b)
        {
            var team = b.Team;
            if (!Bullets.ContainsKey(team)) return;
            var units = Bullets[team];
            if (units.ContainsKey(b.transform))
                units.Remove(b.transform);
        }
    }
}