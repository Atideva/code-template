using Meta.Data;
using SO.AchievementsSO;
using static Meta.Facade.Database;

namespace Meta.Save.SaveSystem
{
    public static class SerializerAchievements
    {
        static bool IsNull(AchievementData data) => data == null || !data.so;
        static bool IsNull(AchievementStageData data) => data == null || !data.so;

        public static void Pack(AchievementData data)
        {
            if (IsNull(data)) return;
            var guid = GetGuid(data.so);
            data.guid = guid;
            foreach (var stage in data.stages)
                Pack(stage);
        }

        public static void Unpack(AchievementData data)
        {
            if (IsNull(data)) return;
            var so = GetSO(data.guid);
            data.so = (AchievementSO) so;
            foreach (var stage in data.stages)
                Unpack(stage);
        }


        static void Pack(AchievementStageData data)
        {
            if (IsNull(data)) return;
            var guid = GetGuid(data.so);
            data.guid = guid;
        }

        static void Unpack(AchievementStageData data)
        {
            if (IsNull(data)) return;
            var so = GetSO(data.guid);
            data.so = (AchievementStageSO) so;
        }
    }
}