using Gameplay.Drop;
using Meta.Enums;
using Sirenix.OdinInspector;

namespace Meta.Data
{
    [System.Serializable]
    public class ExperienceDropData
    {
        public ExperienceDropEnum type;
         public ExperienceDrop prefab;
        public float experienceValue;
    }
}