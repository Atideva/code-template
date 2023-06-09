using Sirenix.OdinInspector;
using UnityEngine;
// ReSharper disable InconsistentNaming

namespace SO.ScenesSO
{
    [CreateAssetMenu(fileName = "New Creeps List", menuName = "Gameplay/Levels/Creeps List", order = 1000)]
    public class SceneBonusChestSO : ScriptableObject
    {
        [Space(20)]
        [SerializeField] [ShowInInspector, InlineEditor(InlineEditorObjectFieldModes.Boxed)]
        SceneBonusChestsSO from0To5min;
       
        [Space(20)]
        [SerializeField] [ShowInInspector, InlineEditor(InlineEditorObjectFieldModes.Boxed)]
        SceneBonusChestsSO from5To10min;
       
        [Space(20)]
        [SerializeField] [ShowInInspector, InlineEditor(InlineEditorObjectFieldModes.Boxed)]
        SceneBonusChestsSO from10To15min;
        
        public SceneBonusChestsSO From0To5min => from0To5min;
        public SceneBonusChestsSO From5To10min => from5To10min;
        public SceneBonusChestsSO From10To15min => from10To15min;
    }
}