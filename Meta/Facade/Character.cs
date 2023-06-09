using System;
using GameManager;
using Meta.Data;
using Meta.Enums;
using Meta.Save.Storage;
using static Meta.Static.Equip;

namespace Meta.Facade
{
    public static class Character
    {
        static CharacterEquipmentStorage _equipmentStorage;

        #region Accecors

        static bool Null
        {
            get
            {
                if (_equipmentStorage) return false;
                Log.InitError(nameof(Character));
                return true;
            }
        }

        public static bool IsEquip(EquipmentData equip) =>
            !Null && _equipmentStorage.IsEquip(equip);

        public static bool Has(EquipEnum type)
        {
            return !SlotEmpty(type);
        }

        public static bool SlotEmpty(EquipEnum type)
            => !Null && _equipmentStorage.SlotEmpty(type);

        public static EquipmentData Weapon => Null ? null : _equipmentStorage.Weapon;
        public static EquipmentData Necklace => Null ? null : _equipmentStorage.Necklace;
        public static EquipmentData Gloves => Null ? null : _equipmentStorage.Gloves;
        public static EquipmentData Helm => Null ? null : _equipmentStorage.Helm;
        public static EquipmentData Chest => Null ? null : _equipmentStorage.Vest;
        public static EquipmentData Boots => Null ? null : _equipmentStorage.Boots;

        public static EquipmentData CurrentEquip(EquipEnum type)
            => _equipmentStorage.GetEquip(type);

        #endregion

        #region Actions

        public static event Action<EquipmentData> OnEquip = delegate { };
        public static event Action<EquipEnum> OnUnequip = delegate { };
        static void EquipCall(EquipmentData d) => OnEquip(d);
        static void UnequipCall(EquipEnum d) => OnUnequip(d);

        #endregion

        public static void Init(CharacterEquipmentStorage charEquip)
        {
            _equipmentStorage = charEquip;
            _equipmentStorage.OnEquip += EquipCall;
            _equipmentStorage.OnUnequip += UnequipCall;
        }

        public static void UnequipCurrent(EquipEnum type)
            => _equipmentStorage.UnequipCurrent(type);

        public static void Equip(EquipmentData equip)
        {
            if (IsNull(equip))
            {
                Log.NullEquip();
                return;
            }

            Log.CharacterEquip(Name(equip));
            _equipmentStorage.Equip(equip);
        }

        public static void Unequip(EquipmentData equip)
        {
            if (IsNull(equip))
            {
                Log.NullEquip();
                return;
            }

            Log.CharacterUnequip(Name(equip));
            _equipmentStorage.Unequip(equip.Type);
        }
    }
}