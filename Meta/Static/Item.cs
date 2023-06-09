using GameManager;
using Meta.Data;
using Meta.Facade;

namespace Meta.Static
{
    public static class Item
    {
        public static ItemUIData NewUIData(EquipmentData equip)
        {
            if (equip == null)
            {
                Log.Error("Equip is null");
                return new ItemUIData();
            }

            if (!equip.so)
            {
                Log.Error("Equip is  empty");
                return new ItemUIData();
            }

            var icon = equip.so.Icon;
            var tier = equip.so.Tier;
            var tierData = Game.Instance.Config.ItemTiers.Get(tier);
            var categoryIcon = Game.Instance.Config.Icons.EquipCategory(equip.Type);

            return new ItemUIData
            {
                Equip = equip,
                Lvl = equip.lvl,
                Icon = icon,
                ItemBackground = tierData.itemBackground,
                CategoryIcon = categoryIcon,
            };
        }
    }
}