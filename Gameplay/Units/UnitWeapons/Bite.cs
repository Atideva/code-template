using Gameplay.Interface;
using NaughtyAttributes;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.MonoCache;

namespace Gameplay.Units.UnitWeapons
{
    public class Bite : MonoCache
    {
        [Tag] public string targets;

        [SerializeField, Sirenix.OdinInspector.ReadOnly] float cooldown;
        float _damage;
        public float attackSpeed = 1;
        bool IsCooldown => cooldown > 0;
        public void SetDamage(float dmg) => _damage = dmg;
        public void SetTargets(string targetTag) => targets = targetTag;
        Unit target;


        void OnTriggerEnter2D(Collider2D enter)
        {
            if (IsCooldown) return;
            if (!enter.CompareTag(targets)) return;

            var unit = Scene.Instance.Units.Get(targets, enter.transform);
            if (!unit) return;

            if (!target)
                target = unit;
        }

        void OnTriggerStay2D(Collider2D enter)
        {
            if (IsCooldown) return;
            if (!enter.CompareTag(targets)) return;

            var unit =
                Scene.Instance.Units.Get(targets, enter.transform);
            if (!unit) return;

            if (!target)
                target = unit;
        }


        void OnTriggerExit2D(Collider2D exit)
        {
            if (!exit.CompareTag(targets)) return;

            var unit =
                Scene.Instance.Units.Get(targets, exit.transform);
            if (!unit) return;

            if (unit == target)
                target = null;
        }

        void Cooldown()
            => cooldown = 1 / attackSpeed;

        public void DealDamage(IHasHitpoints hp)
            => hp.TakeDamage(_damage);

        protected override void OnFixedUpdate()
        {
            if (cooldown <= 0)
            {
                if (!target) return;
                DealDamage(target);
                Cooldown();

                return;
            }

            cooldown -= Time.fixedDeltaTime;
        }
    }
}