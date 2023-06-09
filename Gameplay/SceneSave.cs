using GameManager;
using Meta.Data;
using UnityEngine;

namespace Gameplay
{
    public class SceneSave : MonoBehaviour
    {
        Scene _scene;

        public void Init(Scene scene)
        {
            _scene = scene;
        }

        void OnApplicationQuit()
        {
            Save();
        }

        void Save()
        {
            var newSave = new SceneGameplayData();
            SaveHero(newSave);
            SaveUnits(newSave);
            
            Game.Instance.Storage.SceneGameplay.SaveScene(newSave);
        }

        void SaveHero(SceneGameplayData gameplay)
        {
            gameplay.hero = new HeroSaveData
            {
                position = _scene.Player.Hero.transform.position,
                perksData = _scene.Player.Hero.Perks.Data,
                levelData = _scene.Player.Hero.Level.Data
            };
        }

        void SaveUnits(SceneGameplayData gameplay)
        {
            var dictionary = _scene.Units.All;
            foreach (var (key, value) in dictionary)
            {
                var unitsData = new SceneUnitsData {team = key};

                foreach (var unitPair in value)
                {
                    var unit = unitPair.Value;
                    unitsData.unitsSO.Add(unit.SO);
                }

                gameplay.units.Add(unitsData);
            }
        }
    }
}