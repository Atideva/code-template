using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities.MonoCache.System;

namespace Meta.UI
{
    public class PriceCurrencyUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI txt;
        [SerializeField] Image icon;

        public void Refresh(int value)
        {
            if (value <= 0) 
                Disable();
            else 
                Enable();
        
            txt.text = value.ToString();
        }

        public void Enable()
        {
            txt.gameObject.Enable();
            icon.gameObject.Enable();
        }

        public void Disable()
        {
            txt.gameObject.Disable();
            icon.gameObject.Disable();
        }
    }
}