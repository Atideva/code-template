using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Perks.Active.Content
{
    public class NukeCollider : MonoBehaviour
    {
        [SerializeField] BoxCollider2D boxCollider;

        [SerializeField] [ReadOnly] NukePerk perk;
        public float Damage => perk.Stats.damage;


        public void SetPerk(NukePerk ballsPerk)
        {
            perk = ballsPerk;
        }

        public void Enable(float lifeTime)
        {
            boxCollider.enabled = true;
            Invoke(nameof(Disable), lifeTime);
        }

        public void Disable()
        {
            boxCollider.enabled = false;
        }


        void OnTriggerEnter2D(Collider2D enemy) => Check(enemy);

        void Check(Collider2D enemy)
        {
            var unit = Scene.Instance.Units.Get(perk.Targets, enemy.transform);
            if (unit)
            {
                unit.TakeDamage(Damage);
                return;
            }
            
            var bullet = Scene.Instance.Bullets.Get(perk.Targets, enemy.transform);
            if (bullet)
                bullet.Dispose();
        }
    }
    
    
    
}