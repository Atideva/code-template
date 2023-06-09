using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.UI
{
    public class UserLevelUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI experienceText;
        [SerializeField] TextMeshProUGUI levelText;
        [SerializeField] Slider slider;

        public void RefreshLevel(int lvl)
            => levelText.text = lvl.ToString();

        public void RefreshExperience(float current, float require)
        {
            slider.value = current / require;

            var cur = (int) current;
            var req = (int) require;
            experienceText.text = cur + "/" + req;
        }
    }
}