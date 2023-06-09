using System.Collections.Generic;
using Gameplay.Units;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Perks.Active.Content
{
    public class ElectricFieldCollider : MonoBehaviour
    {

        [SerializeField] [ReadOnly] float cooldown;
        [SerializeField] CircleCollider2D circleCollider;
        [SerializeField] ElectricFieldPerk perk;
        [SerializeField] [ReadOnly] List<Unit> targetsInRange = new();

        public float Damage => perk.Dps / perk.Interval;
        bool _scan;

        void FixedUpdate()
        {
            if (_scan)
            {
                DamageTargets();
                StopScan();
            }

            cooldown -= Time.fixedDeltaTime;
            if (cooldown > 0) return;

            ScanForTargets();
        }

        void OnTriggerEnter2D(Collider2D enemy) => Check(enemy);
        void OnTriggerStay2D(Collider2D enemy) => Check(enemy);

        void Check(Collider2D enemy)
        {
            var unit = Scene.Instance.Units.Get(perk.Targets, enemy.transform);
            if (!unit) return;
            if (targetsInRange.Contains(unit)) return;

            targetsInRange.Add(unit);
        }

        void ScanForTargets()
        {
            _scan = true;
            targetsInRange.Clear();
            circleCollider.enabled = true;
        }

        void StopScan()
        {
            _scan = false;
            circleCollider.enabled = false;
            cooldown = perk.Interval;
        }

        void DamageTargets()
        {
            foreach (var unit in targetsInRange)
                unit.TakeDamage(Damage);
        }
    }
}