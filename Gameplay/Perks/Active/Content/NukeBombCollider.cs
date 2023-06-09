using Gameplay.Pickables;
using UnityEngine;

namespace Gameplay.Perks.Active.Content
{
    public class NukeBombCollider : MonoBehaviour
    {
        [SerializeField] CircleCollider2D triggerCollider;

        NukeBomb bomb;
        public float Damage => bomb.Damage;

        public void SetPerk(NukeBomb ballsPerk)
        {
            bomb = ballsPerk;
        }

        public void Enable(float lifeTime)
        {
            triggerCollider.enabled = true;
            Invoke(nameof(Disable), lifeTime);
        }

        public void Disable()
        {
            triggerCollider.enabled = false;
        }


        void OnTriggerEnter2D(Collider2D enemy) => Check(enemy);

        void Check(Collider2D enemy)
        {
            var unit = Scene.Instance.Units.Get(bomb.Targets, enemy.transform);
            if (unit)
            {
                unit.TakeDamage(Damage);
                return;
            }
            
            var bullet = Scene.Instance.Bullets.Get(bomb.Targets, enemy.transform);
            if (bullet)
                bullet.Dispose();
        }
    }
}