using Gameplay.Animations;
using UnityEngine;

namespace Gameplay.Units
{
    public class UnitModel : MonoBehaviour
    {
        [SerializeField] UnitAnimator animator;
        [SerializeField] SpriteHit hit;
        protected UnitAnimator Animator => animator;

        public void Move()
        {
            if (!animator) return;
            animator.Move();
        }

        public void Death()
        {
            if (!animator) return;
            animator.Death();
        }

        public void Attack()
        {
        }

        public void Idle()
        {
            if (!animator) return;
            animator.Idle();
        }

        public void Hit()
        {
            hit.Play();
        }

     
    }
}