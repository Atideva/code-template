using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Perks.Active.Content
{
    [System.Serializable]
    public class NukeStats
    {
        [Space(20)]
 
        [GUIColor(1.0f, 0.7f, 0.0f)] public float damage = 10;
       
       // [GUIColor(0.9f, 0.9f, 0.9f)] public int count = 1;
        //[GUIColor(0.9f, 0.9f, 0.9f)] public float multiBombInterval = 2;
       
        [GUIColor(1.0f, 1f, 0.0f)] [Range(30, 300)] public float cooldown = 5;
    }
}