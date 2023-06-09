using Sirenix.OdinInspector;
using UnityEngine;

namespace Meta.Data
{
    [System.Serializable]
    public class EquipmentTierData
    {
        [Required]
        [HorizontalGroup("Split", 60)]
        [PreviewField, HideLabel] public Sprite itemBackground;
        [VerticalGroup("Split/Right"), LabelWidth(50)]
        [HideLabel]
        // [LabelText("Name")]
        public string englishName;
        // public Color color;
    }
 
}