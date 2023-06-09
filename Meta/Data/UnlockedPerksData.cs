using System.Collections.Generic;
 
using Meta.Interface;

namespace Meta.Data
{
    [System.Serializable]
    public class UnlockedPerksData : DebugDataSO, ISave, ISerialize
    {
        public List<ActivePerkData> activePerks = new();
        public List<PassivePerkData> passivePerks = new();
    }
}