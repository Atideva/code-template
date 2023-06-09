using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.UI
{
    public class BankResourceUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI valueText;
        [SerializeField] Button clickButton;
        public event Action OnClick = delegate { };

        void Start()
        {
            clickButton.onClick.AddListener(Click);
        }

        void OnDisable()
        {
            clickButton.onClick.RemoveListener(Click);
        }

        void Click() => OnClick();

        public void RefreshText(int value)
        {
            valueText.text = value.ToString();
        }
    }
}