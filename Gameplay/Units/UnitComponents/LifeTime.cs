using System;
using UnityEngine;
using Utilities.MonoCache;

namespace Gameplay.Units.UnitComponents
{
    public class LifeTime : MonoCache
    {
        [SerializeField] float lifeTime;
        bool Expired { get; set; }

        public void Set(float newLifeTime)
        {
            Expired = false;
            lifeTime = newLifeTime;
        }

        public event Action OnExpire = delegate { };

        protected override void OnFixedUpdate()
        {
            lifeTime -= Time.fixedDeltaTime;
            if (lifeTime > 0) return;

            if (!Expired)
            {
                Expired = true;
                OnExpire();
            }
        }
    }
}