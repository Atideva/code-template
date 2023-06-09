using System.Collections.Generic;
using Meta.Interface;
using Meta.Save.Databases;
using SO.DatabasesSO;
using UnityEngine;
using Utilities.Odin;

namespace Meta.Facade
{
    public static class Database
    {
        public static ScriptableObjectReferenceCache Data { get; private set; }
        public static void Init(ScriptableObjectReferenceCache database)
        {
            Data = database;
            Data.Initialize();
            // DatabaseMap.Clear();
            // DatabaseHandler.LoadAll();
        }
        public static string GetGuid(ScriptableObject so) 
            => Data.GetGuid(so);

        public static ScriptableObject GetSO(string guid)
            => Data.GetSO(guid);
        public static int GetID(string key, IDatabaseItem item)
            => DatabaseMap.GetID(key, item);

        public static T Get<T>(string key, int id) where T : IDatabaseItem, new()
            => DatabaseMap.Get<T>(key, id);


        public static IReadOnlyList<DatabaseSO> List
            => DatabaseHandler.Databases;
        public static EquipDatabaseSO Equips
            => DatabaseHandler.Equip;
     
    }
}