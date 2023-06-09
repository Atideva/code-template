using GameManager;
using Gameplay.Units;

namespace Gameplay.Achievements
{
    public class BossKillsListener : AchievementListener
    {
    
        void Start()
        {
            GameplayEvents.Instance.OnBossDeath += OnBossDeath;
        }

        void OnBossDeath(Unit boss)
        {
            ADD_VALUE(1);
        }
    }
}
