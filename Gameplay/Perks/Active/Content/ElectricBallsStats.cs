using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Perks.Active.Content
{
    [System.Serializable]
    public class ElectricBallsStats
    {
        [Space(20)]
        [GUIColor(0.2f, 0.8f, 1f)] [Range(3, 15)] public float radius;
        [GUIColor(1.0f, 0.7f, 0.0f)] public float dps;
      
        [GUIColor(0.9f, 0.9f, 0.9f)] public bool ballBlockBullets;
        [GUIColor(0.9f, 0.9f, 0.9f)] public int ballCount;
        [GUIColor(0.9f, 0.9f, 0.9f)] public float ballSize;
      
        [GUIColor(1.0f, 1f, 0.0f)] [Range(0, 15)] public float lifeTime;
        [GUIColor(1.0f, 1f, 0.0f)] [Range(0, 15)] public float cooldown;
    }
}