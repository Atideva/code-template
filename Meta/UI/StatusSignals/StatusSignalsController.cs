using UnityEngine;

namespace Meta.UI.StatusSignals
{
    public class StatusSignalsController : MonoBehaviour
    {
        public StatusSignalUI prefab;

        void Start()
        {
            var signals = GetComponentsInChildren<StatusSignal>();
            foreach (var signal in signals)
            {
                var key = signal.signalName;
                signal.Create(prefab, "save" + key);
            }
        }
    }
}