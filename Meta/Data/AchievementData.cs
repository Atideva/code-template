using System.Collections.Generic;
using System.Linq;
using SO.AchievementsSO;
using UnityEngine;

namespace Meta.Data
{
    [System.Serializable]
    public class AchievementData : GuidData
    {
        public AchievementSO so;
        public float totalValue;
        public AchievementStageData current;
        public List<AchievementStageData> stages = new();

        public void AddValue(float value)
        {
            current.value += value;
            totalValue += value;
        }

        public void NextStep()
        {
            current.value -= current.StepValue;
            current.completeSteps++;
        }

        public void NextStage()
        {
            current.markAsComplete = true;
            if (NoMoreStages) return;

            var balance = current.value;
            current = GetNewStage();
            current.value = balance;
            stages.Add(current);
        }
 
        bool NoMoreStages  
            => so.Stages.All(stage
                => stages.Any(data => data.so == stage));

        AchievementStageData GetNewStage()
            => new() {so = FreeStageSO()};

        AchievementStageSO FreeStageSO()
            => so.Stages.FirstOrDefault(stage => stages.All(data => data.so != stage));

        public Sprite GetRewardIcon()
            => current.so && current.so.FinalReward
                ? current.so.FinalReward.Icon
                : null;

        int CompleteStagesCount
            => stages.Count(data => data.markAsComplete);

        public int StagesCount 
            => so ? so.Stages.Count : 0;

        public bool IsComplete
            => CompleteStagesCount >= StagesCount;

        public bool IsStepComplete
            => current.IsStepComplete;

        public bool IsStageComplete
            => current.IsComplete;

        public float StageSum(AchievementStageData stage)
        {
            if (stages.Contains(stage))
            {
                return stage.SumRequire + stage.value;

            }

            return float.MaxValue;
        }
    }
}