using System.Collections.Generic;
using Gameplay.Units;
using Meta.Data;
using Sirenix.OdinInspector;
using SO.ScenesSO;
using UnityEngine;


namespace Gameplay.Spawn
{
    public class BonusChestsSpawn : MonoBehaviour
    {
        public SceneSO sceneConfig;
        [SerializeField] Unit player;
        [SerializeField] float radius;


        [SerializeField] Transform wavesContainer;

        [Header("DEBUG")]
        [SerializeField] [DisableInInlineEditors]
        List<BonusChestSpawnWave> waves = new();


        void Start()
        {
            CreateWaveSpawns(sceneConfig);
        }

        void CreateWaveSpawns(SceneSO scene)
        {
            if (!scene.Creeps) return;
            CreateWaves(scene.Chests.From0To5min, "Waves from 0 to 5 min");
            CreateWaves(scene.Chests.From5To10min, "Waves from 5 to 10 min");
            CreateWaves(scene.Chests.From10To15min, "Waves from 10 to 15 min");
        }

        void CreateWaves(SceneBonusChestsWaveSO so, string naming)
        {
            if (!so) return;

            var cont = new GameObject {name = naming};
            cont.transform.SetParent(wavesContainer);

            for (var i = 0; i < so.chests.Count; i++)
            {
                var wave = so.chests[i];
                NewWave(wave, "Wave - " + (i + 1), cont.transform);
            }
        }

        void NewWave(BonusChestWaveData waveSO, string naming, Transform container)
        {
            var cont = new GameObject {name = naming};
            cont.transform.SetParent(container);

            var wave = cont.AddComponent<BonusChestSpawnWave>();
            wave.Set(waveSO, player, radius);

            waves.Add(wave);
        }
    }
}