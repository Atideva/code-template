using Meta.Data;
using Meta.Static;
using UnityEngine;

namespace Meta.UI
{
    public class EquipSlotUI : MonoBehaviour
    {
        public ItemUI itemUI;
        public CanvasGroup itemCanvas;

        public void Empty()
        {
            itemUI.Empty();
            itemCanvas.alpha = 0;
            itemCanvas.interactable = false;
            itemCanvas.blocksRaycasts = false;
        
        }

        public void PutItem(EquipmentData equip)
        {
            if (Equip.IsNull(equip))
            {
                Empty();
            }
            else
            {
                var data = Item.NewUIData(equip);
                itemUI.Refresh(data);

                itemCanvas.alpha = 1;
                itemCanvas.interactable = true;
                itemCanvas.blocksRaycasts = true;
            }
        }
    }
}