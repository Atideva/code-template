using System;
using System.Collections.Generic;
using Gameplay.UI;
using I2.Loc;
using Meta.Data;
using Meta.Enums;
using Meta.Static;
using Meta.UI;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Utilities.Extensions.UIExtensions;


namespace Meta.AchievementRewards
{
    public class AchievementRewardUI : MonoBehaviour
    {
        [field: SerializeField] [ReadOnly] public AchievementData Data { get; private set; }
        [field: SerializeField] [ReadOnly] public AchievementStageData Stage { get; private set; }
        [SerializeField] [ReadOnly] AchievementUI achievement;

        [Space(20)]
        [SerializeField] TakeRewardUI rewardPrefab;
        [SerializeField] [ReadOnly] List<TakeRewardUI> gems = new();

        [Space(20)]
        [SerializeField] RectTransform container;

        [Header("Remain Info")]
        [SerializeField] CanvasGroup infoGroup;
        [SerializeField] TextMeshProUGUI intoTxt;
        [SerializeField] LocalizationParamsManager infoParams;
        [SerializeField] Image infoFill;
        [SerializeField] LocalizedString remainLocalize;

        [Header("Collect Button")]
        [SerializeField] CanvasGroup collectGroup;
        [SerializeField] Button collectButton;

        public event Action<AchievementStageData, BankCurrencyEnum, int> OnCollect = delegate { };
        public event Action<AchievementStageData> OnCollectFinalReward = delegate { };
        public event Action<AchievementRewardUI> OnStageCollected = delegate { };

        public Vector2 achievementSize = new(600, 180);
        Dictionary<TakeRewardUI, SlotUI> slotReward = new();

        void EnableCollectButton()
        {
            collectButton.onClick.RemoveListener(Collect);
            collectButton.onClick.AddListener(Collect);
            EnableGroup(collectGroup);
        }

        void ShowInfo()
        {
            RefreshFillInfo();
            EnableGroup(infoGroup);
        }

        void HideInfo()
        {
            DisableGroup(infoGroup);
        }

        void Collect()
        {
            if (gems.Count > 0)
            {
                var reward = gems[0];
                CollectReward(reward);
                gems.Remove(reward);
                slotReward[reward].ShowIcon();
                slotReward.Remove(reward);
            }
            else
            {
                if (Stage.IsComplete && !Stage.FinaRewardCollected)
                {
                    CollectFinalReward();
                    OnStageCollected(this);
                    //  NextStage();
                }
            }

            if (gems.Count == 0)
            {
                if (Stage.IsComplete && !Stage.FinaRewardCollected)
                {
                }
                else
                {
                    DisableCollectButton();
                    ShowInfo();
                }
            }
        }

        void CollectReward(TakeRewardUI reward)
        {
            OnCollect(Stage, BankCurrencyEnum.GEM, reward.RewardValue);

            reward.OnCollectComplete += DestroyRewardUI;
            //reward.DisableHighlight();
            reward.PlayCollectAnimation();
        }

        void CollectFinalReward()
        {
            OnCollectFinalReward(Stage);
        }

        void DestroyRewardUI(TakeRewardUI reward)
        {
            reward.OnCollectComplete -= DestroyRewardUI;
            Destroy(reward.gameObject);
        }

        void DisableCollectButton()
        {
            collectButton.onClick.RemoveListener(Collect);
            DisableGroup(collectGroup);
        }

        void OnDisable()
        {
            collectButton.onClick.RemoveListener(Collect);
        }

        public void Set(AchievementData achievData)
        {
            Data = achievData;
        }

        public void ChangeStage(AchievementStageData stage, AchievementUI prefab)
        {
            Stage = stage;

            if (achievement)
                Destroy(achievement.gameObject);

            achievement = Instantiate(prefab, container);
            achievement.transform.localScale = Vector3.one;
            var rect = (RectTransform) achievement.transform;
            rect.sizeDelta = new Vector2(achievementSize.x, achievementSize.y);

#if UNITY_EDITOR
            achievement.gameObject.name = "[AchievementInfo] " + stage.so.name;
#endif
        }


        public void Refresh()
        {
            var ui = Achievs.CreateUIData(Data, Stage);
            achievement.Refresh(ui);

            for (int i = Stage.rewardsCollected; i < Stage.completeSteps; i++)
            {
                var gem = Instantiate(rewardPrefab, achievement.Slots[i].transform);
                gem.transform.localScale = Vector3.one;
                gem.EnableHighlight();
                gem.Set(Stage.so.StepGemsReward);

                gems.Add(gem);
                slotReward.Add(gem, achievement.Slots[i]);

                achievement.Slots[i].HideIcon();
            }

            if (Stage.completeSteps < 5)
                RefreshFillInfo();

            if (gems.Count > 0)
            {
                EnableCollectButton();
                HideInfo();
            }
            else
            {
                DisableCollectButton();
                ShowInfo();
            }
        }

      //  [SerializeField] Color remainColor;

        void RefreshFillInfo()
        {
            var fill = Stage.value / Stage.Require;
            infoFill.fillAmount = fill;

            var remain = (int) (Stage.Require - Stage.value);
            infoParams.SetParameterValue("VALUE", remain.ToString());
    

            for (int i = 0; i < Stage.completeSteps; i++)
            {
                achievement.Slots[i].DisableFill();;
            }
            
            if (Stage.completeSteps < 5)
            {
                achievement.Slots[Stage.completeSteps].ShowIcon();
                achievement.Slots[Stage.completeSteps].SetFill(fill);
                achievement.Slots[Stage.completeSteps].ShowInfo();
              //  achievement.Slots[Stage.completeSteps].RefreshInfo(remainLocalize.mTerm,(int) Data.StageSum(Stage));
              var i = (int) Data.StageSum(Stage);
              var s = i.ToString();
                achievement.Slots[Stage.completeSteps].RefreshInfo(s,Color.yellow);
               // achievement.Slots[Stage.completeSteps].RefreshInfo(remainLocalize.mTerm, remain);
                //   achievement.Slots[Stage.completeSteps].RefreshInfo("+"+remain,remainColor);
            }
        }
    }
}