using System.Collections.Generic;
using DG.Tweening;
using Gameplay.Units;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.CharacterStats;

namespace Gameplay.Perks.Active.Content
{
    public class CryogenCollider : MonoBehaviour
    {
        [SerializeField] CircleCollider2D circleCollider;
        [SerializeField] [ReadOnly] CryogenPerk perk;
        [SerializeField] [ReadOnly] List<Unit> slowed = new();

        public float Damage => perk.Stats.damage;

        public void SetPerk(CryogenPerk ballsPerk)
        {
            perk = ballsPerk;
        }

        public void Enable(float lifeTime)
        {
            slowed = new List<Unit>();
            circleCollider.enabled = true;
            Invoke(nameof(Disable), lifeTime);
        }

        public void Disable()
        {
            circleCollider.enabled = false;
        }

        void OnTriggerEnter2D(Collider2D enemy) => Check(enemy);

        void Check(Collider2D enemy)
        {
            var unit = Scene.Instance.Units.Get(perk.Targets, enemy.transform);
            if (!unit) return;
            
            unit.TakeDamage(Damage);
            if (slowed.Contains(unit)) return;
                
            slowed.Add(unit);
            var slow = new CharacterMod(perk.MovementSlow, StatModType.Percent);
            unit.Movement.Multiplier.AddModifier(slow);
                
            DOVirtual.DelayedCall(perk.SlowDuration, () => StopSlow(unit, slow));
        }

        void StopSlow(Unit unit, CharacterMod slow)
            => unit.Movement.Multiplier.RemoveModifier(slow);
    }
}