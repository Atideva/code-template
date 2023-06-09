using System;
using GameManager;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.UI
{
    public class ShowLanguageSelectUI : MonoBehaviour
    {
        [SerializeField] Button button;
        void Start()
        {
            button.onClick.AddListener(Show);  
        }

        void OnDisable()
        {
            button.onClick.RemoveListener(Show);  
        }

        void Show()
        {
            EventsUI.Instance.ShowLanguageList();
        }

    }
}
