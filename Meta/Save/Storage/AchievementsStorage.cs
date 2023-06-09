using System;
using Meta.Data;
using Meta.Save.SaveSystem;
using SO.AchievementsSO;
using SO.ConfigsSO;
using UnityEngine;


namespace Meta.Save.Storage
{
    public class AchievementsStorage : Saveable<AchievementListData>
    {
        public event Action<AchievementData> OnComplete = delegate { };

        public void Init(FirstLaunchSO config)
        {
            if (PlayerPrefs.HasKey(SaveKey))
            {
                Load();
            }
            else
            {
                SaveableData = new AchievementListData();
                Save();
            }
        }

        public AchievementData GetData(AchievementSO so)
        {
            if (SaveableData.achievements.Exists(data => data.so == so))
                return SaveableData.achievements.Find(data => data.so == so);

            var newData = new AchievementData {so = so};
            var newStage = new AchievementStageData {so = so.Stages[0]};
            newData.current = newStage;
            newData.stages.Add(newStage);

            SaveableData.achievements.Add(newData);
            return newData;
        }

        public void AddValue(AchievementSO so, float value)
        {
            if (!so) return;
            if (so.Stages.Count == 0) return;

            var data = GetData(so);
            if (data.IsComplete) return;

            data.AddValue(value);

            if (data.IsStepComplete)
            {
                data.NextStep();

                OnComplete(data);

                if (data.IsStageComplete)
                    data.NextStage();
            }

            Save();
        }
        public void RewardCollected(AchievementStageData stage)
        {
            stage.rewardsCollected++;
            Save();
        }
        public void FinalRewardCollected(AchievementStageData stage)
        {
            stage.finalRewardCollected=true;
            Save();
        }
        
    }
}