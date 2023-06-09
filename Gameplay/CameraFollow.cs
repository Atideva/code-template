using System;
using Gameplay.Spawn;
using UnityEngine;

namespace Gameplay
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] Transform camTransform;
 
        [SerializeField] Vector3 offset;
        [SerializeField] float damping;
       [SerializeField] Transform _target;
Vector3 velocity = Vector3.zero;
        public void SetTarget(Transform target)
        {
            _target = target;
        }
        // void LateUpdate()
        // {
        //     camTransform.position = _target.position+offset;
        // }

        void LateUpdate()
        {
            var movePos = _target.position + offset;
            camTransform.position = Vector3.Lerp(transform.position, movePos,   damping*Time.deltaTime);
        }
    }
}
