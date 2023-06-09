using Meta.Data;
using Meta.Enums;
using Meta.Facade;
using UnityEngine;

namespace Meta.Static
{
    public static class Error
    {
        public static EquipEnum EquipMiss
        {
            get
            {
                Log.Error("Equip enym miss");
                return EquipEnum.None;
            }
        }


        public static TierEnum TierMiss
        {
            get
            {
                Log.Error("Tier switch miss");
                return TierEnum.Tier1;
            }
        }

        public static string EquipEmptyString
        {
            get
            {
                Log.NullEquip();
                return string.Empty;
            }
        }


        public static bool EquipTrue
        {
            get
            {
                Log.EquipMiss();
                return true;
            }
        }

        public static bool EquipFalse
        {
            get
            {
                Log.EquipMiss();
                return false;
            }
        }
        public static EquipmentData NullEquip
        {
            get
            {
                Log.EquipMiss();
                return null;
            }
        }
        public static Sprite EquipNullSprite
        {
            get
            {
                Log.EquipMiss();
                return null;
            }
        }



   
    }
}