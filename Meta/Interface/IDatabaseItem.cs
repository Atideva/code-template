namespace Meta.Interface
{
    public interface IDatabaseItem
    {
        int DatabaseID { get; }
        void SET_DATABASE_ID(int newId);
    }
}