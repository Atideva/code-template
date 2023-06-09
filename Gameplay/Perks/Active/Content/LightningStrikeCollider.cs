using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Perks.Active.Content
{
    public class LightningStrikeCollider : MonoBehaviour
    {
        [SerializeField] CircleCollider2D circleCollider;

        [SerializeField] [ReadOnly] LightningStrikePerk perk;
        public float Damage => perk.Stats.damage;


        public void SetPerk(LightningStrikePerk ballsPerk)
        {
            perk = ballsPerk;
        }

        public void Enable(float lifeTime)
        {
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
            if (unit)
                unit.TakeDamage(Damage);
        }
    }
}