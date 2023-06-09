using Meta.Data;
using static Meta.Save.SaveSystem.SerializerAchievements;
using static Meta.Save.SaveSystem.SerializerEquipment;
using static Meta.Save.SaveSystem.SerializerPerks;
using static Meta.Save.SaveSystem.SerializerScenes;
using static Meta.Save.SaveSystem.SerializerHeroes;


namespace Meta.Save.SaveSystem
{
    public static class SerializerUnpack
    {
        public static void Unpacking(CharacterEquipmentData data)
        {
            foreach (var equip in data.AllEquip)
                Unpack(equip);
        }

        public static void Unpacking(InventoryEquipmentData data)
        {
            foreach (var equip in data.AllEquip)
                Unpack(equip);
        }

        public static void Unpacking(UnlockedPerksData data)
        {
            foreach (var perk in data.activePerks)
                Unpack(perk);
            foreach (var perk in data.passivePerks)
                Unpack(perk);
        }

        public static void Unpacking(CampaignSceneData data)
        {
            foreach (var scene in data.scenes)
                Unpack(scene);
        }

        public static void Unpacking(AchievementListData data)
        {
            foreach (var achievement in data.achievements)
                Unpack(achievement);
        }

        public static void Unpacking(HeroesData data)
        {
            Unpack(data.selected);
            foreach (var hero in data.heroes)
                Unpack(hero);
        }
    }
}