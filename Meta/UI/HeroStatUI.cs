using TMPro;
using UnityEngine;

namespace Meta.UI
{
    public class HeroStatUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI bonusTxt;

        public void SetBonus(float percent)
        {
            bonusTxt.text = "+" + percent + "%";
        }

        public void SetValue(float value)
        {
            bonusTxt.text = ((int) value).ToString();
        }
    }
}