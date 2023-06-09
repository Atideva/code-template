using System.Collections.Generic;
using Gameplay.AI.BossAbilities;
using Gameplay.Perks.Active.Content;
using Gameplay.Units;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.AI
{
    public class BossAI : AI
    {
        [SerializeField] float enableDelay = 2;
        [SerializeField] [ReadOnly] List<BossAbility> abilities = new();
         BossModel _model;
        bool _inProcess;
        float _animTimer;
        bool isEnabled;

        public BossModel Model => _model;

        public void Add(BossAbility ability)
        {
            var abi = Instantiate(ability, transform);
            abilities.Add(abi);
        }

        protected override void OnInit()
        {
            if (Unit.Model is BossModel m)
            {
                _model = m;
            }

            Invoke(nameof(Enable), enableDelay);
        }


        void Enable() => isEnabled = true;

        public void InitAbilities()
        {
            foreach (var ability in abilities)
            {
                ability.Init(this, enableDelay);
                if (ability is IKnowMovement mov)
                    mov.SetMovement(Unit.Movement);
                if (ability is IKnowPlayer p)
                    p.SetPlayer(Scene.Instance.Player);
            }
        }


        void FixedUpdate()
        {
            if (!isEnabled) return;

            if (_inProcess)
            {
                _animTimer -= Time.fixedDeltaTime;
                if (_animTimer < 0)
                {
                    _inProcess = false;
                    Follow.Follow();
                }

                return;
            }

            foreach (var ability in abilities)
            {
                if (ability.IsEnabled && ability.IsReady)
                {
                    ability.Use();
                    
                    StopFollow(ability.AnimTime);
                    _model.PlayAnimation(ability.AnimName, ability.AnimTime);
                    break;
                }
            }
        }

        void StopFollow(float duration)
        {
            Follow.Stop();
       
            _animTimer = duration;
            _inProcess = true;
        }
    }
}