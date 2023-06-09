using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Perks.Active.Content
{
    [System.Serializable]
    public class RpgStats
    {
        [Space(20)]
        [GUIColor(0.2f, 0.8f, 1f)] [Range(1, 10)] public float explosionRadius;
        [GUIColor(1.0f, 0.7f, 0.0f)] public float damage = 10;
       
        [GUIColor(0.9f, 0.9f, 0.9f)] public int count = 1;
        [GUIColor(0.9f, 0.9f, 0.9f)] public float countInterval = 0.1f;
       
        [GUIColor(1.0f, 1f, 0.0f)] [Range(0, 15)] public float cooldown = 5;
    }
}