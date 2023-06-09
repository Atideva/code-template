using Meta.Data;
using Meta.Facade;
using Meta.Interface;

namespace Meta.Save.SaveSystem
{
    public static class SerializerOld
    {
        public static void Pack<T>(T data) where T : ISave
        {
            if (data is not ISerialize serialize)
                return;

            switch (serialize)
            {
                case CharacterEquipmentData charEquip:
                    Pack(charEquip);
                    break;
                case InventoryEquipmentData inventoryEquip:
                    Pack(inventoryEquip);
                    break;
                default:
                    Log.MissSerialize();
                    break;
            }
        }

        public static void Unpack<T>(T data) where T : ISave
        {
            if (data is not ISerialize serialize)
                return;

            switch (serialize)
            {
                case CharacterEquipmentData charEquip:
                    Unpack(charEquip);
                    break;
                case InventoryEquipmentData inventoryEquip:
                    Unpack(inventoryEquip);
                    break;
                default:
                    Log.MissSerialize();
                    break;
            }
        }


        public static void Pack(CharacterEquipmentData data)
        {
            // if (NotNull(data.weapon))
            //     data.weapon.databaseID = Database.GetID(nameof(WeaponSO), data.weapon.so);
            // if (NotNull(data.necklace))
            //     data.necklace.databaseID = Database.GetID(nameof(NecklaceSO), data.necklace.so);
            // if (NotNull(data.gloves))
            //     data.gloves.databaseID = Database.GetID(nameof(GlovesSO), data.gloves.so);
            // if (NotNull(data.helm))
            //     data.helm.databaseID = Database.GetID(nameof(HelmSO), data.helm.so);
            // if (NotNull(data.vest))
            //     data.vest.databaseID = Database.GetID(nameof(VestSO), data.vest.so);
            // if (NotNull(data.boots))
            //     data.boots.databaseID = Database.GetID(nameof(BootsSO), data.boots.so);
        }

        public static void Unpack(CharacterEquipmentData data)
        {
            // if (data.weapon.databaseID > 0)
            //     data.weapon.so = Database.Get<WeaponSO>(nameof(WeaponSO), data.weapon.databaseID);
            // if (data.necklace.databaseID > 0)
            //     data.necklace.so = Database.Get<NecklaceSO>(nameof(NecklaceSO), data.necklace.databaseID);
            // if (data.gloves.databaseID > 0)
            //     data.gloves.so = Database.Get<GlovesSO>(nameof(GlovesSO), data.gloves.databaseID);
            // if (data.helm.databaseID > 0)
            //     data.helm.so = Database.Get<HelmSO>(nameof(HelmSO), data.helm.databaseID);
            // if (data.vest.databaseID > 0)
            //     data.vest.so = Database.Get<VestSO>(nameof(VestSO), data.vest.databaseID);
            // if (data.boots.databaseID > 0)
            //     data.boots.so = Database.Get<BootsSO>(nameof(BootsSO), data.boots.databaseID);
        }

        public static void Pack(InventoryEquipmentData data)
        {
            
            
            // foreach (var s in data.weapons.Where(s => s.so))
            //     s.databaseID = Database.GetID(nameof(WeaponSO), s.so);
            // foreach (var s in data.necklaces.Where(s => s.so))
            //     s.databaseID = Database.GetID(nameof(NecklaceSO), s.so);
            // foreach (var s in data.gloves.Where(s => s.so))
            //     s.databaseID = Database.GetID(nameof(GlovesSO), s.so);
            // foreach (var s in data.helms.Where(s => s.so))
            //     s.databaseID = Database.GetID(nameof(HelmSO), s.so);
            // foreach (var s in data.vests.Where(s => s.so))
            //     s.databaseID = Database.GetID(nameof(VestSO), s.so);
            // foreach (var s in data.boots.Where(s => s.so))
            //     s.databaseID = Database.GetID(nameof(BootsSO), s.so);
        }

        public static void Unpack(InventoryEquipmentData data)
        {
            // foreach (var s in data.weapons)
            //     s.so = Database.Get<WeaponSO>(nameof(WeaponSO), s.databaseID);
            // foreach (var s in data.necklaces)
            //     s.so = Database.Get<NecklaceSO>(nameof(NecklaceSO), s.databaseID);
            // foreach (var s in data.gloves)
            //     s.so = Database.Get<GlovesSO>(nameof(GlovesSO), s.databaseID);
            // foreach (var s in data.helms)
            //     s.so = Database.Get<HelmSO>(nameof(HelmSO), s.databaseID);
            // foreach (var s in data.vests)
            //     s.so = Database.Get<VestSO>(nameof(VestSO), s.databaseID);
            // foreach (var s in data.boots)
            //     s.so = Database.Get<BootsSO>(nameof(BootsSO), s.databaseID);
        }
    }
}