using UnityEngine;

namespace Gameplay.Units.UnitComponents.Move
{
    // ReSharper disable once InconsistentNaming
    public class MovementRB : MoveEngine
    {
        [SerializeField] Rigidbody2D body;

        protected override void OnStop()
        {
            body.velocity = Vector2.zero;
        }

        public override void Move()
        {
            if (Block) return;

            IsMoving = true;
            var move = Direction.normalized * Speed;
            body.velocity = (Vector3) move;
        }
    }
}