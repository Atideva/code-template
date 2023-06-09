using System.Collections.Generic;
using System.Linq;
using GameManager;
using Gameplay.Units;
using Meta.Static;
using Sirenix.OdinInspector;
using SO.UnitsSO;
using UnityEngine;
using Utilities.Pools;


namespace Gameplay
{
    public class SceneUnits : SerializedMonoBehaviour
    {
        [InfoBox("TODO: subscribe to 'Change Team' event and refresh dict", InfoMessageType.Warning)]
        [SerializeField] SceneUnitsPools pool;

        [SerializeField] Dictionary<string, Dictionary<Transform, Unit>> units = new();

        public int Total
            => units.Sum(pair => pair.Value.Count);

        public Dictionary<string, Dictionary<Transform, Unit>> All => units;

        void Start()
            => GameplayEvents.Instance.OnUnitDeath += Unregister;

        void OnDisable()
            => GameplayEvents.Instance.OnUnitDeath -= Unregister;

        public void RemoveAll(string team)
        {
            if(!units.ContainsKey(team)) return;
            
            var dict = units[team];
            List<Unit> remove = new();
          
            foreach (var unit in dict.Values)
            {
                unit.gameObject.SetActive(false);
                remove.Add(unit);
  
            }

            foreach (var unit in remove)
                Unregister(unit);
        }

        public Unit Spawn(UnitSO so)
        {
            var unit = pool.Get(so);
            Register(unit);
            return unit;
        }

        public Unit Spawn(BossSO so)
        {
            var unit = pool.Get(so);
            Register(unit);
            return unit;
        }

        public void ClearAll()
        {
            var list = new List<Unit>();
            foreach (var pair in units)
            {
                if (pair.Key == Tags.PLAYER) continue;

                foreach (var unit in pair.Value)
                    list.Add(unit.Value);
            }

            foreach (var unit in list)
            {
                unit.Kill();
            }
        }


        public Unit Get(string team, Transform t)
        {
            if (!units.ContainsKey(team)) return null;
            var dict = units[team];
            return dict.ContainsKey(t) ? dict[t] : null;
        }

        public void Register(Unit u)
        {
            var team = u.Team;
            if (!units.ContainsKey(team))
            {
                var dict = new Dictionary<Transform, Unit> {{u.transform, u}};
                units.Add(team, dict);
            }
            else
            {
                units[team].Add(u.transform, u);
            }
        }

        public void Unregister(Unit unit)
        {
            var team = unit.Team;
            if (!units.ContainsKey(team)) return;
            var dict = units[team];
            if (dict.ContainsKey(unit.transform))
                dict.Remove(unit.transform);
        }
    }
}