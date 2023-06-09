using System.Collections.Generic;
using Meta.Data;
using UnityEngine;

namespace SO.ScenesSO
{
    [CreateAssetMenu(fileName = "New Chest Wave", menuName = "Gameplay/Levels/Chest Wave", order = 1000)]
    public class SceneBonusChestsWaveSO : ScriptableObject
    {
        public List<BonusChestWaveData> chests = new();
    }
}