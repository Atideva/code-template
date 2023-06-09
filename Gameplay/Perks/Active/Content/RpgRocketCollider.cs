 
using UnityEngine;

namespace Gameplay.Perks.Active.Content
{
    public class RpgRocketCollider : MonoBehaviour
    {
        [SerializeField] CircleCollider2D circleCollider;
        float _damage;
        string _enemyTag;


        public void SetDamage(float damage) => _damage = damage;
        public void SetEnemyTag(string targets) => _enemyTag = targets;

        public void Enable()
        {
            circleCollider.enabled = true;
            Invoke(nameof(Disable), Time.fixedDeltaTime);
        }

        public void Disable()
        {
            circleCollider.enabled = false;
        }


        void OnTriggerEnter2D(Collider2D enemy) => Check(enemy);

        void Check(Collider2D enemy)
        {
            var unit = Scene.Instance.Units.Get(_enemyTag, enemy.transform);
            if (unit)
                unit.TakeDamage(_damage);
        }
    }
}