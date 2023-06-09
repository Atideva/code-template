using System;
using GameManager;
using Meta.Data;
using Meta.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Meta.Facade.Character;
using static Meta.Static.Equip;
using static Meta.Static.Item;

namespace Meta.UI.Popups
{
    public class EquipScanPopupUI : PopupUI
    {
        public ItemUI equipItemUI;
      
        [Header("Buttons")]
        public Button equipButton;
        public Button unequipButton;
        public Button levelUpButton;
        public Button maxLevelButton;
        public Button destroyButton;
       
        [Header("Texts")]
        public TextMeshProUGUI equipName;
        public TextMeshProUGUI tierHeaderName;
        public TextMeshProUGUI equipDescription;
        public TextMeshProUGUI levelsText;
        public TextMeshProUGUI statText;
        public TextMeshProUGUI goldCost;
        public TextMeshProUGUI recipesCost;
        public TextMeshProUGUI equipUnEquipText;
      
        [Header("Images")]
        public Image statIcon;
        public Image tierHeaderImage;

        bool ItemEquiped { get; set; }
        EquipmentData ScannedEquip { get; set; }
        public event Action<EquipmentData> OnEquipButton = delegate { };
        public event Action<EquipmentData, EquipPlaceEnum> OnDestroyButton = delegate { };
        public event Action<EquipmentData> OnUnequipButton = delegate { };

        void Start()
        {
            equipButton.onClick.AddListener(Equip);
            unequipButton.onClick.AddListener(Unequip);
            levelUpButton.onClick.AddListener(LevelUp);
            maxLevelButton.onClick.AddListener(MaxLevel);
            destroyButton.onClick.AddListener(OnDestroyClick);
        }

        void OnDestroy()
        {
            equipButton.onClick.RemoveListener(Equip);
            unequipButton.onClick.RemoveListener(Unequip);
            levelUpButton.onClick.RemoveListener(LevelUp);
            maxLevelButton.onClick.RemoveListener(MaxLevel);
            destroyButton.onClick.RemoveListener(OnDestroyClick);
        }

        void LevelUp()
        {
        }

        void MaxLevel()
        {
        }

        void OnDestroyClick()
        {
            OnDestroyButton(ScannedEquip, ItemEquiped
                ? EquipPlaceEnum.Character
                : EquipPlaceEnum.Inventory);
        }

        void Equip()
        {
            ItemEquiped = true; 
            OnEquipButton(ScannedEquip);
        }

        void Unequip()
        {
            ItemEquiped = false;
            OnUnequipButton(ScannedEquip);
        }
        
        public void Refresh(EquipScanData scan)
        {
            ScannedEquip = scan.Equip;

            ItemEquiped = IsEquip(ScannedEquip);
            var tier = ScannedEquip.so.Tier;
            var tierData = Game.Instance.Config.ItemTiers.Get(tier);
            var eqName = Name(ScannedEquip);
            var desc = ScannedEquip.so.Description;
            var uiData = NewUIData(ScannedEquip);

       //     equipUnEquipText.text = ItemEquiped ? "Unequip" : "Equip";
            if (ItemEquiped)
            {
                equipButton.gameObject.SetActive(false);
                unequipButton.gameObject.SetActive(true);
            }
            else
            {
                equipButton.gameObject.SetActive(true);
                unequipButton.gameObject.SetActive(false); 
            }
            
            equipName.text = eqName;
            tierHeaderName.text = tierData.englishName;
            equipDescription.text = desc;
            levelsText.text = "Level " + scan.Equip.lvl;
            statText.text = "???";
            goldCost.text = "???";
            recipesCost.text = "???";

            equipItemUI.Refresh(uiData);
        }
    }
}