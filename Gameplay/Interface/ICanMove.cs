using Gameplay.Units.UnitComponents;
using Gameplay.Units.UnitComponents.Move;

namespace Gameplay.Interface
{
    public interface ICanMove
    {
        MoveEngine Movement { get; }
        void SetMoveSpeed(float speed);
    }
    public interface IHasLifetime
    {
        LifeTime LifeTime { get; }
 
        void SetLifeTime(float duration);
    }
}