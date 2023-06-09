using Gameplay;
using Gameplay.Units.UnitWeapons;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SO.EquipmentSO
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Equipment/Weapon")]
    public class WeaponSO : EquipSO
    {
        [Space(20)]
        [PropertyOrder(2)]
        [SerializeField] float damage = 1;
        [PropertyOrder(2)]
        [SerializeField] [Min(0.01f)] float attackSpeed = 1;

        public float Damage => damage;
        public float AttackSpeed
        {
            get => attackSpeed;
            set => attackSpeed = value;
        }

        public virtual Weapon Prefab => null;
    }
}