using SO.EquipmentSO;
using UnityEngine;

namespace Gameplay.Units.UnitWeapons
{
    public class MeleeWeapon : AutoAttackWeapon
    {
 
        [SerializeField] MeleeWeaponSO _config;

        public override void SetConfig(WeaponSO setConfig)
        {
            _config =(MeleeWeaponSO) setConfig;
        }

        protected override void Attack()
        {
         
        }
 
    }
}
