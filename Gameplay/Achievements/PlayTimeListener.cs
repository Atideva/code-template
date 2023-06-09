using System;
using GameManager;

namespace Gameplay.Achievements
{
    public class PlayTimeListener : AchievementListener
    {
        void Start()
        {
            GameplayEvents.Instance.OnMinutePassed += OneMoreMinutePassed;
        }

        void OnDisable()
        {
            GameplayEvents.Instance.OnMinutePassed -= OneMoreMinutePassed;
        }

        void OneMoreMinutePassed()
        {
            ADD_VALUE(1);
        }
    }
}