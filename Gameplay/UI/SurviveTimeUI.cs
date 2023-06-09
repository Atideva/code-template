using TMPro;
using UnityEngine;

namespace Gameplay.UI
{
    public class SurviveTimeUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI txtMin;
        [SerializeField] TextMeshProUGUI txtSec;

        public void RefreshText(int minutes, int seconds)
        {
            var minPrefix = minutes < 10 ? "0" : "";
            var secPrefix = seconds < 10 ? "0" : "";
            txtMin.text = minPrefix + minutes;
            txtSec.text = secPrefix + seconds;
        }
    }
}