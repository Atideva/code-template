using Gameplay.Units;
using NaughtyAttributes;
using SO.UnitsSO;
using UnityEngine;
using Utilities.Extensions;
using static Utilities.Extensions.VectorExtensions;

namespace Gameplay.Spawn
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] float spawnFreeAngle = 55;
        [SerializeField] float radius;
        [SerializeField] float bossRadius = 5;
        [SerializeField] [Tag] string team;
        SceneUnits _units;
        BlockSpawner _blockSpawner;
        ScenePlayer _player;
        //[SerializeField] [Layer] string bulletsLayer;

        public void Init(ScenePlayer player, SceneUnits units, BlockSpawner blockSpawner)
        {
            _player = player;
            _blockSpawner = blockSpawner;
            _units = units;
        }

        public void Spawn(UnitSO enemy)
        {
            if (!_player || !_player.Hero) return;

            var moveDir = _player.Hero.Movement.Direction;
            var moveAngle = GetAngle(moveDir);

            var from = (moveAngle - spawnFreeAngle / 2) % 360;
            var to = (moveAngle + spawnFreeAngle / 2) % 360;
            if (from < 0) from += 360;
            if (to < 0) to += 360;

            //  var min = from < to ? from : to;
            //   var max = from < to ? to : from;
 
            float angle;
            if (from < to)
                angle = Random.value > 0.5f ? Random.Range(0f, from) : Random.Range(to, 360);
            else
                angle = Random.Range(to, from);

            var pp = _player.Hero.transform.position;
            var dir = (Vector3) GetVector(angle);
            var dist = Random.Range(radius * 0.6f, radius * 1.4f);
            var pos = pp + dir * dist;

            var unit = _units.Spawn(enemy);
            unit.transform.position = pos;

            if (unit.HasAI)
                unit.AI.FollowTarget(_player.Hero.transform);

            //unit.SetBulletsLayers(bulletsLayer);
            unit.SetTeam(team);
            unit.Reset();
        }

        public Unit Spawn(BossSO boss)
        {
            var angle = Random.Range(0f, 360f);

            var pp = _player.Position;
            var dir = (Vector3) GetVector(angle);
            var pos = pp + dir * bossRadius;

            var unit = _units.Spawn(boss);
            unit.transform.position = pos;

            if (unit.HasAI)
                unit.AI.FollowTarget(_player.Hero.transform);

            //unit.SetBulletsLayers(bulletsLayer);
            unit.SetTeam(team);
            unit.Reset();

            _blockSpawner.SpawnBox(
                _player.Position,
                boss.MoveBlockSettings.moveBlockPrefab,
                boss.MoveBlockSettings.width,
                boss.MoveBlockSettings.height);

            return unit;
        }
    }
}