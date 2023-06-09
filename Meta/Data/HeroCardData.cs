using Sirenix.OdinInspector;
using SO.UnitsSO;

namespace Meta.Data
{
    [System.Serializable]
    public class HeroCardData : GuidData
    {
        [ShowInInspector, InlineEditor(InlineEditorObjectFieldModes.Boxed)]
        public HeroSO so;
        public int lvl = 1;
        public bool owned;
    }
}