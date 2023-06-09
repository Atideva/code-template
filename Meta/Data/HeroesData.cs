using System.Collections.Generic;
using Meta.Interface;

namespace Meta.Data
{
    [System.Serializable]
    public class HeroesData : DebugDataSO, ISave, ISerialize
    {
        public HeroCardData selected;
        public List<HeroCardData> heroes = new();
    }
}