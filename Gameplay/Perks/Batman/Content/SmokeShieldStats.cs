using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Perks.Active.Content
{
    [System.Serializable]
    public class SmokeShieldStats
    {
        [Space(20)]
        [GUIColor(0.2f, 1.0f, 0.2f)] public float duration;
 
        [GUIColor(1.0f, 1f, 0.0f)] [Range(10, 30)] public float cooldown;
    }
}