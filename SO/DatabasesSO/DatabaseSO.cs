using System.Collections.Generic;
using Meta.Interface;
using UnityEngine;

namespace SO.DatabasesSO
{
    public abstract class DatabaseSO : ScriptableObject
    {
        [Space(15)]
        public bool forceRefresh;
        public abstract IReadOnlyList<IDatabaseItem> Items { get; }
        public abstract void LoadItems();

        public abstract void MapItems();

        void OnValidate()
        {
            forceRefresh = false;
            LoadItems();
        }
    }
}