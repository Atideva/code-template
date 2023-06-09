using System.Collections.Generic;
using Meta.Interface;

namespace Meta.Data
{
    [System.Serializable]
    public class CharacterEquipmentData : DebugDataSO, ISave, ISerialize
    {
        public EquipmentData weapon = new();
        public EquipmentData necklace = new();
        public EquipmentData gloves = new();
        public EquipmentData helm = new();
        public EquipmentData vest = new();
        public EquipmentData boots = new();

        public List<EquipmentData> AllEquip =>
            new()
            {
                weapon, necklace, gloves, helm, vest, boots
            };
    }
}