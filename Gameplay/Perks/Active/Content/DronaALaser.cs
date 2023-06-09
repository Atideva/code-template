using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.Pools;

namespace Gameplay.Perks.Active.Content
{
    public class DronaALaser : PoolObject
    {
        [Header("Prefabs")]
        [SerializeField] LineRenderer line;
        [SerializeField] GameObject beamStart;
        [SerializeField] GameObject beamEnd;

        [Header("Beam Options")]
        [SerializeField] float beamEndOffset;
        [SerializeField] float textureScrollSpeed;
        [SerializeField] float textureLengthScale = 1f;
        [SerializeField] [ReadOnly] Transform owner;
        [SerializeField] [ReadOnly] Transform target;
        public Vector3 lookVector;
        public Vector3 lookVectorBack;

        [Button, DisableInEditorMode]
        public void Activate(Transform newOwner, Transform newTarget, float lifeTime)
        {
            owner = newOwner;
            target = newTarget;
            line.useWorldSpace = true;
            line.positionCount = 2;

            Invoke(nameof(Disable), lifeTime);
        }

        void Disable() => ReturnToPool();

        protected override void OnFixedUpdate()
        {
            line.SetPosition(0, owner.position);
            // var end = target.position - (transform.right * beamEndOffset);
            var end = target.position;
            line.SetPosition(1, end);

            if (beamStart)
            {
                beamStart.transform.position = owner.position;
                beamStart.transform.LookAt(end, lookVector);
            }

            if (beamEnd)
            {
                beamEnd.transform.position = end;
                beamEnd.transform.LookAt(beamStart.transform.position, lookVectorBack);
            }

            var distance = Vector3.Distance(owner.position, end);
            line.material.mainTextureScale = new Vector2(distance / textureLengthScale, 1);
            line.material.mainTextureOffset -= new Vector2(Time.deltaTime * textureScrollSpeed, 0);
        }
    }
}