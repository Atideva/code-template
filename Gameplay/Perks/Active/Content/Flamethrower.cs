using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.Pools;

namespace Gameplay.Perks.Active.Content
{
    public class Flamethrower : PoolObject
    {
        [SerializeField] FlamethrowerCollider flameCollider;
        [SerializeField] Transform sizeContainer;
        [Tooltip("If vfx is too slow, set this to sync 'dmg deal' and 'visual'")]
        [SerializeField] float firstDamageDelay = 0.1f;
        [SerializeField] [ReadOnly] FlamethrowerPerk perk;
        [SerializeField] [ReadOnly] float timer;

        public float FirstDamageDelay => firstDamageDelay;

        public void Enable()
        {
            var size = perk.Stats.distance;
            sizeContainer.transform.localScale = new Vector3(size, size, size);

            flameCollider.Disable();
            timer = firstDamageDelay;
            Invoke(nameof(Destroy), firstDamageDelay + perk.LifeTime);
        }

        protected override void OnFixedUpdate()
        {
            timer -= Time.fixedDeltaTime;
            if (timer > 0) return;

            timer = perk.DmgInterval;
            EnableCollider();
        }

        void EnableCollider() => flameCollider.Enable();
        public void Destroy() => ReturnToPool();

        public void SetPerk(FlamethrowerPerk p)
        {
            perk = p;
            flameCollider.SetPerk(p);
        }

        public void SetAngle(float angle)
            => transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}