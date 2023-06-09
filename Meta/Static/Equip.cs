using Meta.Data;
using Meta.Enums;
using Meta.Facade;
using SO.EquipmentSO;
using UnityEngine;

namespace Meta.Static
{
    public static class Equip
    {
        public static bool NotNull(EquipmentData equip)
            => !IsNull(equip);

        public static bool IsNull(EquipmentData equip)
            => equip == null || !equip.so;

        public static EquipEnum Type(EquipSO so)
            => so switch
            {
                null => EquipEnum.None,
                WeaponSO => EquipEnum.Weapon,
                NecklaceSO => EquipEnum.Necklace,
                GlovesSO => EquipEnum.Gloves,
                HelmSO => EquipEnum.Helm,
                VestSO => EquipEnum.Vest,
                BootsSO => EquipEnum.Boots,
                _ => Error.EquipMiss
            };

        public static Sprite Icon(EquipmentData equip)
        {
            if (NotNull(equip))
                return equip.so.Icon;
            Log.NullEquip();
            return null;

        }

        public static string Name(EquipmentData equip)
        {
            if (IsNull(equip))
                return Error.EquipEmptyString;
            return equip.so.Name;
        }

        // public static EquipmentData New(IDatabaseItem item)
        //     => item switch
        //     {
        //         WeaponSO weaponSO => new EquipmentData {so = weaponSO},
        //         NecklaceSO necklaceSO => new EquipmentData {so = necklaceSO},
        //         GlovesSO glovesSO => new EquipmentData {so = glovesSO},
        //         HelmSO helmSO => new EquipmentData {so = helmSO},
        //         BootsSO bootsSO => new EquipmentData {so = bootsSO},
        //         VestSO chestSO => new EquipmentData {so = chestSO},
        //         _ => Error.NullEquip
        //     };
        public static EquipmentData NewData(EquipSO item)
            => item switch
            {
                WeaponSO weaponSO => new EquipmentData {so = weaponSO},
                NecklaceSO necklaceSO => new EquipmentData {so = necklaceSO},
                GlovesSO glovesSO => new EquipmentData {so = glovesSO},
                HelmSO helmSO => new EquipmentData {so = helmSO},
                BootsSO bootsSO => new EquipmentData {so = bootsSO},
                VestSO chestSO => new EquipmentData {so = chestSO},
                _ => Error.NullEquip
            };
    }
}