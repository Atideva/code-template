 
using UnityEngine;
using UnityEngine.UI;

namespace Meta.UI.Buttons
{
    // ReSharper disable once IdentifierTypo
    public class PurchaseButtonUI : MonoBehaviour
    {
        [SerializeField] Button button;
        [SerializeField] PriceUI price;
        [SerializeField] Image back;
        [SerializeField] Sprite backActive;
        [SerializeField] Sprite backInactive;
        public Button Button => button;

        public void Active()
        {
            back.sprite = backActive;
            button.interactable = true;
        }

        public void Inactive()
        {
            back.sprite = backInactive;
            button.interactable = false;
        }

        public void RefreshPrice(int gold, int gem)
            => price.Refresh(gold,gem);
    }
}