using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Perks.Passive.Content
{
    [System.Serializable]
    public class ArmorStats
    {
        [Space(20)]
        [GUIColor(0.2f, 1.0f, 0.2f)] [Range(0,50)] [SerializeField] [LabelText("Damage Reduction %")]   float damageReduction;
        public float DamageReduction => damageReduction * 0.01f;
    }
}