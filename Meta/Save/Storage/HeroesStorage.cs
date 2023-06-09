using System;
using System.Collections.Generic;
using System.Linq;
using Meta.Data;
using Meta.Save.SaveSystem;
using SO.ConfigsSO;
using SO.UnitsSO;
using UnityEngine;


namespace Meta.Save.Storage
{
    public class HeroesStorage : Saveable<HeroesData>
    {
        public event Action OnSelected = delegate { };

        public void Init(FirstLaunchSO config, HeroesListSO allHeroes)
        {
            if (PlayerPrefs.HasKey(SaveKey))
            {
                Load();
            }
            else
            {
                SaveableData = config.Heroes;
                SaveableData.selected = SaveableData.heroes[0];
                Add(allHeroes);
                Save();
            }
        }

        public IReadOnlyList<HeroCardData> List => SaveableData.heroes.ToList();
        public HeroCardData Current => SaveableData.selected;

        void Add(HeroesListSO all)
        {
            foreach (var hero in all.List)
                GetData(hero);
        }

        public HeroCardData GetData(HeroSO so)
        {
            if (SaveableData.heroes.Exists(data => data.so == so))
                return SaveableData.heroes.Find(data => data.so == so);

            var newData = new HeroCardData
            {
                so = so,
                lvl = 1,
                owned = false
            };

            SaveableData.heroes.Add(newData);
            return newData;
        }

        public void SetSelected(HeroSO so)
        {
            var data = GetData(so);
            SaveableData.selected = data;
            Save();
            OnSelected();
        }

        public void LevelUp(HeroSO so )
        {
            var data = GetData(so);
            data.lvl++;
            Save();
        }

        public void SetOwned(HeroSO so)
        {
            var data = GetData(so);
            data.owned = true;
            Save();
        }
    }
}