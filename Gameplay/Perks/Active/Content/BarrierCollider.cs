using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Perks.Active.Content
{
    public class BarrierCollider : MonoBehaviour
    {
        [SerializeField] CircleCollider2D circleCollider;
        [SerializeField] [ReadOnly] BarrierPerk perk;
        public void SetPerk(BarrierPerk ballsPerk) => perk = ballsPerk;
        void OnTriggerEnter2D(Collider2D enemy) => Check(enemy);
        void OnTriggerStay2D(Collider2D enemy) => Check(enemy);
        public event Action<float> OnAbsorb=delegate(float f) {  }; 
        void Check(Collider2D enemy)
        {
            var bullet = Scene.Instance.Bullets.Get(perk.targets, enemy.transform);
            if (!bullet) return;
            
            OnAbsorb(bullet.Damage);
            bullet.Dispose();
        }

        public void Enable()
        {
            circleCollider.enabled = true;
        }

        public void Disable()
        {
            circleCollider.enabled = false;
        }
    }
}