using Sirenix.OdinInspector;
using UnityEngine;

// ReSharper disable InconsistentNaming

namespace SO.ScenesSO
{
    [CreateAssetMenu(fileName = "New Creeps List", menuName = "Gameplay/Levels/Creeps List", order = 1000)]
    public class SceneCreepsSO : ScriptableObject
    {
        [Space(20)]
        [SerializeField] [ShowInInspector, InlineEditor(InlineEditorObjectFieldModes.Boxed)]
        SceneEnemyWaveSO from0To5min;
       
        [Space(20)]
        [SerializeField] [ShowInInspector, InlineEditor(InlineEditorObjectFieldModes.Boxed)]
        SceneEnemyWaveSO from5To10min;
       
        [Space(20)]
        [SerializeField] [ShowInInspector, InlineEditor(InlineEditorObjectFieldModes.Boxed)]
        SceneEnemyWaveSO from10To15min;
        
        public SceneEnemyWaveSO From0To5min => from0To5min;
        public SceneEnemyWaveSO From5To10min => from5To10min;
        public SceneEnemyWaveSO From10To15min => from10To15min;
    }
}