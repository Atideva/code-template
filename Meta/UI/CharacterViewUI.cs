using DG.Tweening;
using Gameplay.Units;
using Meta.Data;
using Meta.Enums;
using Meta.Facade;
using SO.UnitsSO;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities.MonoCache.System;

namespace Meta.UI
{
    public class CharacterViewUI : MonoBehaviour
    {
        [FormerlySerializedAs("characterContainer")] [Header("Character")]
        [SerializeField] Transform container;
        UnitModel _model;
        [SerializeField] float modelSize = 300f;
        [Header("Stats")]
        public HeroStatUI damage;
        public HeroStatUI hitpoints;
        [Header("Slots")]
        public EquipSlotUI weaponSlot;
        public EquipSlotUI necklaceSlot;
        public EquipSlotUI glovesSlot;
        public EquipSlotUI helmSlot;
        public EquipSlotUI chestSlot;
        public EquipSlotUI bootsSlot;

        public void SetCharacter(HeroSO so, int lvl)
        {
            if (_model)
                Destroy(_model.gameObject);

            if (!so.CharacterView) return;

            _model = Instantiate(so.CharacterView, container);
            _model.transform.localScale = new Vector3(modelSize, modelSize, modelSize);
            _model.Idle();

            var bonus = so.GetBonus(lvl);
            var dmgBonus = bonus.attackBonus;
            var hpBonus = bonus.hpBonus;
            
            var dmg = so.BaseDamage * (1 + dmgBonus*0.01f);
            damage.SetValue(dmg);

            var hp = so.Hitpoints * (1 + hpBonus*0.01f);
            hitpoints.SetValue(hp);
        }

        public void EmptySlot(EquipEnum equip)
        {
            switch (equip)
            {
                case EquipEnum.Weapon:
                    weaponSlot.Empty();
                    return;
                case EquipEnum.Necklace:
                    necklaceSlot.Empty();
                    return;
                case EquipEnum.Gloves:
                    glovesSlot.Empty();
                    return;
                case EquipEnum.Helm:
                    helmSlot.Empty();
                    return;
                case EquipEnum.Vest:
                    chestSlot.Empty();
                    return;
                case EquipEnum.Boots:
                    bootsSlot.Empty();
                    return;
                case EquipEnum.None:
                    break;
                default:
                    Log.EquipMiss();
                    break;
            }
        }

        public void PutToSlot(EquipmentData equip)
        {
            switch (equip.Type)
            {
                case EquipEnum.Weapon:
                    weaponSlot.PutItem(equip);
                    return;
                case EquipEnum.Necklace:
                    necklaceSlot.PutItem(equip);
                    return;
                case EquipEnum.Gloves:
                    glovesSlot.PutItem(equip);
                    return;
                case EquipEnum.Helm:
                    helmSlot.PutItem(equip);
                    return;
                case EquipEnum.Vest:
                    chestSlot.PutItem(equip);
                    return;
                case EquipEnum.Boots:
                    bootsSlot.PutItem(equip);
                    return;
                case EquipEnum.None:
                    Debug.Log("Character slot empty");
                    return;
                default:
                    Log.EquipMiss();
                    return;
            }
        }

        public void ShowModel()
        {
            if (!_model) return;
            _model.gameObject.SetActive(true);
        }

        public void HideModel()
        {
            if (!_model) return;
            _model.gameObject.SetActive(false);
        }
    }
}