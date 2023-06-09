using Gameplay.Drop;
using Gameplay.Pickables;
using Gameplay.Units;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Spawn
{
    public class MagnetSpawner : MonoBehaviour
    {
        [SerializeField] Magnet prefab;
        [SerializeField] float moveCooldown = 10;
        [SerializeField] [ReadOnly] Magnet magnet;
        [SerializeField] [ReadOnly] bool cooldown;

        SceneExperienceDrop _drop;
        int trigger;
        Hero _hero;
        float _spawnDistance;

        public void Init(SceneExperienceDrop drop, int magnetTrigger, float spawnDistance, Hero hero)
        {
            _spawnDistance = spawnDistance;
            _hero = hero;
            _drop = drop;
            trigger = magnetTrigger;
        }

        void FixedUpdate()
        {
            if (!_drop) return;
            if (cooldown) return;
            if (_drop.Total < trigger) return;

            CreateMagnet();
            MoveMagnet();
        }

        void CreateMagnet()
        {
            if (magnet) return;
            magnet = Instantiate(prefab);
        }

        void MoveMagnet()
        {
            var dist = Vector2.Distance(magnet.transform.position, _hero.transform.position);
            if (dist < _spawnDistance * 2f) return;

            var dir = _hero.Movement.Direction;
            var pos = (Vector2) _hero.transform.position + dir * _spawnDistance;
            magnet.transform.position = pos;

            cooldown = true;
            Invoke(nameof(CooldownFalse), moveCooldown);
        }

        void CooldownFalse() => cooldown = false;
    }
}