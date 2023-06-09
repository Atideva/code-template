using System;
using Gameplay.Animations;
using Meta.Facade;
using UnityEngine;
using Utilities.MonoCache.System;

namespace Gameplay.AI.BossAbilities
{
    public class Clamp : BossAbility
    {
        [SerializeField] float damage;
        [SerializeField] float dmgDelay;
        [SerializeField] CircleCollider2D circleCollider;
        [SerializeField] Vfx vfx;
        [SerializeField] float vfxDelay = 0.3f;

        public override void Use()
        {
            Started();

            VFX.Play(vfx, transform.position, vfxDelay);
            Invoke(nameof(EnableCollider), dmgDelay);
            Invoke(nameof(Finish), AnimTime);
        }

        public void EnableCollider()
        {
            circleCollider.enabled = true;
            Invoke(nameof(Disable), Time.fixedDeltaTime * 2);
        }

        public void Disable()
        {
            circleCollider.enabled = false;
        }

        void OnTriggerEnter2D(Collider2D enemy) => Check(enemy);

        void Check(Collider2D enemy)
        {
            var player = Scene.Instance.Player.Hero;
            if (!player) return;
            if (enemy.transform != player.transform) return;
            player.TakeDamage(damage);
        }
    }
}