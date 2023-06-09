using Meta.Data;
using SO.PerksSO;
using static Meta.Facade.Database;

namespace Meta.Save.SaveSystem
{
    public static class SerializerPerks
    {
        static bool IsNull(ActivePerkData data) => data == null || !data.so;
        static bool IsNull(PassivePerkData data) => data == null || !data.so;

        public static void Pack(ActivePerkData data)
        {
            if (IsNull(data)) return;
            var guid =  GetGuid(data.so);
            data.guid = guid;
        }

        public static void Pack(PassivePerkData data)
        {
            if (IsNull(data)) return;
            var guid =  GetGuid(data.so);
            data.guid = guid;
        }

        public static void Unpack(ActivePerkData data)
        {
            if (IsNull(data)) return;
            var so =  GetSO(data.guid);
            data.so = (ActivePerkSO) so;
        }

        public static void Unpack(PassivePerkData data)
        {
            if (IsNull(data)) return;
            var so =  GetSO(data.guid);
            data.so = (PassivePerkSO) so;
        }
    }
}