using Meta.Interface;

namespace Meta.Data
{
    [System.Serializable]
    public class BankData : DebugDataSO, ISave
    {
        //Если в BankData добавиться тип данных
        // В Bank в Create() надо добавить запись этого дефолтного значения
        public int gold;
        public int gem;
        public int energy;
    }
}