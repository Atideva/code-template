using System.Collections.Generic;
using System.Linq;
using Meta.Data;
using Meta.Enums;
using Meta.Save.SaveSystem;
using SO.ConfigsSO;
using UnityEngine;

namespace Meta.Save.Storage
{
    public class OptionsStorage : Saveable<OptionsData>
    {
        public IReadOnlyList<OptionToggleData> Toggles => SaveableData.toggles;

        public void Init(FirstLaunchSO config)
        {
            if (PlayerPrefs.HasKey(SaveKey))
            {
                Load();
            }
            else
            {
                SaveableData = config.Options;
                Save();
            }
        }

        public void SaveData(OptionToggleEnum type, bool toggle)
        {
            var find
                = SaveableData.toggles.FirstOrDefault(data => data.type == type);
            
            if (find != null)
                find.toggle = toggle;
            else
                SaveableData.toggles.Add(new OptionToggleData(type, toggle));

            Save();
        }
    }
}