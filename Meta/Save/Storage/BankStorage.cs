using System;
using Meta.Data;
using Meta.Save.SaveSystem;
using SO.ConfigsSO;
using UnityEngine;

namespace Meta.Save.Storage
{
    public class BankStorage : Saveable<BankData>
    {
        public int Gold => SaveableData.gold;
        public int Gem => SaveableData.gem;
        public int Energy => SaveableData.energy;
        public event Action OnChange=delegate {  };
        public void Init(FirstLaunchSO config)
        {
            if (PlayerPrefs.HasKey(SaveKey))
            {
                Load();
            }
            else
            {
                SaveableData = config.Bank;
                Save();
            }
        }

        public void AddGold(int value) => ChangeGold(value);
        public void RemoveGold(int value) => ChangeGold(-value);
        void ChangeGold(int value) => Change(value, 0, 0);


        public void AddEnergy(int value) => ChangeEnergy(value);
        public void RemoveEnergy(int value) => ChangeEnergy(-value);
        void ChangeEnergy(int value) => Change(0, 0, value);


        public void AddGem(int value) => ChangeGem(value);
        public void RemoveGem(int value) => ChangeGem(-value);
        void ChangeGem(int value) => Change(0, value, 0);


        public void Add(int gold, int gem, int energy) => Change(gold, gem, energy);
        public void Remove(int gold, int gem, int energy) => Change(-gold, -gem, -energy);

        void Change(int coin, int gem, int energy)
        {
            SaveableData.gold += coin;
            SaveableData.energy += energy;
            SaveableData.gem += gem;
            OnChange();
            Save();
        }
    }
}