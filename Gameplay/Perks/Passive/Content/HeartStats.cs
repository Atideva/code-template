using Sirenix.OdinInspector;
using UnityEngine;

// ReSharper disable InconsistentNaming

namespace Gameplay.Perks.Passive.Content
{
    [System.Serializable]
    public class HeartStats
    {
        [LabelText("+MAX HP %")]
        [GUIColor(0.2f, 1.0f, 0.2f)] [Space(20)] [Range(0, 50)] [SerializeField] float bonusHP;
        public float BonusHP => bonusHP * 0.01f;
    }
}