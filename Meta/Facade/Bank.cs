using System;
using GameManager;
using Meta.Enums;
using Meta.Save.Storage;
using UnityEngine;

namespace Meta.Facade
{
    public static class Bank
    {
        static BankStorage _bankStorage;

        static bool Null
        {
            get
            {
                if (_bankStorage) return false;
                Log.InitError(nameof(Bank));
                return true;
            }
        }


        public static int Gold => _bankStorage.Gold;
        public static int Gem => _bankStorage.Gem;
        public static int Energy => _bankStorage.Energy;

        public static void Init(BankStorage bank)
        {
            _bankStorage = bank;
            _bankStorage.OnChange += Changed;
        }

        static void Changed() => OnChanged();
        public static event Action OnChanged = delegate { };

        public static void Add(BankCurrencyEnum type, int amount, bool useAnimation = false,
            Vector3 animationFrom = default)
        {
            if (Null) return;
            switch (type)
            {
                case BankCurrencyEnum.Gold:
                    _bankStorage.AddGold(amount);
                    break;
                case BankCurrencyEnum.GEM:
                    _bankStorage.AddGem(amount);
                    break;
                case BankCurrencyEnum.Energy:
                    _bankStorage.AddEnergy(amount);
                    break;
                default:
                    Log.BankMiss();
                    return;
            }

            if (useAnimation)
                EventsUI.Instance.PlayBankVFX(type, amount, animationFrom);
        }

        public static void AddGold(int value)
        {
            if (Null) return;
            _bankStorage.AddGold(value);
        }

        public static void RemoveGold(int value)
        {
            if (Null) return;
            _bankStorage.RemoveGold(value);
        }

        public static void AddEnergy(int value)
        {
            if (Null) return;
            _bankStorage.AddEnergy(value);
        }

        public static void RemoveEnergy(int value)
        {
            if (Null) return;
            _bankStorage.RemoveEnergy(value);
        }

        public static void AddGem(int value)
        {
            if (Null) return;
            _bankStorage.AddGem(value);
        }

        public static void RemoveGem(int value)
        {
            if (Null) return;
            _bankStorage.RemoveGem(value);
        }
    }
}