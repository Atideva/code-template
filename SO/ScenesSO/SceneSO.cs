 
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.Odin;

namespace SO.ScenesSO
{
    [CreateAssetMenu(fileName = "New Level", menuName = "Gameplay/Levels/Level")]
    public class SceneSO : BasicSO, ISerializeReferenceByAssetGuid
    {
        [Space(20)]
        [SerializeField] [ShowInInspector, InlineEditor(InlineEditorObjectFieldModes.Boxed)]
        SceneBossesSO bosses;

        [Space(20)]
        [SerializeField] [ShowInInspector, InlineEditor(InlineEditorObjectFieldModes.Boxed)]
        SceneCreepsSO creeps;

        [Space(20)]
        [SerializeField] [ShowInInspector, InlineEditor(InlineEditorObjectFieldModes.Boxed)]
        SceneBonusChestsSO chest;
        
        public SceneBossesSO Bosses => bosses;

        public SceneCreepsSO Creeps => creeps;

        public SceneBonusChestsSO Chests => chest;
    }
}