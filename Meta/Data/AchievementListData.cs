using System.Collections.Generic;
using Meta.Interface;

namespace Meta.Data
{
    [System.Serializable]
    public class AchievementListData: ISave, ISerialize
    {
        public List<AchievementData> achievements = new();
    }
}