using UnityEngine;

namespace Meta.UI.Popups
{
    public class AchievementPopupUI : PopupUI
    {
        [SerializeField] Transform container;
        public Transform Container => container;
    }
}