using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Perks.Batman.Content
{
    [System.Serializable]
    public class BoomerangStats  
    {
 
        [Space(20)]
        [GUIColor(0.2f, 0.8f, 1f)] [Range(1, 10)] public float size;
        [GUIColor(1.0f, 0.7f, 0.0f)] public float damage = 10;
       
        [GUIColor(0.9f, 0.9f, 0.9f)] public int count = 1;
       
        [GUIColor(1.0f, 1f, 0.0f)] [Range(0, 15)] public float cooldown = 5;
 
    }
}
