using System;
using GameManager;
using Meta.Data;
using Meta.Enums;
using Meta.Facade;
using UnityEngine;

namespace Meta.UI.Controllers
{
    public class EquipActionController : MonoBehaviour
    {
        void Start()
        {
            EventsUI.Instance.OnCharacterEquip += Equip;
            EventsUI.Instance.OnCharacterUnequip += Unequip;
            EventsUI.Instance.OnDestroyEquip += Destroy;
        }

        void OnDisable()
        {
            EventsUI.Instance.OnCharacterEquip -= Equip;
            EventsUI.Instance.OnCharacterUnequip -= Unequip;
            EventsUI.Instance.OnDestroyEquip -= Destroy;
        }

        void Equip(EquipmentData newEquip)
        {
            var type = newEquip.Type;

            if (Character.Has(type))
            {
                Inventory.Add(
                    Character.CurrentEquip(type));
                Character.UnequipCurrent(type);
            }

            Character.Equip(newEquip);
            Inventory.Remove(newEquip);
        }

        void Unequip(EquipmentData current)
        {
            Inventory.Add(current);
            Character.Unequip(current);
        }

        void Destroy(EquipmentData equip, EquipPlaceEnum from)
        {
            switch (from)
            {
                case EquipPlaceEnum.Character:
                    Character.Unequip(equip);
                    break;
                case EquipPlaceEnum.Inventory:
                    Inventory.Remove(equip);
                    break;
                default:
                    Log.Error("EquipPlaceEnum miss");
                    break;
            }
        }
    }
}