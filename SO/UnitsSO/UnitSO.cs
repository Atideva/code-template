using System.Collections.Generic;
using Gameplay.Animations;
using Gameplay.Units;
using Meta.Enums;
using Sirenix.OdinInspector;
using SO.EquipmentSO;
using UnityEngine;

namespace SO.UnitsSO
{
    [CreateAssetMenu(fileName = "New Unit", menuName = "Gameplay/Units/Unit")]
    public class UnitSO : BasicSO
    {
        [Space(20)] 
        [Required]
        [LabelText("[Model]")] [SerializeField] UnitModel model;
        [LabelText("[VFX] Death")] [SerializeField] Vfx deathVFX;

        [Space(20)]
        [SerializeField] bool hasWeapons;

        [ShowIf(nameof(hasWeapons))]
        [SerializeField] [InlineEditor()] List<WeaponSO> weapons = new() {null};

        [Space(20)]
        [SerializeField] float hitpoints;
        [SerializeField] float moveSpeed;
        
        public UnitModel Model => model;
        public float MoveSpeed => moveSpeed;
        public float Hitpoints => hitpoints;
        public Vfx DeathVFX => deathVFX;
        public bool HasWeapons => hasWeapons;
        public IReadOnlyList<WeaponSO> Weapons => weapons;
    }
}