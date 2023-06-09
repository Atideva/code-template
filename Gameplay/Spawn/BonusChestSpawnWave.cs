using System.Collections;
using Gameplay.Units;
using Meta.Data;
using UnityEngine;
using static Utilities.Extensions.VectorExtensions;

namespace Gameplay.Spawn
{
    public class BonusChestSpawnWave : MonoBehaviour
    {
        [SerializeField] BonusChestWaveData waveSO;
        [SerializeField] Unit player;
        [SerializeField] float radius;

        public void Set(BonusChestWaveData so, Unit p, float r)
        {
            player = p;
            radius = r;
            waveSO = so;

            StartCoroutine(Spawn(so));
        }

        IEnumerator Spawn(BonusChestWaveData data)
        {
            yield return new WaitForSeconds(data.SpawnTime);

            var angle = Random.Range(0f, 360f);

            var pp = (Vector2) player.transform.position;
            //     var dir = (Vector3) GetVector(angle);
            var dir = player.Movement.Direction;
            var dist = Random.Range(radius * 0.6f, radius * 1.4f);
            var pos = pp + dir * dist;

            var c = Instantiate(data.Prefab);
            c.transform.position = pos;
        }
    }
}