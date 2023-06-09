 
using Gameplay.UI.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.UI.Popups
{
    public class PopupUI : ViewUI
    {
        [SerializeField] Button closeButton;

        void OnEnable()
        {
            closeButton.onClick.AddListener(Hide);
        }

        void OnDisable()
        {
            closeButton.onClick.RemoveListener(Hide);
        }
 
    }
}