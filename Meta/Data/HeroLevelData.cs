using System.Collections.Generic;

namespace Meta.Data
{
    [System.Serializable]
    public class HeroLevelData
    {
        public int level;
        public float experience;
        public float require;
        public List<float> table = new();
    }
}