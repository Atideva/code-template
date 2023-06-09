using GameManager;

namespace Gameplay.Achievements
{
    public class QuestCompleteListener : AchievementListener
    {
        void Start()
        {
            GameplayEvents.Instance.OnQuestComplete += OnBossDeath;
        }

        void OnBossDeath()
        {
            ADD_VALUE(1);
        }
    }
}