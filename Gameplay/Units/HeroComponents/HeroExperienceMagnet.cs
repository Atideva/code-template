using System.Collections.Generic;
using Gameplay.Drop;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.CharacterStats;

namespace Gameplay.Units.HeroComponents
{
    public class HeroExperienceMagnet : MonoBehaviour
    {
        [SerializeField] float power;
        [SerializeField] float radius;
        [SerializeField] float checkCooldown = 0.1f;
        [SerializeField] [ReadOnly] List<ExperienceDrop> inRadius = new();
        [SerializeField] [ReadOnly] List<ExperienceDrop> magnetByItem = new();
        public CharacterStat RadiusMultiplier { get; set; }
        public float Mult => RadiusMultiplier.Value;
        const float HERO_SIZE = 1f;

        SceneExperienceDrop droper;
        float _timer;
        Hero _hero;

        public void Init(Hero hero) => _hero = hero;

        void Start()
        {
            RadiusMultiplier = new CharacterStat(1);
            droper = Scene.Instance.ExperienceDrop;
            droper.RegisterHero(this);
        }

        void FixedUpdate()
        {
            _timer -= Time.fixedDeltaTime;
            if (_timer < 0)
            {
                _timer = checkCooldown;
                Check();
            }

            if (inRadius.Count <= 0) return;

            var pos = transform.position;
            List<ExperienceDrop> toRemove = new();
            
            foreach (var drop in inRadius)
            {
                if (!drop.gameObject.activeSelf) continue;

                if (CloseEnough(pos, drop.Position))
                {
                    drop.Pickup(_hero);
                    droper.PickUp(drop);
                    toRemove.Add(drop);
                }
                else
                {
                    var dir = (pos - drop.Position).normalized;
                    drop.Transform.position += dir * (power * Time.fixedDeltaTime);
                }
            }

            foreach (var drop in toRemove)
                if (inRadius.Contains(drop))
                    inRadius.Remove(drop);
        }

        public void Pickup(ExperienceDrop drop)
        {
            drop.Pickup(_hero);
            droper.PickUp(drop);
            if (inRadius.Contains(drop))
                inRadius.Remove(drop);
        }
        
        void Check()
        {
            var pos = transform.position;
            foreach (var drop in droper.CloseToPlayer)
            {
                if (InRadius(pos, drop.Position) && drop.gameObject.activeSelf && !inRadius.Contains(drop))
                    inRadius.Add(drop);
            }
        }

        bool InRadius(Vector3 from, Vector3 to)
            => (from - to).sqrMagnitude <= radius * radius * Mult * Mult;

        bool CloseEnough(Vector3 from, Vector3 to)
            => (from - to).sqrMagnitude <= HERO_SIZE * HERO_SIZE;
    }
}