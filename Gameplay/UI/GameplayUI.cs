using GameManager;
using Gameplay.UI.Perks;
using Gameplay.UI.Views;
using Meta.UI;
using Plugins.Joystick.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Utilities.MonoCache.System;

namespace Gameplay.UI
{
    public class GameplayUI : Singleton<GameplayUI>
    {
        [Header("Other")]
        [SerializeField] PerkSelectViewUI perkSelect;
        [SerializeField] Joystick joystick;
        [SerializeField] HeroLevelUI heroLevel;
        [SerializeField] BossHitpointsUI bossHitpoints;
        [SerializeField] BossWarningUI bossWarning;

        [Header("Counters")]
        [SerializeField] KillsCounterUI killsCounter;
        [SerializeField] GoldCounterUI goldCounter;
        [SerializeField] SurviveTimeUI surviveTime;

        [Header("Pause")]
        [SerializeField] PauseViewUI pauseMenu;
        [SerializeField] Button pauseButton;
        [SerializeField] Button continueButton;
        [SerializeField] Button quit;

        [Header("Win/Lose")]
        [SerializeField] LoseViewUI lose;

        public PerkSelectViewUI PerkSelect => perkSelect;
        public Joystick Joystick => joystick;
        public HeroLevelUI HeroLevel => heroLevel;
        public SurviveTimeUI SurviveTime => surviveTime;
        public LoseViewUI Lose => lose;
        public KillsCounterUI KillsCounter => killsCounter;
        public GoldCounterUI GoldCounter => goldCounter;
        public BossHitpointsUI BossHitpoints => bossHitpoints;
        public BossWarningUI BossWarning => bossWarning;

        void Awake()
        {
            pauseButton.onClick.AddListener(Pause);
            continueButton.onClick.AddListener(Continue);
            quit.onClick.AddListener(Quit);
            
            bossHitpoints.Hide();
            heroLevel.Show();
            bossWarning.Disable();
        }

        void Pause()
        {
            pauseMenu.Show();
            Game.Instance.Pause();
        }

        void Continue()
        {
            pauseMenu.Hide();
            Game.Instance.Continue();
        }

        void Quit()
        {
            Continue();
            Game.Instance.LoadMainMenu();
        }
    }
}