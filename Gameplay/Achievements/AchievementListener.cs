using System;
using Meta.Data;
using Sirenix.OdinInspector;
using SO.AchievementsSO;
using UnityEngine;

namespace Gameplay.Achievements
{
    public class AchievementListener : MonoBehaviour
    {
        public event Action<AchievementSO, float> OnValueAdd = delegate { };
        [ReadOnly] [InlineEditor] [SerializeField] protected AchievementSO achievement;
        [ReadOnly] [SerializeField]   AchievementData data;
        public void SetSO(AchievementSO so) => achievement = so;
        public void SetData(AchievementData d) => data = d;

        protected void ADD_VALUE(float value) => OnValueAdd(achievement, value);
    }
}