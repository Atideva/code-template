using System.Collections.Generic;
using Meta.Interface;

namespace Meta.Data
{
    [System.Serializable]
    public class OptionsData: DebugDataSO, ISave
    {
        public List<OptionToggleData> toggles = new();

 
 
    }
}