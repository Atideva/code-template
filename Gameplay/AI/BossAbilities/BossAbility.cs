using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.AI.BossAbilities
{
    public abstract class BossAbility : MonoBehaviour
    {
        [SerializeField] string animName;
        [SerializeField] float animTime;
        [SerializeField] bool enable;
        [SerializeField] float enableDelay;
        [SerializeField] float cooldown;
        [Header("DEBUG")]
        [ReadOnly] [SerializeField] bool isReady;
        [ReadOnly] [SerializeField] float cdTimer;
        [ReadOnly] public bool inUse;

        protected BossAI AI;
        public bool IsEnabled => enable;
        public string AnimName => animName;
        public bool IsReady => isReady;
        public float AnimTime => animTime;

        public void Init(BossAI ai, float delay)
        {
            AI = ai;
            isReady = true;
            if (enable && enableDelay > 0)
            {
                enable = false;
                Invoke(nameof(Enable), enableDelay + delay);
            }
        }

        void Enable() => enable = true;

        protected void Started()
        {
            isReady = false;
            inUse = true;
        }

        protected void Finish()
        {
            OnFinish();
            cdTimer = cooldown;
            inUse = false;
            onCooldown = true;
        }

        bool onCooldown;
        public abstract void Use();
        public event Action OnFinish = delegate { };

        void FixedUpdate()
        {
            if (!onCooldown) return;

            if (cdTimer > 0)
            {
                cdTimer -= Time.fixedDeltaTime;
            }
            else
            {
                isReady = true;
                onCooldown = false;
            }
        }
    }
}