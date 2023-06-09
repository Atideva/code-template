using Gameplay.Units.UnitComponents;

namespace Gameplay.Interface
{
    public interface IHasHitpoints
    {
        Hitpoints Hitpoints { get; }

        void Heal(float value);

        void TakeDamage(float value);
    }
}