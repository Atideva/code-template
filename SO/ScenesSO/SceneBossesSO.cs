using Sirenix.OdinInspector;
using SO.UnitsSO;
using UnityEngine;

// ReSharper disable InconsistentNaming

namespace SO.ScenesSO
{
    [CreateAssetMenu(fileName = "New Boss List", menuName = "Gameplay/Levels/Boss List", order = 1000)]
    public class SceneBossesSO : ScriptableObject
    {
        [Space(20)]
        [SerializeField] [ShowInInspector, InlineEditor(InlineEditorObjectFieldModes.Boxed, Expanded = true)]
        BossSO at5min;

        [SerializeField] [ShowInInspector, InlineEditor(InlineEditorObjectFieldModes.Boxed, Expanded = true)]
        BossSO at10min;

        [SerializeField] [ShowInInspector, InlineEditor(InlineEditorObjectFieldModes.Boxed, Expanded = true)]
        BossSO at15min;


        public BossSO At5minute => at5min;
        public BossSO At10minute => at10min;
        public BossSO At15minute => at15min;
        
    }
}