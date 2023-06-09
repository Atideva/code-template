using Meta.Enums;

namespace Meta.Data
{
    [System.Serializable]
    public class OptionToggleData
    {
        public OptionToggleEnum type;
        public bool toggle;

        public OptionToggleData(OptionToggleEnum newType, bool newToggle)
        {
            type = newType;
            toggle = newToggle;
        }
    }
}