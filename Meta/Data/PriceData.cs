using System.Collections.Generic;
using System.Linq;
using Meta.Enums;

namespace Meta.Data
{
    [System.Serializable]
    public class PriceData
    {
        public List<BankCurrencyData> price = new();

        public int GetGoldPrice()
        {
            var find = price.FirstOrDefault(p => p.currency == BankCurrencyEnum.Gold);
            return find?.amount ?? 0;
        }
        
        public int GetGemPrice()
        {
            var find = price.FirstOrDefault(p => p.currency == BankCurrencyEnum.GEM);
            return find?.amount ?? 0;
        }
    }
}