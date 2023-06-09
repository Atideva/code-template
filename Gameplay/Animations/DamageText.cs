using System;
using DamageNumbersPro;
using GameManager;
using Gameplay.Spawn;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Animations
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField] DamageNumber playerTakenDamage;
        [SerializeField] DamageNumber unitsTakenDamage;
        [SerializeField] Vector2 offset;
        [SerializeField, ReadOnly]  ScenePlayer _player;

        void Start()
        {
            GameplayEvents.Instance.OnDamageTaken += SpawnText;
        }

        void OnDisable()
        {
            GameplayEvents.Instance.OnDamageTaken -= SpawnText;
        }

        public void Init(ScenePlayer player)
        {
            _player = player;
        }

        void SpawnText(Transform unit, float dmg)
        {
            if (unit == _player.Hero.transform)
            {
                playerTakenDamage.Spawn((Vector2) unit.position + offset, dmg);
            }
            else
            {
                unitsTakenDamage.Spawn((Vector2) unit.position + offset, dmg);
            }
           
        }
    }
}