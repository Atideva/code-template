using Gameplay.Units.UnitComponents.Move;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.MonoCache;

namespace Gameplay.Units.UnitComponents
{
    public class MoveDirectionMarker : MonoCache
    {
        [SerializeField] [Required] Unit unit;
        MoveEngine _moveEngine;
        Transform t;

        void Start()
        {
            t = transform;
            if (!unit) return;
            _moveEngine = unit.Movement;
        }

        protected override void OnFixedUpdate()
        {
            if (!_moveEngine) return;
            if (!_moveEngine.IsMoving) return;
            t.up = _moveEngine.Direction;
        }
    }
}