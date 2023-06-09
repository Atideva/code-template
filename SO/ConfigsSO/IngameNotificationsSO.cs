using Gameplay.UI;
using Meta.Enums;
using UnityEngine;

namespace SO.ConfigsSO
{
    [CreateAssetMenu(fileName = "Ingame Notifications", menuName = "Config/Ingame Notifications")]
    public class IngameNotificationsSO : ScriptableObject
    {
        [SerializeField] AchievementUI achievementTier1;
        [SerializeField] AchievementUI achievementTier2;
        [SerializeField] AchievementUI achievementTier3;
        [SerializeField] AchievementUI achievementTier4;
        [SerializeField] AchievementUI achievementTier5;
        [SerializeField] AchievementUI achievementTier6;
 
        public AchievementUI GetAchievementPrefab(TierEnum tier) =>
            tier switch
            {
                TierEnum.Tier1 => achievementTier1,
                TierEnum.Tier2 => achievementTier2,
                TierEnum.Tier3 => achievementTier3,
                TierEnum.Tier4 => achievementTier4,
                TierEnum.Tier5 => achievementTier5,
                TierEnum.Tier6 => achievementTier6,
                _ => null
            };
    }
}