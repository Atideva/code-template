using UnityEngine;

namespace Gameplay.Animations
{
    public class DroneFollow : MonoBehaviour
    {
        [SerializeField] Vector3 offset = new(0f, 0f, -10f);
        [SerializeField] float smoothTime = 0.25f;
        Vector3 _velocity  ;

        [SerializeField] Transform target;
        public void SetTarget(Transform t) => target = t;
        void Update()
        {
            if(!target) return;
            var targetPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
        }
    }
}
