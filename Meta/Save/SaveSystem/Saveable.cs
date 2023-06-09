using Meta.Data;
using Meta.Interface;
using SO.ConfigsSO;
using UnityEngine;

namespace Meta.Save.SaveSystem
{
    public abstract class Saveable<T> : MonoBehaviour, IDebugableSO where T : ISave, new()
    {
        [field: SerializeField, Header("DEBUG")]
        protected string SaveKey { get; private set; }

        // ReSharper disable once InconsistentNaming
        [SerializeField] protected T SaveableData;

        public void SetSaveKey(string k) => SaveKey = k;

        protected void Save()
        {
            Serializer.Pack(SaveableData);
            DataSaver.Save(SaveableData, SaveKey);
            DoDebug();
        }

        protected void Load()
        {
            SaveableData = DataSaver.Load<T>(SaveKey);
            Serializer.Unpack(SaveableData);
            DoDebug();
        }
 

        //DEBUGABLE INTERFACE
        public DebugSO DebugSO { get; set; }
        public void DoDebug()
        {
            if (!DebugSO) return;
            if (SaveableData is DebugDataSO debugable)
                DebugSO.Refresh(debugable);
        }
        
    }
}