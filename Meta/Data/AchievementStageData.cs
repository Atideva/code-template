using System.Linq;
using Meta.Enums;
using SO.AchievementsSO;

namespace Meta.Data
{
    [System.Serializable]
    public class AchievementStageData : GuidData
    {
        public AchievementStageSO so;
        public bool markAsComplete;
        public float value;

        public int completeSteps;
        public int rewardsCollected;
        public bool finalRewardCollected;

        
        public float TotalRequire
            => so
                ? so.Steps.Sum()
                : float.MaxValue;
        public float Require
            => so
                ? so.Steps.Count > completeSteps
                    ? so.Steps[completeSteps]
                    : so.Steps[^1]
                : float.MaxValue;

        public float SumRequire
        {
            get
            {
                var sum = 0f;
                for (int i = 0; i <  completeSteps ; i++)
                {
                    sum += so.Steps[i];
                }
                return sum;
            }
        }
        
        public int StepsCount => so ? so.Steps.Count : int.MaxValue;


        public bool IsComplete => completeSteps >= StepsCount;
        public bool RewardsCollected => rewardsCollected >= completeSteps;
        public bool FinaRewardCollected => !so.FinalReward || finalRewardCollected;
        public bool NotCollected => !AllCollect;

        public bool AllCollect
            => IsComplete
               && RewardsCollected
               && FinaRewardCollected;


        public float StepValue =>
            so && so.Steps.Count >= completeSteps
                ? so.Steps[completeSteps - 1]
                : float.MaxValue;

        public bool IsStepComplete
            => value >= StepValue;


        public TierEnum Tier => so
            ? so.Tier
            : TierEnum.Tier1;
    }
}