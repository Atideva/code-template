using Gameplay.Units.UnitComponents.Move;

namespace Gameplay.Perks.Active.Content
{
    public interface IKnowMovement
    {
        MoveEngine Move { get; }
        void SetMovement(MoveEngine move);
    }
}