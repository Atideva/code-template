namespace Gameplay.Units.UnitWeapons
{
    public abstract class AutoAttackWeapon : Weapon
    {
        protected override void OnEnabled()
        {
            OnAttackReady += PerformAttack;
        }

        protected override void OnDisabled()
        {
            OnAttackReady -= PerformAttack;
        }
        
        protected abstract void Attack();
        void PerformAttack()
        {
            Cooldown();
            Attack();
        }
    }
}