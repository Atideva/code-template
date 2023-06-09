using System.Collections.Generic;
using Gameplay.Units;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Perks.Active.Content
{
    public class CanisterCollider : MonoBehaviour
    {
        [SerializeField] CircleCollider2D dmgCollider;
        [SerializeField] [ReadOnly] CanisterPerk perk;
        [SerializeField] [ReadOnly] List<Unit> targets = new();
        public float Damage => perk.Damage;
        public void SetPerk(CanisterPerk p) => perk = p;

        public void Enable()
        {
            dmgCollider.enabled = true;
            Invoke(nameof(DamageTargets), Time.fixedDeltaTime);
        }

        public void Disable() => dmgCollider.enabled = false;

        public void DamageTargets()
        {
            foreach (var unit in targets)
                unit.TakeDamage(Damage);

            targets.Clear();
            Disable();
        }


        void OnTriggerEnter2D(Collider2D enemy) => Check(enemy);

        void Check(Collider2D enemy)
        {
            var unit = Scene.Instance.Units.Get(perk.Targets, enemy.transform);

            if (!unit) return;
            if (targets.Contains(unit)) return;

            targets.Add(unit);
        }
    }
}