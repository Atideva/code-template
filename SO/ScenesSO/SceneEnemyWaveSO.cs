using System.Collections.Generic;
using Meta.Data;
using UnityEngine;

namespace SO.ScenesSO
{
    [CreateAssetMenu(fileName = "New Enemy Wave", menuName = "Gameplay/Levels/Enemy Wave", order = 1000)]
    public class SceneEnemyWaveSO : ScriptableObject
    {
        public List<EnemyWaveData> waves = new();
        
    }
}
