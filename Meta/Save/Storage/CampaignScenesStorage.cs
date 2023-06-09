using System.Collections.Generic;
using System.Linq;
using Meta.Data;
using Meta.Save.SaveSystem;
using SO.ConfigsSO;
using SO.ScenesSO;
using UnityEngine;

namespace Meta.Save.Storage
{
    public class CampaignScenesStorage : Saveable<CampaignSceneData>
    {
        public ScenesListSO Scenes { get; private set; }

        readonly List<SceneData> _data = new();
        public IReadOnlyList<SceneData> Data => _data;

        public SceneData CurrentScene
            => Scenes.Exist(SaveableData.current.so)
                ? SaveableData.current
                : TakeFirst(Scenes);


        public void Init(ScenesListSO unlocked, ScenesListSO campaign)
        {
            Scenes = campaign;

            if (PlayerPrefs.HasKey(SaveKey))
                Load();
            else
                CreateNewData(campaign);

            Check(campaign);
            Unlocks(unlocked);
            Save();

            InitData();
        }

        #region Init

        void CreateNewData(ScenesListSO scenes)
        {
            SaveableData = new CampaignSceneData();

            foreach (var so in scenes.List)
                SaveableData.scenes.Add(new SceneData {so = so});

            if (SaveableData.scenes.Count > 0)
                SaveableData.current = SaveableData.scenes[0];
        }

        void Check(ScenesListSO scenes)
        {
            foreach (var so in scenes.List)
            {
                if (SaveableData.scenes.Exists(d => d.so == so)) continue;
                AddNewData(so);
            }
        }

        SceneData AddNewData(SceneSO so)
        {
            var newData = new SceneData {so = so};
            SaveableData.scenes.Add(newData);
            return newData;
        }

        void Unlocks(ScenesListSO unlocked)
        {
            foreach (var so in unlocked.List)
                Unlock(so);
        }

        void InitData()
        {
            foreach (var so in Scenes.List)
            {
                var newData
                    = SaveableData.scenes.Exists(d => d.so == so)
                        ? SaveableData.scenes[IndexOf(so)]
                        : new SceneData();

                _data.Add(newData);
            }
        }

        SceneData TakeFirst(ScenesListSO scenes)
        {
            var newCurrentSO = scenes.List[0];
            var newCurrent
                = SaveableData.scenes.Exists(d => d.so == newCurrentSO)
                    ? SaveableData.scenes.Find(d => d.so == newCurrentSO)
                    : AddNewData(newCurrentSO);

            SaveableData.current = newCurrent;
            SaveableData.current.isLock = false;
            Save();
            return newCurrent;
        }

        #endregion

        public void SetCurrent(SceneData data)
        {
            SaveableData.current = data;
            Save();
        }

        SceneSO GetNextLevel(SceneSO so)
        {
            var list = Scenes.List.ToList();

            if (list.Contains(so))
            {
                var index = list.IndexOf(so);
                return index + 1 < list.Count ? list[index + 1] : null;
            }

            return null;
        }

        public void UnlockNext(SceneSO so)
        {
            Unlock(GetNextLevel(so));
        }

        public void Complete(SceneSO so)
        {
            if (!so) return;
            if (SaveableData.scenes.Exists(d => d.so == so))
                SaveableData.scenes[IndexOf(so)].isComplete = true;
            else
                SaveableData.scenes.Add(new SceneData()
                {
                    so = so,
                    isLock = false,
                    isComplete = true
                });
            Save();
        }

        public void Unlock(SceneSO so)
        {
            if (!so) return;

            if (SaveableData.scenes.Exists(d => d.so == so))
                SaveableData.scenes[IndexOf(so)].isLock = false;
            else
            {
                SaveableData.scenes.Add(new SceneData
                {
                    so = so,
                    isLock = false,
                    isComplete = false
                });
            }

            Save();
        }

        int IndexOf(SceneSO so)
        {
            for (var i = 0; i < SaveableData.scenes.Count; i++)
                if (SaveableData.scenes[i].so == so)
                    return i;

            Debug.LogError("Cant find proper level DatabaseID in data");
            return 0;
        }

        public void RefreshCurrentRecord(int seconds)
        {
            if (seconds <= CurrentScene.recordSeconds) return;
            CurrentScene.recordSeconds = seconds;
            Save();
        }
    }
}