using System.Collections.Generic;
using Meta.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SO.ScenesSO
{
    [CreateAssetMenu(fileName = "New Bonus Chests Wave", menuName = "Gameplay/Levels/Bonus Chests Wave", order = 1000)]
    public class SceneBonusChestsSO : ScriptableObject
    {
        [Space(20)]
        [SerializeField] [ShowInInspector, InlineEditor(InlineEditorObjectFieldModes.Boxed)]
        SceneBonusChestsWaveSO from0To5min;
       
        [Space(20)]
        [SerializeField] [ShowInInspector, InlineEditor(InlineEditorObjectFieldModes.Boxed)]
        SceneBonusChestsWaveSO from5To10min;
       
        [Space(20)]
        [SerializeField] [ShowInInspector, InlineEditor(InlineEditorObjectFieldModes.Boxed)]
        SceneBonusChestsWaveSO from10To15min;
        
        public SceneBonusChestsWaveSO From0To5min => from0To5min;
        public SceneBonusChestsWaveSO From5To10min => from5To10min;
        public SceneBonusChestsWaveSO From10To15min => from10To15min;
    }
}