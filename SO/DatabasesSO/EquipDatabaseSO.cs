using System.Collections.Generic;
using Meta.Interface;
using SO.EquipmentSO;
using UnityEngine;

namespace SO.DatabasesSO
{
    public class EquipDatabaseSO : DatabaseSO
    {
        [Space(15)]
        [SerializeField] List<WeaponSO> weapons = new();
        [SerializeField] List<NecklaceSO> necklaces = new();
        [SerializeField] List<GlovesSO> gloves = new();
        [SerializeField] List<HelmSO> helms = new();
        [SerializeField] List<VestSO> chests = new();
        [SerializeField] List<BootsSO> boots = new();
        List<IDatabaseItem> _items = new();
        public override IReadOnlyList<IDatabaseItem> Items => _items;

        public override void LoadItems()
        {
           //  weapons = DatabaseSearcher.Find<WeaponSO>();
            // necklaces = DatabaseSearcher.Find<NecklaceSO>();
            // gloves = DatabaseSearcher.Find<GlovesSO>();
            // helms = DatabaseSearcher.Find<HelmSO>();
            // chests = DatabaseSearcher.Find<VestSO>();
            // boots = DatabaseSearcher.Find<BootsSO>();
            CreateList();
        }

        public override void MapItems()
        {
            // DatabaseMap.Add(nameof(WeaponSO), weapons);
            // DatabaseMap.Add(nameof(NecklaceSO), necklaces);
            // DatabaseMap.Add(nameof(GlovesSO), gloves);
            // DatabaseMap.Add(nameof(HelmSO), helms);
            // DatabaseMap.Add(nameof(VestSO), chests);
            // DatabaseMap.Add(nameof(BootsSO), boots);
        }


        void CreateList()
        {
            // _items = new List<IDatabaseItem>();
            // foreach (var so in weapons) _items.Add(so);
            // foreach (var so in necklaces) _items.Add(so);
            // foreach (var so in gloves) _items.Add(so);
            // foreach (var so in helms) _items.Add(so);
            // foreach (var so in chests) _items.Add(so);
            // foreach (var so in boots) _items.Add(so);
        }
    }
}