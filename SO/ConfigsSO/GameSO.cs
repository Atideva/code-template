using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.Odin;

namespace SO.ConfigsSO
{
    [CreateAssetMenu(fileName = "Game", menuName = "Config/GAME", order = 1000)]
    public class GameSO : ScriptableObject
    {
        [FoldoutGroup("CONFIGS"), SerializeField, Required, InlineEditor] LogSO log;
        [FoldoutGroup("CONFIGS"), SerializeField, Required, InlineEditor] FirstLaunchSO firstLaunch;
     
        
        [FoldoutGroup("GAMEPLAY"), SerializeField, Required, InlineEditor] ScenesListSO campaignScenes;
        [FoldoutGroup("GAMEPLAY"), SerializeField, Required, InlineEditor] HeroesListSO heroes;
        [FoldoutGroup("GAMEPLAY"), SerializeField, Required, InlineEditor] HeroLevelSO heroLevel;
        [FoldoutGroup("GAMEPLAY"), SerializeField, Required, InlineEditor] ExperienceDropSO experienceDrop;
        [FoldoutGroup("GAMEPLAY"), SerializeField, Required, InlineEditor] AchievementsListSO achievements;
        [FoldoutGroup("GAMEPLAY"), SerializeField, Required, InlineEditor] SettingsSO settings;

        [FoldoutGroup("SETTINGS"), SerializeField, Required, InlineEditor] IconsSO icons;
        [FoldoutGroup("SETTINGS"), SerializeField, Required, InlineEditor] EquipmentTiersSO itemTiers;
        [FoldoutGroup("SETTINGS"), SerializeField, Required, InlineEditor] IngameNotificationsSO ingameNotifications;
        [FoldoutGroup("SETTINGS"), SerializeField, Required, InlineEditor] AudioSO audio;
        [FoldoutGroup("SETTINGS"), SerializeField, Required, InlineEditor] AndroidSO android;
        [FoldoutGroup("SETTINGS"), SerializeField, Required, InlineEditor] ScriptableObjectReferenceCache database;
        [FoldoutGroup("SETTINGS"), SerializeField, Required, InlineEditor] DebugSO debug;
  
        
        public AudioSO Audio => audio;
        public FirstLaunchSO FirstLaunch => firstLaunch;
        public DebugSO Debug => debug;
        public IconsSO Icons => icons;
        public AndroidSO Android => android;
        public EquipmentTiersSO ItemTiers => itemTiers;
        public LogSO Logs => log;
        public SettingsSO Settings => settings;
        public ScriptableObjectReferenceCache Database => database;
        public HeroLevelSO HeroLevelTable => heroLevel;
        public ExperienceDropSO ExperienceDrop => experienceDrop;
        public ScenesListSO CampaignScenes => campaignScenes;
        public AchievementsListSO Achievements => achievements;
        public IngameNotificationsSO IngameNotifications => ingameNotifications;
        public HeroesListSO Heroes => heroes;
        
    }
}