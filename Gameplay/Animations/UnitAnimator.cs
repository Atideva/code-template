using UnityEngine;

namespace Gameplay.Animations
{
    
    public abstract class UnitAnimator : MonoBehaviour
    {
   
        protected Vfx DeathVfx;
        protected Vfx AttackVfx;
        protected float AttackVfxDelay;
        
        public void SetDeathVFX(Vfx vfx) => DeathVfx = vfx;

        public void SetAttackVFX(Vfx vfx, float delay)
        {
            AttackVfxDelay = delay;
            AttackVfx = vfx;
        }
 
        // protected bool IsMove => Enemy.IsMove;
        // protected Enemy Enemy { get; private set; }
        // public void Init(Enemy enemy) => Enemy = enemy;

        public abstract void Move();
        public abstract void Death();
        public abstract void Attack();
        public abstract void Idle();
        public abstract void Hit();
    }
}