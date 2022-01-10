using System;
using Parabola;
using UnityEngine;

namespace PlayerScripts
{
    [ExecuteInEditMode]
    public class PlayerMove : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Transform player;
        [SerializeField] private float jumpHeight;
        [SerializeField] private float jumpDuration;
        [Range(0f, 0.3f)]
        [SerializeField] private float jumpThreshold;

        private float _jumpDuration, _jumpTimeElapse;
        private Vector3 _startPos, _endPos;
        public event Action OnLandAtCube = delegate { };


        public bool IsJumping { get; private set; }
        public bool NextJumpAllowed { get; private set; }


        private void Update()
        {
            if (IsJumping)
            {
                _jumpTimeElapse += Time.deltaTime;

                var prc = _jumpTimeElapse / _jumpDuration;

                if (prc >= (1 - jumpThreshold) && !NextJumpAllowed)
                {
                    NextJumpAllowed = true;
                    OnLandAtCube();
                }

                if (prc >= 1)
                {
                    IsJumping = false;
                }

                player.position = MathParabola.Parabola(_startPos, _endPos, jumpHeight, prc);
            }
        }

        public void Jump(Vector3 jumpPos, float spdMult)
        {
            IsJumping = true;
            NextJumpAllowed = false;

            _jumpTimeElapse = 0;
            _jumpDuration = jumpDuration / spdMult;

            _startPos = player.position;
            _endPos = jumpPos;
        }
    }
}