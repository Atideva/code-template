using DG.Tweening;
using Gameplay.Perks.Active.Content;
using Gameplay.Units.UnitComponents.Move;
using UnityEngine;
using Utilities.CharacterStats;

namespace Gameplay.AI.BossAbilities
{
    public class Charge : BossAbility, IKnowMovement
    {
        public float speedMultiplier = 2;
        //    public float duration = 1;
        float _lifetime;
        Vector3 _dir;
        Vector3 _pos;

        public override void Use()
        {
            Started();

            inUse = true;
            _pos = AI.Follow.Target.position;
            _dir = (_pos - transform.position).normalized;
            _lifetime = AnimTime;

            var spdBoost = new CharacterMod(speedMultiplier, StatModType.Percent);
            Move.Multiplier.AddModifier(spdBoost);

            DOVirtual.DelayedCall(AnimTime, () => StopSlow(spdBoost));
        }

        void StopSlow(CharacterMod slow)
            => Move.Multiplier.RemoveModifier(slow);

        void Update()
        {
            if (!inUse) return;
            Move.SetDirection(_dir);
            Move.Move();

            var dist = Vector2.Distance(_pos, transform.position);
            if (dist < 1f && inUse)
            {
                Finish();
            }

            _lifetime -= Time.deltaTime;
            if (_lifetime <= 0 && inUse)
            {
                Finish();
            }
        }

        public MoveEngine Move { get; private set; }
        public void SetMovement(MoveEngine move) => Move = move;
    }
}