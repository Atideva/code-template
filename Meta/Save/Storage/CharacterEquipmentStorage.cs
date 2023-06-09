using System;
using Meta.Data;
using Meta.Enums;
using Meta.Facade;
using Meta.Save.SaveSystem;
using Meta.Static;
using SO.ConfigsSO;
using SO.EquipmentSO;
using UnityEngine;

namespace Meta.Save.Storage
{
    public class CharacterEquipmentStorage : Saveable<CharacterEquipmentData>
    {
        public void Init(FirstLaunchSO config)
        {
            if (PlayerPrefs.HasKey(SaveKey))
            {
                Load();
            }
            else
            {
                SaveableData = config.CharacterEquip;
                Save();
            }
        }

        #region Acceccors

        public bool IsEquip(EquipmentData equip)
            => equip.Type switch
            {
                EquipEnum.Weapon  => Weapon is { } && Weapon == equip,
                EquipEnum.Necklace => Necklace is { } && Necklace == equip,
                EquipEnum.Gloves  => Gloves is { } && Gloves == equip,
                EquipEnum.Helm  => Helm is { } && Helm == equip,
                EquipEnum.Vest => Vest is { } && Vest == equip,
                EquipEnum.Boots  => Boots is { } && Boots == equip,
                _ => Error.EquipFalse
            };


        public bool SlotEmpty(EquipEnum equip) 
            => equip switch
            {
                EquipEnum.Weapon => Weapon == null || !Weapon.so,
                EquipEnum.Necklace => Necklace == null || !Necklace.so,
                EquipEnum.Gloves => Gloves == null || !Gloves.so,
                EquipEnum.Helm => Helm == null || !Helm.so,
                EquipEnum.Vest => Vest == null || !Vest.so,
                EquipEnum.Boots => Boots == null || !Boots.so,
                _ => Error.EquipFalse
            };

        public EquipmentData Weapon => SaveableData.weapon;
        public EquipmentData Necklace => SaveableData.necklace;
        public EquipmentData Gloves => SaveableData.gloves;
        public EquipmentData Helm => SaveableData.helm;
        public EquipmentData Vest => SaveableData.vest;
        public EquipmentData Boots => SaveableData.boots;

        #endregion

        #region Actions

        public event Action<EquipmentData> OnEquip = delegate { };
        public event Action<EquipEnum> OnUnequip = delegate { };

        #endregion

        public void Equip(EquipmentData equip)
        {
            UnequipCurrent(equip.Type);
            EquipNew(equip);
            Save();
        }

        public void Unequip(EquipEnum type)
        {
            UnequipCurrent(type);
            Save();
        }

        void EquipNew(EquipmentData equip)
        {
            switch (equip.so)
            {
                case WeaponSO:
                    SaveableData.weapon = equip;
                    break;
                case NecklaceSO:
                    SaveableData.necklace = equip;
                    break;
                case GlovesSO:
                    SaveableData.gloves = equip;
                    break;
                case HelmSO:
                    SaveableData.helm = equip;
                    break;
                case VestSO:
                    SaveableData.vest = equip;
                    break;
                case BootsSO:
                    SaveableData.boots = equip;
                    break;
                default:
                    Log.EquipMiss();
                    break;
            }

            OnEquip(equip);
        }

        public EquipmentData GetEquip(EquipEnum type)
            => type switch
            {
                EquipEnum.Weapon => Weapon,
                EquipEnum.Necklace => Necklace,
                EquipEnum.Gloves => Gloves,
                EquipEnum.Helm => Helm,
                EquipEnum.Vest => Vest,
                EquipEnum.Boots => Boots,
                _ => Error.NullEquip
            };

        public void UnequipCurrent(EquipEnum type)
        {
            if (type == EquipEnum.None) return;
            if (SlotEmpty(type)) return;

            switch (type)
            {
                case EquipEnum.Weapon:
                    SaveableData.weapon = null;
                    break;
                case EquipEnum.Necklace:
                    SaveableData.necklace = null;
                    break;
                case EquipEnum.Gloves:
                    SaveableData.gloves = null;
                    break;
                case EquipEnum.Helm:
                    SaveableData.helm = null;
                    break;
                case EquipEnum.Vest:
                    SaveableData.vest = null;
                    break;
                case EquipEnum.Boots:
                    SaveableData.boots = null;
                    break;
                default:
                    Log.EquipMiss();
                    break;
            }

            OnUnequip(type);
        }
    }
}