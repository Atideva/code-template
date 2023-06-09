using System;
using Meta.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.Views
{
    public class LoseViewUI : ViewUI
    {
        [SerializeField] TextMeshProUGUI surviveTime;
        [SerializeField] TextMeshProUGUI bestTime;
        [SerializeField] TextMeshProUGUI kills;
        [SerializeField] TextMeshProUGUI levelName;
        [SerializeField] Button exitToMainMenu;
        public event Action OnExitToMainMenu = delegate { };

        protected override void OnShowUI()
        {
            exitToMainMenu.onClick.AddListener(Exit);
        }

        void OnDisable()
        {
            exitToMainMenu.onClick.RemoveListener(Exit);
        }

        void Exit() => OnExitToMainMenu();

        public void SetLevelName(string lvlName)
        {
            levelName.text = lvlName;
        }

        public void SetBestTime(string best)
        {
            bestTime.text = best;
        }

        public void SetSurviveTime(string survive)
        {
            surviveTime.text = survive;
        }

        public void SetKills(int amount)
        {
            kills.text = amount.ToString();
        }
    }
}