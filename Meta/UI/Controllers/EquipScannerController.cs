using System;
using GameManager;
using Meta.Data;
using Meta.Enums;
using Meta.UI.Popups;
using UnityEngine;

namespace Meta.UI.Controllers
{
    public class EquipScannerController : MonoBehaviour
    {
        public EquipScanPopupUI popupUI;

        void Start()
        {
            EventsUI.Instance.OnShowItemScan += ShowItemScan;
            popupUI.OnEquipButton += OnEquipButton;
            popupUI.OnUnequipButton += OnUnequipButton;
            popupUI.OnDestroyButton += OnDestroyButton;
        }

        void OnDisable()
        {
            EventsUI.Instance.OnShowItemScan -= ShowItemScan;
            popupUI.OnEquipButton -= OnEquipButton;
            popupUI.OnUnequipButton -= OnUnequipButton;
            popupUI.OnDestroyButton -= OnDestroyButton;
        }

        void OnDestroyButton(EquipmentData equip, EquipPlaceEnum from)
        {
            popupUI.Hide();
            EventsUI.Instance.DestroyEquip(equip, from);
        }

        void OnEquipButton(EquipmentData equip)
        {
            popupUI.Hide();
            EventsUI.Instance.CharacterEquip(equip);
        }

        void OnUnequipButton(EquipmentData equip)
        {
            popupUI.Hide();
            EventsUI.Instance.CharacterUnequip(equip);
        }

        void ShowItemScan(EquipmentData equip)
        {
            var scan = new EquipScanData
            {
                Equip = equip
            };

            popupUI.Refresh(scan);
            popupUI.Show();
        }
    }
}