using UnityEngine;

namespace SO.ConfigsSO
{
    [CreateAssetMenu(fileName = "Android settings", menuName = "Config/Settings Android")]
    public class AndroidSO : ScriptableObject
    {
        [SerializeField] bool neverSleepScreen = true;
        [SerializeField] bool backButtonQuit = true;
        [SerializeField] bool disableDebugger = true;

        public bool NeverSleepScreen => neverSleepScreen;
        public bool BackButtonQuit => backButtonQuit;
        public bool DisableDebugger => disableDebugger;
    }
}