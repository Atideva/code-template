using GameManager;
using Gameplay.UI.Views;
using Gameplay.Units;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.UI.Controllers
{
    public class LoseController : MonoBehaviour
    {
        Hero Player => Scene.Instance.Player.Hero;
        LoseViewUI UI => GameplayUI.Instance.Lose;

        void Start()
        {
            Player.Hitpoints.OnDeath += ShowLose;
        }

        [Button(ButtonSizes.Gigantic), DisableInEditorMode]
        void ShowLose()
        {
            var kills = GameplayUI.Instance.KillsCounter.Total;

            var min = Scene.Instance.PlayTime.Minutes;
            var sec = Scene.Instance.PlayTime.Seconds;
            var survive = TimeFormat(min, sec);

            var хуйня = Game.Instance.CurrentLevelData;
            var seconds = хуйня.recordSeconds;
            var mins = seconds / 60;
            var secs = seconds - min * 60;
            var best = TimeFormat(mins, secs);

            
            var levelName = хуйня.so.Name.ToString();

            UI.SetKills(kills);
            UI.SetSurviveTime(survive);
            UI.SetBestTime(best);
            UI.SetLevelName(levelName);
            UI.Show();

            UI.OnExitToMainMenu += Exit;
            Game.Instance.Pause();
        }

        void Exit()
        {
            UI.OnExitToMainMenu -= Exit;
            Game.Instance.LoadMainMenu();
        }

        string TimeFormat(int min, int sec)
        {
            var minPrefix = min < 10 ? "0" : "";
            var secPrefix = sec < 10 ? "0" : "";
            return minPrefix + min + ":" + secPrefix + sec;
        }
    }
}