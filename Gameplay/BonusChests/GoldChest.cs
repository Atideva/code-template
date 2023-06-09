using Meta.Data;
using Meta.Facade;
using UnityEngine;

namespace Gameplay.BonusChests
{
    public class GoldChest : BonusChest
    {
        [SerializeField] BankCurrencyData bonus;

        protected override void OnPickup()
        {
            Bank.Add(bonus.currency, bonus.amount);
        }
    }
}