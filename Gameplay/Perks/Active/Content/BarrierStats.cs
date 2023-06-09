using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Perks.Active.Content
{
    [System.Serializable]
    public class BarrierStats
    {
        [Space(20)]
        [GUIColor(0.2f, 1.0f, 0.2f)] public float capacity;
        [GUIColor(0.7f, 1.0f, 0.7f)] public float capacityRestorePerSec;
        [GUIColor(0.7f, 1.0f, 0.7f)] public float capacityRestoreDelay;
        [GUIColor(1.0f, 1f, 0.0f)] [Range(10, 30)] public float cooldown;
    }
}