using Meta.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SO.ConfigsSO
{
    [CreateAssetMenu(fileName = "Log", menuName = "Config/Log")]
    public class LogSO : ScriptableObject
    {
        [SerializeField] bool ingameDebugConsole;
     [HideLabel]   [SerializeField] LogData logSettings;


        public bool IngameDebug => ingameDebugConsole;

        public LogData LogSettings => logSettings;
    }
}