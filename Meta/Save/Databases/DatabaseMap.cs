using System.Collections.Generic;
using Meta.Facade;
using Meta.Interface;
using Meta.Static;

namespace Meta.Save.Databases
{
    public static class DatabaseMap
    {
        static readonly Dictionary<string, Dictionary<int, object>> ObjectMap = new();
        static readonly Dictionary<string, Dictionary<object, int>> IdMap = new();

        public static void Clear()
        {
            ObjectMap.Clear();
            IdMap.Clear();
        }

        public static T Get<T>(string key, int id) where T : IDatabaseItem, new()
        {
            if (ObjectMap.ContainsKey(key))
            {
                var road = ObjectMap[key];
                if (road.ContainsKey(id))
                    return (T) road[id];
            }

            Log.Warning(Colors.WHITE + key + Colors.END + " is not in Database");
            return default;
        }

        public static int GetID(string key, IDatabaseItem item)
        {
            if (IdMap.ContainsKey(key))
            {
                var road = IdMap[key];
                if (road.ContainsKey(item))
                    return road[item];
            }

            Log.Warning(Colors.WHITE + key + Colors.END + " is not in Database");
            return default;
        }


        public static void Add<T>(string key, IEnumerable<T> items) where T : IDatabaseItem
        {
            if (!ObjectMap.ContainsKey(key))
            {
                var newRoad = new Dictionary<int, object>();
                ObjectMap.Add(key, newRoad);
            }

            if (!IdMap.ContainsKey(key))
            {
                var newRoad = new Dictionary<object, int>();
                IdMap.Add(key, newRoad);
            }

            foreach (var t in items)
            {
                if (!ObjectMap[key].ContainsKey(t.DatabaseID))
                    ObjectMap[key].Add(t.DatabaseID, t);
                if (!IdMap[key].ContainsKey(t))
                    IdMap[key].Add(t, t.DatabaseID);
            }
        }
    }
}