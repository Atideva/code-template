using System.Collections.Generic;
using System.Linq;
using GameManager;
using Meta.AchievementRewards;
using Meta.Data;
using Meta.Enums;
using Meta.Facade;
using Meta.Save.Storage;
using Meta.Static;
using Meta.UI.Popups;
using Sirenix.OdinInspector;
using SO.ConfigsSO;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.UI.Controllers
{
    public class AchievementsController : MonoBehaviour
    {
        [InfoBox("Example will be destroyed at game start")]
        [SerializeField] AchievementRewardUI example;
        [SerializeField] AchievementPopupUI popup;
        [SerializeField] AchievementRewardUI prefab;
        [SerializeField] Button openButton;

        IngameNotificationsSO _notifications;
        AchievementsListSO _achievements;
        AchievementsStorage _storage;
        [SerializeField] [ReadOnly] List<AchievementRewardUI> rewardUIs = new();

        public IReadOnlyList<AchievementRewardUI> RewardUIs => rewardUIs;

        void Start()
        {
            openButton.onClick.AddListener(ShowPopup);
            Destroy(example.gameObject);

            _notifications = Game.Instance.Config.IngameNotifications;
            _achievements = Game.Instance.Config.Achievements;
            _storage = Game.Instance.Storage.Achievements;

            CreateUI();
            RefreshUI();
        }

        void ShowPopup()
        {
            popup.Show();
        }

        void OnDisable()
        {
            foreach (var ui in rewardUIs)
            {
                ui.OnCollect -= CollectReward;
                ui.OnStageCollected -= Refresh;
                ui.OnCollectFinalReward -= CollectFinalReward;
            }
        }

        void CollectReward(AchievementStageData stage, BankCurrencyEnum type, int value)
        {
            _storage.RewardCollected(stage);
            Bank.Add(type, value);
        }

        void CollectFinalReward(AchievementStageData stage)
        {
            _storage.FinalRewardCollected(stage);
            var reward = Equip.NewData(stage.so.FinalReward);
            Inventory.Add(reward);
            EventsUI.Instance.ShowNewItem(reward);
        }

        void CreateUI()
        {
            foreach (var so in _achievements.List)
            {
                var data = _storage.GetData(so);
                if (data.StagesCount > 0)
                    Create(data);
            }
        }

        void RefreshUI()
        {
            foreach (var ui in rewardUIs)
                Refresh(ui);
        }

        void Create(AchievementData data)
        {
            var ui = Instantiate(prefab, popup.Container);
            ui.transform.localScale = Vector3.one;
            ui.Set(data);
            ui.OnStageCollected += Refresh;
            ui.OnCollect += CollectReward;
            ui.OnCollectFinalReward += CollectFinalReward;

            rewardUIs.Add(ui);

#if UNITY_EDITOR
            ui.gameObject.name = "[AchievementReward] " + data.so.name;
#endif
        }

        void Refresh(AchievementRewardUI ui)
        {
            var stageToShow = ui.Data.stages.FirstOrDefault(stage => stage.NotCollected) ?? ui.Data.stages[^1];
            if (stageToShow == null) return;

            if (ui.Stage != stageToShow)
            {
                var achievement = _notifications.GetAchievementPrefab(stageToShow.Tier);
                ui.ChangeStage(stageToShow, achievement);
            }

            ui.Refresh();
        }
    }
}