using Meta.Data;
using Meta.Facade;
using Meta.Interface;
using static Meta.Save.SaveSystem.SerializerPack;
using static Meta.Save.SaveSystem.SerializerUnpack;


namespace Meta.Save.SaveSystem
{
    public static class Serializer
    {
        public static void Pack<T>(T data) where T : ISave
        {
            if (data is not ISerialize serialize)
                return;

            switch (serialize)
            {
                case CharacterEquipmentData ce:
                    Packing(ce);
                    break;
                case InventoryEquipmentData ie:
                    Packing(ie);
                    break;
                case UnlockedPerksData up:
                    Packing(up);
                    break;
                case CampaignSceneData scn:
                    Packing(scn);
                    break;
                case AchievementListData ac:
                    Packing(ac);
                    break;
                case HeroesData h:
                    Packing(h);
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
                case CharacterEquipmentData ce:
                    Unpacking(ce);
                    break;
                case InventoryEquipmentData ie:
                    Unpacking(ie);
                    break;
                case UnlockedPerksData up:
                    Unpacking(up);
                    break;
                case CampaignSceneData sc:
                    Unpacking(sc);
                    break;
                case AchievementListData ac:
                    Unpacking(ac);
                    break;
                case HeroesData h:
                    Unpacking(h);
                    break;
                default:
                    Log.MissSerialize();
                    break;
            }
        }
    }
}