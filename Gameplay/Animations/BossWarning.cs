
using Gameplay.UI;
using Meta.UI;
using UnityEngine;

namespace Gameplay.Animations
{
    public class BossWarning : MonoBehaviour
    {
        PlayTime _playTime;
        public int secondsBeforeBoss = 10;

        int _start1;
        int _start2;
        int _start3;
        int _end1;
        int _end2;
        int _end3;
       
        BossWarningUI ui;

        void Start()
        {
            ui = GameplayUI.Instance.BossWarning;
        }

        public void Init(PlayTime playTime)
        {
            _playTime = playTime;

            var boss1 = 300;
            var boss2 = 600;
            var boss3 = 900;

            _start1 = boss1 - secondsBeforeBoss;
            _start2 = boss2 - secondsBeforeBoss;
            _start3 = boss3 - secondsBeforeBoss;

            _end1 = boss1;
            _end2 = boss2;
            _end3 = boss3;
        }

        void FixedUpdate()
        {
            var sec = _playTime.TotalSeconds;
            
            if (sec == _start1 || sec == _start2 || sec == _start3)
                ui.Show();

            if (sec == _end1 || sec == _end2 || sec == _end3)
                ui.Hide();
        }
    }
}