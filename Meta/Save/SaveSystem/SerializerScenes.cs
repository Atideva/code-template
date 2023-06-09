using Meta.Data;
using SO.ScenesSO;
using static Meta.Facade.Database;

namespace Meta.Save.SaveSystem
{
    public static class SerializerScenes
    {
        static bool IsNull(SceneData data) => data == null || !data.so;

        public static void Pack(SceneData data)
        {
            if (IsNull(data)) return;
            var guid = GetGuid(data.so);
            data.guid = guid;
        }

        public static void Unpack(SceneData data)
        {
            if (IsNull(data)) return;
            var so = GetSO(data.guid);
            data.so = (SceneSO) so;
        }
    }
}