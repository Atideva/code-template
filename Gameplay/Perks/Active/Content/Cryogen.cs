using UnityEngine;
using Utilities.Pools;

namespace Gameplay.Perks.Active.Content
{
    public class Cryogen : PoolObject
    {
        [SerializeField] Transform sizeContainer;
        [SerializeField] CryogenCollider dmgCollider;
        [SerializeField] float damageDelay;
        [SerializeField] float vfxLifeTime;

        public void Activate(CryogenPerk perk)
        {
            dmgCollider.SetPerk(perk);

            var size = perk.Stats.radius;
            sizeContainer.transform.localScale = new Vector3(size, size, size);

            Invoke(nameof(Damage), damageDelay);
            Invoke(nameof(Disable), vfxLifeTime);
        }

        void Damage() => dmgCollider.Enable(0.1f);
 
        void Disable() => ReturnToPool();
    }
}