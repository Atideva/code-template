namespace Gameplay.Units.UnitComponents.Move
{
    public class AutoMoveForward : Movement
    {
        protected override void OnUpdate()
        {
            SetDirection(MovingTransform.right);
            Move();
        }
    }
}