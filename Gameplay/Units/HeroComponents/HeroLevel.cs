using System;
using System.Collections.Generic;
using System.Linq;
using GameManager;
using Gameplay.UI;
using Meta.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Units.HeroComponents
{
    public class HeroLevel : MonoBehaviour
    {
        [ReadOnly] [SerializeField] HeroLevelData data;
        [ReadOnly] [SerializeField] Hero _hero;
        HeroLevelUI _levelUI;
        float _expPerKill;

        void Start() => GameplayEvents.Instance.OnUnitDeath += AddKillExp;
        void OnDisable() => GameplayEvents.Instance.OnUnitDeath -= AddKillExp;
        void AddKillExp(Unit dead) => AddExperience(_expPerKill);


        [Button(ButtonSizes.Gigantic), DisableInEditorMode]
        public void AddLevel() => AddExperience(data.require);


        public void AddExperience(float amount)
        {
            if (data.table.Count == 0) return;

            data.experience += amount;
            _levelUI.RefreshExperience(ExperienceValue);

            if (data.experience >= data.require)
                LevelUp();
        }


        public void Init(Hero hero, IEnumerable<float> experienceTable, float expPerKill, HeroLevelUI levelUI)
        {
            _levelUI = levelUI;
            _hero = hero;
            _expPerKill = expPerKill;

            data.level = 1;
            data.table = experienceTable.ToList();
            data.require = Require(data.level);

            levelUI.RefreshLevel(data.level);
            levelUI.RefreshExperience(ExperienceValue);
        }

        float ExperienceValue => data.experience / data.require;

        public HeroLevelData Data => data;

        float Require(int lvl) =>
            lvl <= data.table.Count
                ? data.table[lvl - 1]
                : data.table[^1];
 
        
        void LevelUp()
        {
            var levelUps = 0;
            var maxIteration = 100;

            while (levelUps < maxIteration)
            {
                data.level++;
                data.experience -= data.require;
                data.require = Require(data.level);

                levelUps++;
                if (data.experience < data.require)
                    break;
            }

            _levelUI.RefreshLevel(data.level);
            GameplayEvents.Instance.HeroLevelUp(_hero, levelUps);
        }
    }
}