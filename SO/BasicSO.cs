using System.Drawing;
using I2.Loc;
using NaughtyAttributes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SO
{
    public class BasicSO : ScriptableObject //, IDatabaseItem 
    {
        [PreviewField(ObjectFieldAlignment.Left, Height = 80)]
       
        [HorizontalGroup("Split", 80), HideLabel]
        [SerializeField, Required] Sprite icon;
 
        [VerticalGroup("Split/Right"), LabelWidth(80), LabelText("Name")]
        [SerializeField] LocalizedString nameLabel;

        [VerticalGroup("Split/Right"), LabelWidth(80)]
        [SerializeField] LocalizedString description;
      
        public Sprite Icon => icon;
        public LocalizedString Name => nameLabel;
        public LocalizedString Description => description;
    }
}