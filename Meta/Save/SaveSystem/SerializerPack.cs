using Meta.Data;
using static Meta.Save.SaveSystem.SerializerAchievements;
using static Meta.Save.SaveSystem.SerializerEquipment;
using static Meta.Save.SaveSystem.SerializerPerks;
using static Meta.Save.SaveSystem.SerializerScenes;
using static Meta.Save.SaveSystem.SerializerHeroes;


namespace Meta.Save.SaveSystem
{
    public static class SerializerPack
    {
        public static void Packing(CharacterEquipmentData data)
        {
            foreach (var equip in data.AllEquip)
                Pack(equip);
        }

        public static void Packing(InventoryEquipmentData data)
        {
            foreach (var equip in data.AllEquip)
                Pack(equip);
        }

        public static void Packing(UnlockedPerksData data)
        {
            foreach (var perk in data.activePerks)
                Pack(perk);
            foreach (var perk in data.passivePerks)
                Pack(perk);
        }

        public static void Packing(CampaignSceneData data)
        {
            foreach (var scene in data.scenes)
                Pack(scene);
        }

        public static void Packing(AchievementListData data)
        {
            foreach (var achievement in data.achievements)
                Pack(achievement);
        }

        public static void Packing(HeroesData data)
        {
            Pack(data.selected);
            foreach (var hero in data.heroes) 
                Pack(hero);
        }
    }
}