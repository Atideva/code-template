using System.Collections.Generic;
using Meta.Facade;
using SO.DatabasesSO;

namespace Meta.Save.Databases
{
    public static class DatabaseHandler
    {
        static List<DatabaseSO> _databases = new();
        static EquipDatabaseSO _equip;
        public static IReadOnlyList<DatabaseSO> Databases => _databases;
        public static EquipDatabaseSO Equip => _equip;

        public static void LoadAll()
        {
            _databases = DatabaseSearcher.FindDatabases();
            _equip = Get<EquipDatabaseSO>();

            foreach (var data in _databases)
            {
                data.LoadItems();
                data.MapItems();
            }
        }

        static T Get<T>() where T : DatabaseSO
        {
            foreach (var database in _databases)
                if (database is T so)
                    return so;

            Log.Error("Cannot find database" + nameof(T));
            return null;
        }
    }
}