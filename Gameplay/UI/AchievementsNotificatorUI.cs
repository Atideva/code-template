using System.Collections.Generic;
using GameManager;
using Meta.Data;
using SO.ConfigsSO;
using UnityEngine;
using static Meta.Static.Achievs;

namespace Gameplay.UI
{
    public class AchievementsNotificatorUI : MonoBehaviour
    {
        [SerializeField] Transform container;
        IngameNotificationsSO _config;
        AchievementUI _activeNotification;
        readonly Queue<AchievementUIData> _queue = new();

        void Start()
        {
            _config = Game.Instance.Config.IngameNotifications;
            Game.Instance.Storage.Achievements.OnComplete += CreateNotification;
        }

        void OnDisable()
        {
            Game.Instance.Storage.Achievements.OnComplete -= CreateNotification;
        }

        public void CreateNotification(AchievementData data)
        {
            var ui = CreateUIData(data, data.current);

            if (!_activeNotification)
                ShowNotification(ui);
            else
                _queue.Enqueue(ui);
        }

        void HideUI(AchievementUI achievementUI)
        {
            if (_activeNotification == achievementUI)
                _activeNotification = null;

            achievementUI.OnHide -= HideUI;
            Destroy(achievementUI.gameObject);

            if (_queue.Count == 0) return;
            var ui = _queue.Dequeue();
            ShowNotification(ui);
        }

        void ShowNotification(AchievementUIData uiData )
        {
            var prefab = _config.GetAchievementPrefab(uiData.Stage.Tier);
            var ui = Instantiate(prefab, container);

#if UNITY_EDITOR
            ui.gameObject.name = "[AchievementNotification] " + uiData.Name;
#endif

            ui.Refresh(uiData);
            ui.ShakeNumbers();
            ui.MoveInAnimation();
            ui.OnHide += HideUI;
            
            if (uiData.Stage.CompletedSteps < ui.SlotsCount)
                ui.HighlightAchievedSlot(uiData.Stage.CompletedSteps);
            else
                ui.PlaySlotsScaleAnimation();
      
            _activeNotification = ui;
        }
    }
}