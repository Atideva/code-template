using Gameplay.Units.UnitComponents.Move;
using UnityEngine;
using Utilities.MonoCache;
using static Utilities.Extensions.VectorExtensions;

namespace Gameplay.AI
{
    public class FollowTarget : MonoCache
    {
        [SerializeField] bool follow;
        [SerializeField] Transform target;
        [SerializeField] MoveEngine movement;
        [SerializeField] float minDistance;
        [SerializeField] float randomMoveDistance;
        Transform _my;
        [SerializeField] Rigidbody2D rb;

        public Transform Target => target;

        void Awake()
        {
            _my = transform;
        }

        float offsetAngle;
        float offsetDist;

        protected override void OnEnabled()
        {
            offsetAngle = Random.Range(0f, 360f);
            offsetDist = Random.Range(randomMoveDistance / 2f, randomMoveDistance);
        }

        public void Init(MoveEngine move)
        {
            movement = move;
        }

        protected override void OnUpdate()
        {
            if (!follow || !target) return;

            var vector = target.position - _my.position;
            if (vector.sqrMagnitude < minDistance * minDistance)
            {
                rb.velocity = Vector2.zero;
                return;
            }

            Vector3 dir;

            if (vector.sqrMagnitude > randomMoveDistance * randomMoveDistance)
            {
                var offset = GetVector(offsetAngle) * offsetDist;
                var offsetPos = target.position + (Vector3) offset;
                dir = (offsetPos - _my.position).normalized;
            }
            else
            {
                dir = vector.normalized;
            }

            movement.SetDirection(dir);
            movement.Move();
        }


        public void SetTarget(Transform newTarget) => target = newTarget;
        public void Follow() => follow = true;
        public void Stop()
        {
            follow = false;
            movement.Stop();
        }
    }
}