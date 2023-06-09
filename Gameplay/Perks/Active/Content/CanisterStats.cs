using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Perks.Active.Content
{
    [System.Serializable]
    public class CanisterStats
    {
        [Space(20)]
      //  [GUIColor(0.2f, 0.8f, 1f)] [Range(2, 10)] public float offset = 5;
        [GUIColor(0.2f, 0.8f, 1f)] [Range(2, 10)] public float wallWidth = 4f;
        [GUIColor(1.0f, 0.7f, 0.0f)] public float dps = 10;
        
        [GUIColor(0.9f, 0.9f, 0.9f)] public int count = 1;
        
        [GUIColor(1.0f, 1f, 0.0f)] [Range(0, 15)] public float lifeTime=3f;
        [GUIColor(1.0f, 1f, 0.0f)] [Range(0, 15)] public float cooldown = 5;
    }
}