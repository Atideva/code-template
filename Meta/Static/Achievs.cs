using Meta.Data;

namespace Meta.Static
{
    public static class Achievs
    {
        public static AchievementUIData CreateUIData(AchievementData data, AchievementStageData stage)
            => new()
            {
                Name = data.so.name,
                RewardIcon = data.GetRewardIcon(),
                TotalValue = (int) data.totalValue,

                DescriptionLocalize = data.so.Description,
                SlotInfoLocalize = data.so.SlotInfoLocalize,
                
                FullSlotSprite = data.so.FullSlotSprite,
                EmptySlotSprite = data.so.EmptySlotSprite,
                
                Stage = CreateStageUIData(stage)
            };

        public static AchievementStageUIData CreateStageUIData(AchievementStageData data)
            => new()
            {
                Tier = data.Tier,
                TotalRequire =  data.TotalRequire,
                StepRequire = data.Require,
                SumRequire = data.SumRequire,
                CompletedSteps = data.completeSteps,
             
            };
    }
}