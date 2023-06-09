using System;
using GameManager;
using Gameplay.Units;
using SO.ConfigsSO;
using SO.UnitsSO;
using UnityEngine;
using Utilities.Pools;

namespace Gameplay.Drop
{
    public class ExperienceDropSpawner : MonoBehaviour
    {
        [SerializeField] SceneExperienceDropPools pool;
        [SerializeField] SceneExperienceDrop list;
        ExperienceDropSO _config;

        void Start()
        {
            _config = Game.Instance.Config.ExperienceDrop;
            GameplayEvents.Instance.OnUnitDeath += OnUnitDeath;
        }

        void OnDisable()
        {
            GameplayEvents.Instance.OnUnitDeath -= OnUnitDeath;
        }

        void OnUnitDeath(Unit unit)
        {
            if (!unit.SO) return;
            if (unit.SO is not EnemySO so) return;
            
            var type = so.ExperienceDrop;
            var data = _config.GetDrop(type);
            var drop = pool.Pool(data.prefab).Get();

            drop.Set(data.experienceValue);
            drop.transform.position = unit.transform.position;
            
            list.Add(drop);

        }
    }
}