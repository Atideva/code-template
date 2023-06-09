using UnityEngine;

namespace Meta.UI
{
    public class PriceUI : MonoBehaviour
    {
        [SerializeField] PriceCurrencyUI goldPrice;
        [SerializeField] PriceCurrencyUI gemPrice;
    
        public void Refresh(int gold, int gem)
        {
            goldPrice.Refresh(gold);
            gemPrice.Refresh(gem);
        }
    }
}
