using DG.Tweening;
using Meta.Facade;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.MonoCache.System;
using Utilities.Pools;

namespace Gameplay.Perks.Active.Content
{
    public class RpgRocket : PoolObject
    {
        [SerializeField] Transform size;
        [SerializeField] Transform container;
        
        [SerializeField] RpgRocketCollider dmgCollider;
        [SerializeField] ParticleSystem explosionVfx;
        [SerializeField] SpriteRenderer rocketImage;
        [SerializeField] Ease moveEase;
      
        [SerializeField] float moveTime = 0.7f;
        [SerializeField] float moveByY = 2;
        [SerializeField] float moveByYDuration = 0.5f;
        [SerializeField] float explosionVfxDuration = 0.5f;

        [SerializeField] [ReadOnly] float distance;
        RpgPerk _perk;

        void Disable()
        {
            explosionVfx.Disable();
            ReturnToPool();
        }

        public void Set(RpgPerk perk) => _perk = perk;
        public void SetAngle(float angle) => transform.localRotation = Quaternion.Euler(0, 0, angle);
        public void SetExplosionRadius(float radius) => size.localScale = new Vector3(radius, radius, radius);
        public void SetFlyDistance(float dist) => distance = dist;


        public void Launch()
        {
            rocketImage.enabled = true;
            explosionVfx.Disable();
            dmgCollider.Disable();

            var modelSize = rocketImage.transform.localScale;
            
            size.localPosition = Vector3.zero;
            container.localPosition = Vector3.zero;
            rocketImage.transform.localScale = Vector3.zero;

            rocketImage.transform.DOScale(modelSize, 0.15f);
            
            container
                .DOLocalMoveX(distance, moveTime)
                .SetEase(moveEase)
                .OnComplete(Explode);

            if (moveByY != 0)
                size.DOLocalMoveY(moveByY, moveByYDuration)
                    .SetDelay(0.1f)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.InOutQuad);
            
            Audio.Play(_perk.Sound);
        }


        void Explode()
        {
            rocketImage.enabled = false;
            size.DOKill();

            dmgCollider.SetDamage(_perk.Damage);
            dmgCollider.SetEnemyTag(_perk.Targets);
            dmgCollider.Enable();

            explosionVfx.Enable();
            Invoke(nameof(Disable), explosionVfxDuration);
        }
    }
}