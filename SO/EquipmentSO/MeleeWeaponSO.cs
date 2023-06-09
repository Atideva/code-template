using Gameplay;
using Gameplay.Units.UnitWeapons;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SO.EquipmentSO
{
    [CreateAssetMenu(fileName = "New MeleeWeapon", menuName = "Equipment/MeleeWeapon")]
    public class MeleeWeaponSO : WeaponSO
    {
        public override Weapon Prefab => meleePrefab;

        [Space(20)]
        [SerializeField] [Required()] MeleeWeapon meleePrefab;

        [Space(10)]
        [SerializeField] bool burstMode;
        [ShowIf("burstMode", "burstMode")]
        [SerializeField] int burstTimes;
        [ShowIf("burstTimes", "burstMode")]
        [SerializeField] float burstDuration;
    }
}