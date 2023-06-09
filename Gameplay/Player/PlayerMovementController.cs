using System;
using Gameplay.UI;
using Gameplay.Units;
using Gameplay.Units.UnitComponents.Move;
using Plugins.Joystick.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.MonoCache;

namespace Gameplay.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] [Required] Unit unit;
        [SerializeField] [ReadOnly] MoveEngine moveEngine;
        [SerializeField] [ReadOnly] Joystick variableJoystick;

        void Awake()
        {
            moveEngine = unit.Movement;
            variableJoystick = GameplayUI.Instance.Joystick;
        }

        void LateUpdate()
        {
            if (variableJoystick.Direction == Vector2.zero)
            {
                moveEngine.Stop();
                return;
            }

            moveEngine.SetDirection(variableJoystick.Direction);
            moveEngine.Move();
        }
    }
}