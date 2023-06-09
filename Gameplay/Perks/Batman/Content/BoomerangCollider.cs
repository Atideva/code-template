using System.Collections.Generic;
using Gameplay.Units;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Perks.Batman.Content
{
    public class BoomerangCollider : MonoBehaviour
    {
        [SerializeField] BoxCollider2D dmgCollider;

        [SerializeField] [ReadOnly] BoomerangPerk perk;
        [SerializeField] [ReadOnly] List<Unit> touched = new();
        public float Damage => perk.Stats.damage;


        public void SetPerk(BoomerangPerk p)
        {
            perk = p;
        }

        public void Enable()
        {
            touched.Clear();
            dmgCollider.enabled = true;
        }

        public void Disable()
        {
            dmgCollider.enabled = false;
        }


        void OnTriggerEnter2D(Collider2D enemy) => Check(enemy);

        void Check(Collider2D enemy)
        {
            var unit = Scene.Instance.Units.Get(perk.Targets, enemy.transform);
      
            if (!unit) return;
            if (touched.Contains(unit)) return;
      
            unit.TakeDamage(Damage);
            touched.Add(unit);
        }
    }
}