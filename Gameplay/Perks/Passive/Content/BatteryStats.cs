using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Perks.Passive.Content
{
    [System.Serializable]
    public class BatteryStats
    {
        [Space(20)]
        [GUIColor(0.2f, 1.0f, 0.2f)] [Range(0,50)] [SerializeField] [LabelText("Duration Increase %")]   float durationBonus;
        public float DurationBonus => durationBonus * 0.01f;
    }
}