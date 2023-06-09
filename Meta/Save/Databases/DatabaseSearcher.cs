using System.Collections.Generic;
using System.Linq;
using Meta.Facade;
using Meta.Interface;
using Meta.Static;
using SO.DatabasesSO;
using SO.EquipmentSO;
using SO.ShopSO;
using UnityEngine;

namespace Meta.Save.Databases
{
    public static class DatabaseSearcher
    {
        public static List<DatabaseSO> FindDatabases()
        {
            var inFolder = Resources.LoadAll<DatabaseSO>(Folder.DATABASES);
            return inFolder.ToList();
        }

        //    static T Find<T>() where T : ScriptableObject
        //    {
        // var b =      Get<ShopBundleSO>()
        //        var inFolder = Resources.LoadAll<T>(Folder.DATABASES);
        //        if (inFolder.Length > 0)
        //            return inFolder[0];
        //        Log.Error("Cannot find database" + nameof(T));
        //        return null;
        //    }
        public static List<T> Find<T>() where T : ScriptableObject, IDatabaseItem
        {
            //EQUIP
            if (typeof(T) == typeof(WeaponSO))
                return Load<T>(Folder.EQUIP_WEAPONS).ToList();
            if (typeof(T) == typeof(NecklaceSO))
                return Load<T>(Folder.EQUIP_NECKLACES).ToList();
            if (typeof(T) == typeof(GlovesSO))
                return Load<T>(Folder.EQUIP_GLOVES).ToList();
            if (typeof(T) == typeof(HelmSO))
                return Load<T>(Folder.EQUIP_HELMS).ToList();
            if (typeof(T) == typeof(VestSO))
                return Load<T>(Folder.EQUIP_VESTS).ToList();
            if (typeof(T) == typeof(BootsSO))
                return Load<T>(Folder.EQUIP_BOOTS).ToList();
            
            //SHOP
            if (typeof(T) == typeof(ShopListSO))
                return Load<T>(Folder.SHOP_LISTS).ToList();
            if (typeof(T) == typeof(ShopBundleSO))
                return Load<T>(Folder.SHOP_BUNDLES).ToList();
            if (typeof(T) == typeof(ShopChestSO))
                return Load<T>(Folder.SHOP_CHESTS).ToList();
            if (typeof(T) == typeof(ShopSingleItemSO))
                return Load<T>(Folder.SHOP_ITEM).ToList();
 
            return null;
        }
 

        static T[] Load<T>(string folderPath) where T : ScriptableObject, IDatabaseItem
        {
            var inFolder = Resources.LoadAll<T>(folderPath);
            DatabaseID.Set(inFolder);
            return inFolder;
        }
 
        public static T LoadSpecificDatabase<T>() where T : ScriptableObject
        {
            var inFolder = Resources.LoadAll<T>(Folder.DATABASES);
            if (inFolder.Length > 0)
                return inFolder[0];
            Log.Error("Cannot find database" + nameof(T));
            return null;
        }
    }
}