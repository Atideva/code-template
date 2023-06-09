using System.Collections.Generic;
using SO.PerksSO;

namespace Meta.Data
{
    [System.Serializable]
    public class HeroPerksData
    {
        public List<ActivePerkSO> activeList = new();
        public List<PassivePerkSO> passiveList = new();
        public List<PerkData> activeOwned = new();
        public List<PerkData> passiveOwned = new();
 
        public void Add(PerkData data)
        {
            if (data.so is ActivePerkSO)
            {
                activeOwned.Add(data);
            }

            if (data.so is PassivePerkSO)
            {
                passiveOwned.Add(data);
            }
        }
    }
}