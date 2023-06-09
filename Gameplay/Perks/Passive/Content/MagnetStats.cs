using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Perks.Passive.Content
{
    [System.Serializable]
    public class MagnetStats
    {
        [Space(20)]
        [GUIColor(0.2f, 1.0f, 0.2f)] [Range(0,300)] [SerializeField] [LabelText("Radius Increase %")]   float radiusBonus;
        public float RadiusBonus => radiusBonus * 0.01f;
    }
}