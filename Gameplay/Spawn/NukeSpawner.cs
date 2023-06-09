using Gameplay.Drop;
using Gameplay.Pickables;
using Gameplay.Units;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Spawn
{
    public class NukeSpawner : MonoBehaviour
    {
        [SerializeField] NukeBomb prefab;
        [SerializeField] float moveCooldown = 10;
        [SerializeField] [ReadOnly] NukeBomb bomb;
        [SerializeField] [ReadOnly] bool isMoved;

        SceneUnits _units;
        int trigger;
        Hero _hero;
        float _spawnDistance;
        float _createCooldown;
        float createTimer;

        public void Init(SceneUnits units, int triggerCount, float createCooldown, float spawnDistance)
        {
            _createCooldown = createCooldown;
            createTimer = createCooldown;

            _spawnDistance = spawnDistance;
            _units = units;
            trigger = triggerCount;
        }

        void FixedUpdate()
        {
            if (!_units) return;
            createTimer -= Time.fixedDeltaTime;
            if (createTimer > 0) return;

            if (_units.Total < trigger) return;
            Create();
            Move();
        }

        void Create()
        {
            if (bomb) return;
            bomb = Instantiate(prefab);
            createTimer = _createCooldown;
        }

        void Move()
        {
            if (isMoved) return;
            isMoved = true;
            Invoke(nameof(AllowMovement), moveCooldown);

            var dist = Vector2.Distance(bomb.transform.position, _hero.transform.position);
            if (dist < _spawnDistance * 1.6f) return;

            var dir = _hero.Movement.Direction;
            var pos = (Vector2) _hero.transform.position + dir * _spawnDistance;
            bomb.transform.position = pos;
        }

        void AllowMovement() => isMoved = false;
    }
}