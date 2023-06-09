using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEngine;

namespace Gameplay.Animations
{
    public class SpineAnimator : UnitAnimator
    {
        [SerializeField] SkeletonAnimation skeleton;
        bool _lockAnim;
        
        public void PlayAnimation(string animName, float animTime = 0)
        {
            if (_lockAnim) return;
            
            skeleton.loop = false;
            skeleton.AnimationName = animName;
            Lock(animTime);
        }

        void Lock(float duration)
        {
            if (duration == 0) return;
            _lockAnim = true;
            Invoke(nameof(Unlock), duration);
        }
        void Unlock() => _lockAnim = false;

        [Button]
        public override void Move()
        {
            if (_lockAnim) return;

            skeleton.loop = true;
            skeleton.AnimationName = "walk";
        }

        public override void Death()
        {
        }

        public override void Attack()
        {
        }

        public override void Idle()
        {
        }

        public override void Hit()
        {
        }
    }
}