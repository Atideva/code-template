using GameManager;
using Gameplay.Units;
using UnityEngine;

namespace Gameplay.Achievements
{
    public class MonsterKillsListener : AchievementListener
    {
        [SerializeField] float sendFrequency = 100;
        string _enemyTeam;
        float _kills;

        void Start()
        {
            _enemyTeam = Game.Instance.Config.Settings.EnemyTeam;
            GameplayEvents.Instance.OnUnitDeath += OnUnitDeath;
        }


        void OnUnitDeath(Unit unit)
        {
            if (unit.Team == _enemyTeam)
            {
                _kills++;
            }

            if (_kills >= sendFrequency)
            {
                ADD_VALUE(_kills);
                _kills = 0;
            }
        }
    }
}