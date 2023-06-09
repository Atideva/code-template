using UnityEngine;

namespace Gameplay.Units.UnitComponents.Move
{
    public class Movement : MoveEngine
    {
        protected Transform MovingTransform;

        protected override void Awake()
        {
            base.Awake();
            MovingTransform = transform;
        }


        public override void Move()
        {
            if (Block) return;

            IsMoving = true;
            var move = Direction.normalized * (Speed * Time.deltaTime);
            MovingTransform.position += (Vector3) move;
        }
    }
}