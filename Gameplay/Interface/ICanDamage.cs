namespace Gameplay.Interface
{
    public interface ICanDamage
    {
        float Damage { get; }
        void SetDamage(float damage);
        void DealDamage(IHasHitpoints hp);
    }
}