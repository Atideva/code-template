using System.Collections.Generic;
using Sirenix.OdinInspector;
using SO.AchievementsSO;
using UnityEngine;

namespace SO.ConfigsSO
{
    [CreateAssetMenu(fileName = "Achievements List", menuName = "Config/Achievements List")]
    public class AchievementsListSO : ScriptableObject
    {
 
        [InfoBox("Please, add Achievements here manually / Незабудь добавить сюда список ачивок")]
        [Space(20)]
        [ListDrawerSettings(Expanded = true), InlineEditor]
        [SerializeField] List<AchievementSO> list = new();

        public IReadOnlyList<AchievementSO> List => list;
 
    }
}
