using DG.Tweening;
using Meta.Facade;
using UnityEngine;
using Utilities.MonoCache.System;
using Utilities.Pools;
using static Utilities.Extensions.MathParabola;

namespace Gameplay.Perks.Active.Content
{
    public class Canister : PoolObject
    {
        [SerializeField] CanisterCollider dmgCollider;
        [SerializeField] GameObject fireWallParticle;
        [SerializeField] GameObject explosionParticle;
        [SerializeField] Transform canister;
        [SerializeField] Transform sizeContainer;
        [SerializeField] Transform shadow;
        [SerializeField] SpriteRenderer canisterImage;

        [Header("Throw anim")]
        [SerializeField] float throwDur = 0.6f;
        [SerializeField] float height = 5;
        [SerializeField] float sizeTrigger = 0.6f;
        [SerializeField] float size = 1.5f;
        [SerializeField] Vector3 rot = new(0, 0, 300);

        Vector2 _fromPos;
        float _timer;
        bool _fired;
        float _interval;
        CanisterPerk _perk;

        public void Throw(Vector3 fromPos, float lifeTime)
        {
            _fired = false;
            canisterImage.enabled = true;
            _timer = _interval;
            _fromPos = fromPos;

            fireWallParticle.SetActive(false);
            dmgCollider.Disable();

            DOVirtual.Float(0, 1, throwDur, Callback).SetEase(Ease.OutSine);

            canister
                .DOLocalRotate(rot, throwDur * 0.5f)
                .SetRelative(true)
                .SetLoops(2, LoopType.Incremental)
                .SetEase(Ease.Linear);

            canister.DOScale(size, throwDur * sizeTrigger).OnComplete(()
                => canister.transform.DOScale(1, throwDur * (1 - sizeTrigger)));

            shadow.position = fromPos;
            shadow.DOMove(transform.position, throwDur);
            shadow.DOScale(size * 1.2f, throwDur * sizeTrigger).OnComplete(()
                => shadow.transform.DOScale(1, throwDur * (1 - sizeTrigger)));

            Invoke(nameof(Fire), throwDur);
            Invoke(nameof(Disable), throwDur + lifeTime);
        }

        void Fire()
        {
            Audio.Play(_perk.Sound);
            canisterImage.enabled = false;
            _fired = true;
            fireWallParticle.Enable();
            explosionParticle.Enable();
        }


        protected override void OnFixedUpdate()
        {
            if (!_fired) return;

            _timer -= Time.fixedDeltaTime;
            if (_timer > 0) return;

            _timer = _interval;
            dmgCollider.Enable();
        }

        void Disable()
        {
            _fired = false;
            fireWallParticle.SetActive(false);
            dmgCollider.Disable();
            ReturnToPool();
        }

        void Callback(float v)
            => canister.transform.position
                = Parabola(_fromPos, (Vector2) transform.position, height, v);

        public void SetPerk(CanisterPerk perk)
        {
            _perk = perk;
            dmgCollider.SetPerk(perk);
        }

        public void SetWidth(float width) => sizeContainer.localScale = new Vector3(width, width, width);
        public void SetInterval(float interval) => _interval = interval;
    }
}