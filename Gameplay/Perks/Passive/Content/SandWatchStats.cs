using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Perks.Passive.Content
{
    [System.Serializable]
    public class SandWatchStats
    {
        [Space(20)]
        [GUIColor(0.2f, 1.0f, 0.2f)] [Range(0,50)] [SerializeField] [LabelText("Cooldown Reduction %")]   float cooldownReduction;
        public float CooldownReduction => cooldownReduction * 0.01f;
    }
}