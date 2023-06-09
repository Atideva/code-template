using Gameplay.Units;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.MonoCache.System;
using Utilities.Pools;

namespace Gameplay.Perks.Active.Content
{
    public class Blaster : PoolObject
    {
        [Header("Prefabs")]
        [SerializeField] LineRenderer line;
        [SerializeField] GameObject beamStart;
        [SerializeField] GameObject beamEnd;

        [Header("Beam Options")]
        [SerializeField] float beamEndOffset;
        [SerializeField] float textureScrollSpeed;
        [SerializeField] float textureLengthScale = 1f;
        [SerializeField] Vector3 lookVector;
        [SerializeField] Vector3 lookVectorBack;

        [SerializeField] [ReadOnly] Unit unit;

        public bool HasTarget => unit;


        BlasterPerk _perk;
        float _interval;
        public float damageInterval = 0.2f;


        public void SetPerk(BlasterPerk perk) => _perk = perk;


        [Button, DisableInEditorMode]
        public void Activate(Unit targetUnit)
        {
            if (!targetUnit) return;
            
            gameObject.Enable();
            unit = targetUnit;
            unit.Hitpoints.OnDeath += Disable;

            line.useWorldSpace = true;
            line.positionCount = 2;
        }

        public void Disable()
        {
            if (unit)
            {
                unit.Hitpoints.OnDeath -= Disable;
                unit = null;
            }

            ReturnToPool();
        }

        protected override void OnFixedUpdate()
        {
            if (!unit) return;

            line.SetPosition(0, transform.position);
            var end = unit.transform.position - (transform.right * beamEndOffset);
            line.SetPosition(1, end);

            if (beamStart)
            {
                beamStart.transform.position = transform.position;
                beamStart.transform.LookAt(end, lookVector);
            }

            if (beamEnd)
            {
                beamEnd.transform.position = end;
                beamEnd.transform.LookAt(beamStart.transform.position, lookVectorBack);
            }

            var distance = Vector2.Distance(transform.position, end);
            line.material.mainTextureScale = new Vector2(distance / textureLengthScale, 1);
            line.material.mainTextureOffset -= new Vector2(Time.deltaTime * textureScrollSpeed, 0);

            if (distance > maxDistance)
                Disable();

            _interval -= Time.fixedDeltaTime;
            if (_interval > 0) return;

            _interval = damageInterval;
         //   unit.TakeDamage(Damage);
        }

    //    float Damage => _perk.Stats.dps * damageInterval;
        public float maxDistance = 15;
    
    }
}