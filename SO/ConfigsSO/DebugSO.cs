using System.Collections.Generic;
using Meta.Data;
using Meta.Static;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SO.ConfigsSO
{
    [CreateAssetMenu(fileName = "DEBUG", menuName = "Config/Debug")]
    public class DebugSO : ScriptableObject
    {
        [InfoBox("Quick access to runtime data, for easy debug" )]
        [Space(20)] [Header("---DEBUG---")]
        [ReadOnly] [SerializeField] BankData bank;
        
        [Header("---DEBUG---") ]
        [ReadOnly] [SerializeField] List<OptionToggleData> options;

        [Header("---DEBUG---") ]
        [ReadOnly] [SerializeField] CharacterEquipmentData charEquipment;
        
        [Header("---DEBUG---") ]
        [ReadOnly] [SerializeField] List<string> inventoryEquipment;


        public void Refresh(DebugDataSO data)
        {
            if (data is BankData b)
            {
                bank.gem = b.gem;
                bank.gold = b.gold;
                bank.energy = b.energy;
                return;
            }

            if (data is OptionsData o)
            {
                options = new List<OptionToggleData>();
                foreach (var d in o.toggles)
                    options.Add(d);
                
                return;
            }

            if (data is InventoryEquipmentData ie)
            {
                inventoryEquipment = new List<string>();
                foreach (var d in ie.weapons)
                    inventoryEquipment.Add(Equip.Name(d));
                foreach (var d in ie.necklaces)
                    inventoryEquipment.Add(Equip.Name(d));
                foreach (var d in ie.gloves)
                    inventoryEquipment.Add(Equip.Name(d));
                foreach (var d in ie.helms)
                    inventoryEquipment.Add(Equip.Name(d));
                foreach (var d in ie.vests)
                    inventoryEquipment.Add(Equip.Name(d));
                foreach (var d in ie.boots)
                    inventoryEquipment.Add(Equip.Name(d));
                
                return;
            }

            if (data is CharacterEquipmentData ce)
            {
                charEquipment.weapon = ce.weapon;
                charEquipment.necklace = ce.necklace;
                charEquipment.gloves = ce.gloves;
                charEquipment.helm = ce.helm;
                charEquipment.vest = ce.vest;
                charEquipment.boots = ce.boots;
            }
        }
    }
}