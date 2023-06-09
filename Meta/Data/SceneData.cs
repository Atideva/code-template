using SO.ScenesSO;

namespace Meta.Data
{
    [System.Serializable]
    public class SceneData: GuidData
    {
        public SceneSO so;
      
        public bool isLock = true;
        public bool isComplete;

        public int bossKills;
        public int questsComplete;
        public int treasureFound;

        public bool bossCollected;
        public bool questsCollected;
        public bool treasureCollected;
        
        public int recordSeconds;
        public int totalSeconds;
        public int totalKills;
    }
}