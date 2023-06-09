using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Animations
{
    public class AttachedVfx : Vfx
    {
        [SerializeField] [ReadOnly] Transform target;

        public void SetTarget(Transform followTarget)
        {
            target = followTarget;
        }

        protected override void OnUpdate()
        {
            if (!target) return;
            transform.position = target.position;
        }
    }
}