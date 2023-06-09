using GameManager;
using Meta.UI.Popups;
using UnityEngine;

namespace Meta.UI.Controllers
{
    public class LanguageSelectController : MonoBehaviour
    {
        [SerializeField] LanguagePopupUI popup;

        void Start()
        {
 
            EventsUI.Instance.OnShowLanguageList += Show;
        }
 
        void OnDisable()
        {
            EventsUI.Instance.OnShowLanguageList -= Show;
        }

        void Show()
        {
            popup.Show();
        }
    }
}