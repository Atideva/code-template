using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.UI.Buttons
{
    public class ButtonUI : MonoBehaviour
    {
        [SerializeField] [Required] Button button;
        [SerializeField] [Required] Image image;
        [SerializeField] TextMeshProUGUI txt;
        [SerializeField] Material enableMaterial;
        [SerializeField] Material disableMaterial;
        [SerializeField] CanvasGroup canvas;

        public void Enable()
        {
            button.interactable = true;
            image.material = enableMaterial;
            if (txt) txt.DOFade(1f, 0f);
        }

        public void Hide()
        {
            if (!canvas) return;

            canvas.alpha = 0;
            canvas.interactable = false;
            canvas.blocksRaycasts = false;
        }

        public void Show()
        {
            if (!canvas) return;

            canvas.alpha = 1;
            canvas.interactable = true;
            canvas.blocksRaycasts = true;
        }

        public void Disable()
        {
            button.interactable = false;
            image.material = disableMaterial;
            if (txt) txt.DOFade(0.7f, 0f);
        }
    }
}