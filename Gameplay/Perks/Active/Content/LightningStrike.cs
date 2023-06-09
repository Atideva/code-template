using Meta.Facade;
using UnityEngine;
using Utilities.Pools;

namespace Gameplay.Perks.Active.Content
{
    public class LightningStrike : PoolObject
    {
        [SerializeField] Transform sizeContainer;
        [SerializeField] LightningStrikeCollider strikeCollider;
        [SerializeField] float damageDelay;
        [SerializeField] float vfxLifeTime;

        public void Activate(LightningStrikePerk perk)
        {
            strikeCollider.SetPerk(perk);

            var size = perk.Stats.radius;
            sizeContainer.transform.localScale = new Vector3(size, size, size);

            Audio.Play(perk.Sound);
            Invoke(nameof(Damage), damageDelay);
            Invoke(nameof(Disable), vfxLifeTime);
        }

        void Damage() => strikeCollider.Enable(0.1f);
        void Disable() => ReturnToPool();
    }
}