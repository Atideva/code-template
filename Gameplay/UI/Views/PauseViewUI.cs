using System.Linq;
using GameManager;
using Gameplay.UI.Perks;
using Meta.Enums;
using Meta.UI;
using Meta.UI.Popups;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.Views
{
    public class PauseViewUI : ViewUI
    {
        [Header("Perks")]
        [SerializeField] PerkPanelUI activePerks;
        [SerializeField] PerkPanelUI passivePerks;

        [Header("Components")]
        [SerializeField] ButtonSwitcherUI volumeSwitcher;
        [SerializeField] Button quitButton;
        [SerializeField] PopupUI quitUI;

        protected override void OnShowUI()
        {
            var player = Scene.Instance.Player;
            activePerks.Set(player.ActivePerks);
            passivePerks.Set(player.PassivePerks);
        }

        void Start()
        {
            RefreshSwitcher();
        }

        void OnEnable()
        {
            volumeSwitcher.OnSwitch += SwitchVolume;
            quitButton.onClick.AddListener(QuitPopup);
        }

        void OnDestroy()
        {
            volumeSwitcher.OnSwitch -= SwitchVolume;
            quitButton.onClick.RemoveListener(QuitPopup);
        }

        void QuitPopup() => quitUI.Show();

        void SwitchVolume(bool switcher)
        {
            Debug.Log("Options toggle: " + OptionToggleEnum.Sound + " (" + switcher + ")");
            Debug.Log("Options toggle: " + OptionToggleEnum.Music + " (" + switcher + ")");
            EventsUI.Instance.OptionToggle(OptionToggleEnum.Sound, switcher);
            EventsUI.Instance.OptionToggle(OptionToggleEnum.Music, switcher);
        }

        void RefreshSwitcher()
        {
            var optionData = Game.Instance.Storage.Options.Toggles;
            var sfx = optionData.FirstOrDefault(d => d.type == OptionToggleEnum.Sound);
            var music = optionData.FirstOrDefault(d => d.type == OptionToggleEnum.Music);

            if (sfx is {toggle: true} || music is {toggle: true})
            {
                volumeSwitcher.Enable();
            }
            else
                volumeSwitcher.Disable();
        }
    }
}