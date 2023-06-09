using Meta.Data;
using Meta.Save.SaveSystem;
using SO.ConfigsSO;
using UnityEngine;


namespace Meta.Save.Storage
{
    public class SceneGameplayStorage : Saveable<SceneGameplayData>
    {
        public void Init(FirstLaunchSO config)
        {
            if (PlayerPrefs.HasKey(SaveKey))
            {
                Load();
            }
            else
            {
                SaveableData = new SceneGameplayData();
                Save();
            }
        }

        public void SaveScene(SceneGameplayData data)
        {
            SaveableData = data;
            Save();
        }

        public void Clear()
        {
            SaveableData = new SceneGameplayData();
            Save();
        }
    }
}