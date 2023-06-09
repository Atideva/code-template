using System.Collections.Generic;
using Meta.Interface;

namespace Meta.Data
{
    [System.Serializable]
    public class InventoryEquipmentData : DebugDataSO, ISave, ISerialize
    {
        public List<EquipmentData> weapons = new();
        public List<EquipmentData> necklaces = new();
        public List<EquipmentData> gloves = new();
        public List<EquipmentData> helms = new();
        public List<EquipmentData> vests = new();
        public List<EquipmentData> boots = new();

        public List<EquipmentData> AllEquip
        {
            get
            {
                var newList = new List<EquipmentData>();
                newList.AddRange(weapons);
                newList.AddRange(necklaces);
                newList.AddRange(gloves);
                newList.AddRange(helms);
                newList.AddRange(vests);
                newList.AddRange(boots);
                return newList;
            }
        }
    }
}