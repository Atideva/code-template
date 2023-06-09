using System.Collections.Generic;
using SO;
using SO.UnitsSO;
using UnityEngine;

namespace Meta.Data
{
    [System.Serializable]
    public class SceneUnitsData
    {
        public string team;
        public List<UnitSO> unitsSO = new();
        public List<HitpointsData> hitpoints = new();
        public List<Vector3> positions;
 
    }
}