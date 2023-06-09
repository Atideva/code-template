using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.Pools;

namespace Gameplay.Perks.Batman.Content
{
    public class Boomerang : PoolObject
    {
        [SerializeField] BoomerangCollider dmgCollider;
        [SerializeField] Transform image;
        [SerializeField] Transform sizeContainer;
        [SerializeField] Transform moveContainer;

        [SerializeField] [ReadOnly] bool backToOwner;
        [SerializeField] [ReadOnly] float z;
        BoomerangPerk _perk;
        [SerializeField] Ease moveEase;
        [SerializeField] Ease paraboleEase;

        public void Activate(BoomerangPerk perk, float angle, float dist)
        {
            backToOwner = false;
            _perk = perk;
  
            transform.localRotation = Quaternion.Euler(0, 0, angle);

            dmgCollider.SetPerk(perk);
            dmgCollider.Enable();

            var size = perk.Stats.size;
            sizeContainer.transform.localScale = new Vector3(size, size, size);

            var flightTime = dist / perk.FlightSpeed;
            
            moveContainer
                .DOLocalMoveX(dist, flightTime)
                .SetEase(moveEase)
                .OnComplete(BackToOwner);
            
            sizeContainer
                .DOLocalMoveY(_perk.ParabolaOffset, flightTime / 2)
                .SetEase(paraboleEase)
                .OnComplete(() => sizeContainer
                        .DOLocalMoveY(0, flightTime / 2)
                        .SetEase(paraboleEase));
        }


        void BackToOwner()
        {
            backToOwner = true;
            
            var dist = Vector2.Distance(_perk.transform.position, moveContainer.position);
            var flightTime = dist / _perk.FlightSpeed;

            moveContainer
                .DOLocalMoveX(0, flightTime)
                .SetEase(moveEase)
                .OnComplete(Disable);

            sizeContainer
                .DOLocalMoveY(-_perk.ParabolaOffset, flightTime / 2)
                .SetEase(paraboleEase)
                .OnComplete(() => sizeContainer
                        .DOLocalMoveY(0, flightTime / 2)
                        .SetEase(paraboleEase));
        }

        void Disable() => ReturnToPool();

        protected override void OnUpdate()
        {
            z += _perk.RotateSpeed * Time.deltaTime;
            image.transform.rotation = Quaternion.Euler(0, 0, z);

            if (backToOwner)
                transform.position = _perk.transform.position;
        }
    }
}