using Meta.Data;
using Meta.Static;
using UnityEngine;

// ReSharper disable InconsistentNaming

namespace Meta.Facade
{
    public static class Log
    {
//_____________________________________________________________________
        static LogData settings;

        public static LogData Settings
        {
            get => settings;
            set
            {
                beforeInitLog = false;
                settings = value;
            }
        }

        const string ColorEnd = Colors.END;

        const string ColorUI = Colors.LIGHT_BLUE;
        const string ColorCharacter = Colors.YELLOW;
        const string ColorInventory = Colors.LIGHT_GREEN;

        const string ColorWeapon = Colors.YELLOW;

        const string ColorWarning = Colors.WHITE;
        const string ColorError = Colors.RED;
        static bool beforeInitLog = true;

//_____________________________________________________________________
        static string ui(string message, string objectName)
            => ColorUI + "[UI] " + message + ": " + objectName + ColorEnd;

        static string inventory(string message, string objectName)
            => ColorInventory + "[Inventory] " + message + ": " + objectName + ColorEnd;

        static string character(string message, string objectName)
            => ColorCharacter + "[Character] " + message + ": " + objectName + ColorEnd;

        static string warning(string message)
            => ColorWarning + "[Warning]: " + ColorEnd + message;

        static string error(string message)
            => ColorError + "[Error]: " + ColorEnd + message;

        static string return_to_pool(string objectName)
            => "Return to pool: " + objectName;

        static string weapon(string message, string objectName)
            => ColorWeapon + "[Weapon]: " + message + ColorEnd + objectName;

//_____________________________________________________________________

        //MISC
        public static void PoolObjectReturned(string txt, GameObject obj = null)
        {
            if (beforeInitLog || settings.returnToPoolLog)
                Debug.Log(return_to_pool(txt), obj);
        }

        #region [Combat] Weapons

        public static void Weapon(string message, string objectName, GameObject obj = null)
        {
            Debug.Log(weapon(message, objectName), obj);
        }

        public static void WeaponShoot(string objectName, GameObject obj = null)
            => Weapon("shoot ", objectName, obj);

        #endregion


        #region CHARACTER

        //CHARACTER 
        public static void CharacterMessage(string message, string objectName, GameObject obj = null)
        {
            if (beforeInitLog || settings.characterLog)
                Debug.Log(character(message, objectName), obj);
        }

        public static void CharacterUnequip(string objectName, GameObject obj = null)
            => CharacterMessage("un-equip", objectName, obj);

        public static void CharacterEquip(string objectName, GameObject obj = null)
            => CharacterMessage("equip", objectName, obj);

        #endregion

        #region INVENTORY

        //INVENTORY
        static void InventoryMessage(string message, string objectName, GameObject obj = null)
        {
            if (beforeInitLog || settings.inventoryLog)
                Debug.Log(inventory(message, objectName), obj);
        }

        public static void InventoryAddItem(string objectName, GameObject obj = null)
            => InventoryMessage("add", objectName, obj);

        public static void InventoryRemoveItem(string objectName, GameObject obj = null)
            => InventoryMessage("remove", objectName, obj);

        #endregion

        #region UI

        static void UIMessage(string message, string objectName, GameObject obj = null)
        {
            if (beforeInitLog || settings.uiLog)
                Debug.Log(ui(message, objectName), obj);
        }

        public static void UIClick(string objectName, GameObject obj = null)
            => UIMessage("CLICK", objectName, obj);

        public static void UIHide(string objectName, GameObject obj = null)
            => UIMessage("HIDE", objectName, obj);

        public static void UIShow(string objectName, GameObject obj = null)
            => UIMessage("SHOW", objectName, obj);

        #endregion

        #region Errors

        public static void Warning(string message, GameObject obj = null)
            => Debug.Log(warning(message), obj);

        public static void Error(string message, GameObject obj = null)
            => Debug.Log(error(message), obj);

        public static void InitError(string message)
            => Error(message + " not initialized");

        public static void PerkMiss()
            => Error("PerkType miss");
        
        public static void EquipMiss()
            => Error("EquipmentData miss");

        public static void BankMiss()
            => Error("BankCurrencyEnum miss");

        public static void NullEquip()
            => Error("EquipmentData is null");

        public static void MissSerialize()
            => Warning("Serialize miss this data");

        public static void MissReference(string message)
            => Error(message + " null reference");

        #endregion
    }
}