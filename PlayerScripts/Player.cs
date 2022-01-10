using Gameplay;
using UnityEngine;

namespace PlayerScripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Vector3 standOffsetByY;
        [SerializeField] private int jumpStep;
        [SerializeField] private float jumpSpdMult;
        [SerializeField] private PlayerMove move;
        [SerializeField] private PlayerSounds sounds;
        [SerializeField] private PlayerController controller;
        [SerializeField] private PlayerLookAt lookAt;
        
        private int _imAtCubeId;
        private CubeList _cubeList;
        public PlayerMove Move => move;

        public void Init(CubeList cubeList, Vector3 startPos)
        {
            _cubeList = cubeList;
            Move.OnLandAtCube += OnLandAtCube;
            transform.position = startPos + standOffsetByY;
            controller.OnJumpButton += OnJumpButton;
        }

        private void OnJumpButton()
        {
            if (Move.IsJumping && !Move.NextJumpAllowed)
            {
                return;
            }

            _imAtCubeId += jumpStep;
            Debug.Log("Jump to: " + _imAtCubeId);
            
            var pos = _cubeList.GetCube(_imAtCubeId).transform.position + standOffsetByY;
            Move.Jump(pos, jumpSpdMult);
        }

        private void OnLandAtCube()
        {
            sounds.PlayStone();
            var target = _cubeList.GetCube(_imAtCubeId + lookAt.LookForwardBy).transform;
            lookAt.SetNewTarget(target);
        }
    }
}