using System.Collections.Generic;
using Gameplay.Units.HeroComponents;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.MonoCache.System;

namespace Gameplay.Drop
{
    public class SceneExperienceDrop : MonoBehaviour
    {
        [Space(20)]
        [InfoBox("Sort all drops in 2 groups: CLOSE to player, FAR from player")]
        [SerializeField] float closeRadius = 15;
        [SerializeField] float sortIntervalSec = 1;


        [Space(20)]
        [SerializeField] [ReadOnly] List<ExperienceDrop> closeToPlayer = new();
        [SerializeField] [ReadOnly] List<ExperienceDrop> farFromPlayer = new();
        public int Total => closeToPlayer.Count + farFromPlayer.Count;

        float _timer;
        HeroExperienceMagnet _hero;
        public IEnumerable<ExperienceDrop> CloseToPlayer => closeToPlayer;


        void FixedUpdate()
        {
            _timer -= Time.fixedDeltaTime;
            if (_timer > 0) return;

            _timer = sortIntervalSec;
            SortByDistanceToPlayer();
        }

        public void RegisterHero(HeroExperienceMagnet hero)
        {
            _hero = hero;
        }

        public void Add(ExperienceDrop drop)
        {
            if (!closeToPlayer.Contains(drop))
                closeToPlayer.Add(drop);
        }

        public void PickUp(ExperienceDrop drop)
        {
            if (closeToPlayer.Contains(drop))
                closeToPlayer.Remove(drop);
            if (farFromPlayer.Contains(drop))
                farFromPlayer.Remove(drop);
        }

        void SortByDistanceToPlayer()
        {
            var heroPos = _hero.transform.position;

            var far = new List<ExperienceDrop>();
            foreach (var drop in closeToPlayer)
            {
                if (IsFar(heroPos, drop.Position))
                    far.Add(drop);
            }

            var close = new List<ExperienceDrop>();
            foreach (var drop in farFromPlayer)
            {
                if (IsClose(heroPos, drop.Position))
                    close.Add(drop);
            }

            foreach (var drop in far)
            {
                closeToPlayer.Remove(drop);
                farFromPlayer.Add(drop);
            }

            foreach (var drop in close)
            {
                closeToPlayer.Add(drop);
                farFromPlayer.Remove(drop);
            }
        }

        bool IsClose(Vector3 from, Vector3 to)
            => (from - to).sqrMagnitude <= closeRadius * closeRadius;

        bool IsFar(Vector3 from, Vector3 to)
            => (from - to).sqrMagnitude > closeRadius * closeRadius;
    }
}