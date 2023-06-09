using Gameplay.Units.UnitComponents;

namespace Gameplay.Perks.Active.Content
{
    public interface IKnowHitpoints
    {
        Hitpoints Hitpoints { get; }
        void SetHitpoints(Hitpoints hitpoints);
    }
}