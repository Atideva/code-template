using GameManager;
using MenuTabsUI;
using Meta.Data;
using Meta.Enums;
using Meta.Facade;
using Meta.Save.Storage;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Meta.UI.Controllers
{
    public class CharacterViewController : MonoBehaviour
    {
        [SerializeField] CharacterViewUI ui;
        [SerializeField] Tab characterTab;

        [ReadOnly] [SerializeField] EquipmentData weaponSlot;
        [ReadOnly] [SerializeField] EquipmentData necklaceSlot;
        [ReadOnly] [SerializeField] EquipmentData glovesSlot;
        [ReadOnly] [SerializeField] EquipmentData helmSlot;
        [ReadOnly] [SerializeField] EquipmentData chestSlot;
        [ReadOnly] [SerializeField] EquipmentData bootsSlot;

        HeroesStorage _storage;

        void Start()
        {
            Character.OnEquip += PutToSlot;
            Character.OnUnequip += EmptySlot;
            EventsUI.Instance.OnRefreshHeroUI += RefreshUI;
            
            _storage = Game.Instance.Storage.Heroes;
            _storage.OnSelected += RefreshUI;

            characterTab.OnTabEnabled += ShowModel;
            characterTab.OnTabDisabled += HideModel;
            HideModel();

            LoadItems();
        }

        void ShowModel() => ui.ShowModel();
        void HideModel() => ui.HideModel();

        void RefreshUI() => ui.SetCharacter(_storage.Current.so, _storage.Current.lvl);

        void OnDisable()
        {
            Character.OnEquip -= PutToSlot;
            Character.OnUnequip -= EmptySlot;
            EventsUI.Instance.OnRefreshHeroUI -= RefreshUI;
            _storage.OnSelected -= RefreshUI;
            
            characterTab.OnTabEnabled -= ShowModel;
            characterTab.OnTabDisabled -= HideModel;
        }

        void LoadItems()
        {
            ui.SetCharacter(_storage.Current.so, _storage.Current.lvl);
            ui.PutToSlot(Character.Weapon);
            ui.PutToSlot(Character.Necklace);
            ui.PutToSlot(Character.Gloves);
            ui.PutToSlot(Character.Helm);
            ui.PutToSlot(Character.Chest);
            ui.PutToSlot(Character.Boots);
            Refresh();
        }

        void PutToSlot(EquipmentData equip)
        {
            ui.PutToSlot(equip);
            Refresh();
        }

        void EmptySlot(EquipEnum type)
        {
            ui.EmptySlot(type);
            Refresh();
        }

        void Refresh()
        {
            weaponSlot = ui.weaponSlot.itemUI.Data;
            necklaceSlot = ui.necklaceSlot.itemUI.Data;
            glovesSlot = ui.glovesSlot.itemUI.Data;
            helmSlot = ui.helmSlot.itemUI.Data;
            chestSlot = ui.chestSlot.itemUI.Data;
            bootsSlot = ui.bootsSlot.itemUI.Data;
        }
    }
}