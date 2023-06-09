using Meta.Data;
using SO.EquipmentSO;
using static Meta.Facade.Database;
using static Meta.Static.Equip;

namespace Meta.Save.SaveSystem
{
    public static class SerializerEquipment
    {
        public static void Pack(EquipmentData data)
        {
            if (IsNull(data)) return;
            var guid = GetGuid(data.so);
            data.guid = guid;
        }

        public static void Unpack(EquipmentData data)
        {
            if (IsNull(data)) return;
            var so = GetSO(data.guid);
            data.so = (EquipSO) so;
        }
    }
}