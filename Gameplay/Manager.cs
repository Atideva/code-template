using System;
using System.Collections;
using PlayerScripts;
using UnityEngine;

namespace Gameplay
{
    public class Manager : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Transform spawnStartPos;
        [SerializeField] private int autoSpawnCount;
        [SerializeField] private int cubesPerCurve;
        [SerializeField] private float curveHeight;
        [SerializeField] private float curveWidth;

        [Header("Setup")]
        [SerializeField] private Cube spawnPrefab;
        [SerializeField] private Player playerPrefab;
        [SerializeField] private Spawner spawner;
        [SerializeField] private Pool pool;
        [SerializeField] private Curve curves;
        [SerializeField] private SpawnerLocator spawnLocator;
        [SerializeField] private CubeList cubeList;

        private Player _player;
        [SerializeField]   private int _cubeId;

        private void Start()
        {
            _cubeId = -1;
            
            pool.SetPrefab(spawnPrefab);
            spawner.SetPool(pool);
            
            var data = GetLocatorData();
            spawnLocator.SetData(data);

            _player = Instantiate(playerPrefab);
            _player.Init(cubeList, spawnStartPos.position);

            _player.Move.OnLandAtCube += OnLandAtCube;
            spawner.OnCubeSpawned += OnCubeSpawn;

            StartCoroutine(AutoSpawn(autoSpawnCount));
        }

        private IEnumerator AutoSpawn(int count)
        {
            var pos0 = spawnLocator.GetPos();
            spawner.Spawn(pos0);
            yield return null;
            
            for (var i = 1; i < count; i++)
            {
                SpawnNextCube();
                yield return null;
            }
        }

        private void OnCubeSpawn(Cube cube)
        {
            cube.SetID(++_cubeId);
            cubeList.Add(cube);
        }

        private void OnLandAtCube()
        {
            cubeList.Dequeue();

            if (spawnLocator.IsNewDataRequire)
            {
                spawnLocator.SetData(GetLocatorData());
            }

            SpawnNextCube();
        }

        private void SpawnNextCube()
        {
            spawnLocator.NextStep();
            spawner.Spawn(spawnLocator.GetPos());
        }
        
        private SpawnerLocatorData GetLocatorData()
        {
            var locatorData = new SpawnerLocatorData
            {
                height = curves.GetHeightCurve(),
                direction = curves.GetDirectionCurve(),
                maxHeight = curveHeight,
                maxWidth = curveWidth,
                totalSteps = cubesPerCurve,
                startPos = cubeList.GetLastCube() ? cubeList.GetLastCube().transform.position : spawnStartPos.position
            };
            return locatorData;
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireCube(spawnStartPos.position, Vector3.one);
        }
    }
}