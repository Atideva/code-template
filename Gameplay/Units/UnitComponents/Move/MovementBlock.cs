using UnityEngine;
using Utilities.MonoCache;

namespace Gameplay.Units.UnitComponents.Move
{
    public class MovementBlock : MonoCache
    {
        [SerializeField] Unit unit;
        [SerializeField] float raycastDistance = 1.5f;
        MoveEngine move;
        [SerializeField] LayerMask layer;

        void Start()
        {
            move = unit.Movement;
        }

        protected override void OnUpdate()
        {
            //Debug.DrawRay(transform.position, move.Direction * raycastDistance, Color.red);
            var hit = Physics2D.Raycast(transform.position, move.Direction, raycastDistance, layer);
            move.Block = hit;
        }
    }
}