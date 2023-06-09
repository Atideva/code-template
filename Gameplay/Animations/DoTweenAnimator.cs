using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


namespace Gameplay.Animations
{
    public class DoTweenAnimator : UnitAnimator
    {
        [Space(20)]
        public List<DOTweenAnimation> moveAnims = new();

        [Space(20)]
        public List<DOTweenAnimation> attackAnims = new();
        
        [Space(20)]
        public List<DOTweenAnimation> idleAnims = new();
        
        public bool IsMove;

        public override void Move()
        {
            if (IsMove) return;
            IsMove = true;
            IsIdle = false;
               
            foreach (var anim in idleAnims)
            {
                anim.DORestart();
                anim.DOPause();
            }
            
            foreach (var anim in attackAnims)
            {
                anim.DORestart();
                anim.DOPause();
            }

            foreach (var anim in moveAnims)
            {
                anim.DORestart();
                anim.DOPlay();
            }
        }

        public override void Death()
        {
            IsMove = false;
            IsIdle = false;
            
            foreach (var anim in attackAnims)
                anim.DOPause();
            foreach (var anim in moveAnims)
                anim.DOPause();

            //    if (Enemy.Config)
            //    AudioManager.Instance.PlaySound(Enemy.Config.deathSound);
            //  Events.Instance.PlayVfx(DeathVfx, transform.position);
        }

        public override void Attack()
        {
            //    if (Enemy.Config)
            //    AudioManager.Instance.PlaySound(Enemy.Config.attackSound, AttackVfxDelay);
            // Events.Instance.PlayVfx(AttackVfx, transform.position, 0.2f);

            foreach (var anim in moveAnims)
            {
                anim.DORestart();
                anim.DOPause();
            }

            foreach (var anim in attackAnims)
            {
                anim.DORestart();
                anim.DOPlay();
            }
            
            foreach (var anim in idleAnims)
            {
                anim.DORestart();
                anim.DOPause();
            }
        }

        bool IsIdle;
        public override void Idle()
        {
            if (IsIdle) return;
            IsIdle = true;
            IsMove = false;
            
            foreach (var anim in moveAnims)
            {
                anim.DORestart();
                anim.DOPause();
            }

            foreach (var anim in attackAnims)
            {
                anim.DORestart();
                anim.DOPause();
            }
            
            foreach (var anim in idleAnims)
            {
                anim.DORestart();
                anim.DOPlay();
            }
        }

        public override void Hit()
        {
        }
    }
}