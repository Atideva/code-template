using UnityEngine;

namespace Meta.Data
{
    [System.Serializable]
    public class  HeroSaveData
    {
        public Vector3 position;
        public HeroLevelData levelData = new();
        public HeroPerksData perksData = new();
    }
}