using System.Collections.Generic;
using Meta.Interface;

namespace Meta.Data
{
    [System.Serializable]
    public   class SceneGameplayData: ISave, ISerialize
    {
        public HeroSaveData hero;
        public List<SceneUnitsData> units = new();
    }
}