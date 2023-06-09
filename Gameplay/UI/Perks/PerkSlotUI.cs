using Meta.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.Perks
{
    public class PerkSlotUI : MonoBehaviour
    {
        [SerializeField] Image icon;
        [SerializeField] bool hasStars;
        [SerializeField] [ShowIf(nameof(hasStars))] [Required] CanvasGroup starsCanvas ;
        [SerializeField] [ShowIf(nameof(hasStars))] [Required] StarsPanelUI stars ;
        public void Set(PerkData data)
        {
            icon.enabled = true;
            icon.sprite = data.so.Icon;
            if (hasStars)
            {
                stars.Set(data.Level);
                starsCanvas.alpha = 1;
                starsCanvas.interactable = true;
            }
        }

        public void Empty()
        {
            icon.enabled = false;
            if (hasStars)
            {
                starsCanvas.alpha = 0;
                starsCanvas.interactable = false;
            }
        }

 
    }
}
