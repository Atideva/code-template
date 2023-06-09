using Meta.Facade;
using UnityEngine;

namespace Meta.UI.StatusSignals
{
    public class StatusSignalUI : MonoBehaviour
    {
        public CanvasGroup canvasGroup;

        public void Show()
        {
            canvasGroup.alpha = 1;
            Log.UIShow(gameObject.name, gameObject);
        }

        public void Hide()
        {
            canvasGroup.alpha = 0;
            Log.UIHide(gameObject.name, gameObject);
        }
    }
}