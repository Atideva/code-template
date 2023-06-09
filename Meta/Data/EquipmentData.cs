using Meta.Enums;
using Meta.Static;
using Sirenix.OdinInspector;
using SO.EquipmentSO;

namespace Meta.Data
{
    [System.Serializable]
    public class EquipmentData : GuidData
    {
        // [ReadOnly] public int databaseID;
        
        [ShowInInspector, InlineEditor(InlineEditorObjectFieldModes.Boxed)]
        public EquipSO so;
        public int lvl = 1;
        public EquipEnum Type => Equip.Type(so);
    }
    
 }