using System;
using GameManager;
using Meta.Facade;
using Meta.UI.Anims;
using UnityEngine;

namespace Meta.UI.Controllers
{
    public class StatsPanelController : MonoBehaviour
    {
        [Header("User level")]
        [SerializeField] UserLevelUI userLevelUI;
        [Header("Bank stats")]
        [SerializeField] BankStatsAnimator animator;
        [SerializeField] BankResourceUI goldUI;
        [SerializeField] BankResourceUI gemUI;
        [SerializeField] BankResourceUI energyUI;
        public int AnimatedGoldAmount { get; set; }
        public int AnimatedGemAmount { get; set; }
        public int AnimatedEnergyAmount { get; set; }

        void Start()
        {
            animator.Init(this);

            Bank.OnChanged += Refresh;

            goldUI.OnClick += GoldClick;
            gemUI.OnClick += GemClick;
            energyUI.OnClick += EnergyClick;

            userLevelUI.RefreshLevel(1);
            userLevelUI.RefreshExperience(0, 100);

            Refresh();
        }

        void OnDisable()
        {
            Bank.OnChanged -= Refresh;
            goldUI.OnClick -= GoldClick;
            gemUI.OnClick -= GemClick;
            energyUI.OnClick -= EnergyClick;
        }

        void GoldClick() => EventsUI.Instance.OpenShop();
        void GemClick() => EventsUI.Instance.OpenShop();
        void EnergyClick() => EventsUI.Instance.OpenShop();

        public void Refresh()
        {
            goldUI.RefreshText(Bank.Gold - AnimatedGoldAmount);
            gemUI.RefreshText(Bank.Gem - AnimatedGemAmount);
            energyUI.RefreshText(Bank.Energy - AnimatedEnergyAmount);
        }
    }
}