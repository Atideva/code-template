using I2.Loc;
using UnityEngine;

namespace Meta.Data
{
    public class AchievementUIData
    {
        public string Name;
        
        public AchievementStageUIData Stage;
        public LocalizedString DescriptionLocalize;
        public LocalizedString SlotInfoLocalize;
        public int TotalValue;

        public Sprite RewardIcon;
        public Sprite FullSlotSprite;
        public Sprite EmptySlotSprite;
   
    }
}