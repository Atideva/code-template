using System.Collections.Generic;
using Gameplay.Units;
using UnityEngine;

namespace Gameplay.AI
{
    public class AI : MonoBehaviour
    {
        [SerializeField] FollowTarget followTarget;
        protected Unit Unit;
        public FollowTarget Follow => followTarget;

        public void Init(Unit unit)
        {
            Unit = unit;
            followTarget.Init(unit.Movement);
            OnInit();
        }

        protected virtual void OnInit() { }

        public void FollowTarget(Transform target)
        {
            followTarget.SetTarget(target);
            followTarget.Follow();
        }
    }
}