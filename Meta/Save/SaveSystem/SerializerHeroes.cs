using Meta.Data;
using Meta.Facade;
using SO.UnitsSO;

namespace Meta.Save.SaveSystem
{
    public static class SerializerHeroes
    {
        static bool IsNull(HeroCardData data) => data == null || !data.so;

        public static void Pack(HeroCardData data)
        {
            if (IsNull(data)) return;
            var guid = Database.GetGuid(data.so);
            data.guid = guid;
        }

        public static void Unpack(HeroCardData data)
        {
            if (IsNull(data)) return;
            var so = Database.GetSO(data.guid);
            data.so = (HeroSO) so;
        }
    }
}