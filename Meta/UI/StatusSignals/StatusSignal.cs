using Sirenix.OdinInspector;
using UnityEngine;

namespace Meta.UI.StatusSignals
{
    public abstract class StatusSignal : MonoBehaviour
    {
        [ValidateInput("EmptyString", "Please provide a name")]
        public string signalName;
        public RectTransform signalContainer;
        StatusSignalUI _signal;
        string _key;

        bool EmptyString(string checkName)
            => checkName != string.Empty;

        protected bool SignalActive;
        protected bool SignalPassive => !SignalActive;

        protected void EnableSignal()
        {
            SignalActive = true;

            _signal.Show();
            PlayerPrefs.SetInt(_key, 1);
        }

        protected void DisableSignal()
        {
            SignalActive = false;

            _signal.Hide();
            PlayerPrefs.SetInt(_key, 0);
        }

        public void Create(StatusSignalUI prefab, string key)
        {
            _signal = Instantiate(prefab, signalContainer);
            _signal.gameObject.name = "[StatusSignal] " + signalName;
            _signal.transform.localScale = Vector3.one;

            _key = key;
            CheckStatus();

            OnInit();
        }

        void CheckStatus()
        {
            var status = PlayerPrefs.GetInt(_key, 0) == 1;
            if (status)
                EnableSignal();
            else
                DisableSignal();
        }

        protected abstract void OnInit();
    }
}