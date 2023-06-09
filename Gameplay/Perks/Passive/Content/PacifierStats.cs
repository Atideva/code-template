using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Perks.Passive.Content
{
    [System.Serializable]
    public class PacifierStats
    {
        [Space(20)]
        [GUIColor(0.2f, 1.0f, 0.2f)] [Range(0,10)] [SerializeField] [LabelText("Heal %")]   float healPercent;
        [GUIColor(0.7f, 1.0f, 0.7f)] public float healIntervalSec;

        public float HealPercent => healPercent * 0.01f;
    }
}