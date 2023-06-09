using Gameplay.Interface;
using NaughtyAttributes;
using UnityEngine;

namespace Gameplay.Units
{
    public class BossMeleeAttack : MonoBehaviour
    {

        public string animName;
        [Tag] public string targets;

        float _cooldown;
        float _damage;
        public float attackSpeed = 1;
        bool IsCooldown => _cooldown > 0;
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
            => _cooldown = 1 / attackSpeed;

        public void DealDamage(IHasHitpoints hp)
            => hp.TakeDamage(_damage);

         void  FixedUpdate()
        {
            if (_cooldown <= 0)
            {
                if (target)
                {
                    DealDamage(target);
                    Cooldown();
                }

                return;
            }

            _cooldown -= Time.fixedDeltaTime;
        }
        
    }
}
