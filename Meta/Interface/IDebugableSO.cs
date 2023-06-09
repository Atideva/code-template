using SO.ConfigsSO;

namespace Meta.Interface
{
    public interface IDebugableSO
    {
        DebugSO DebugSO { get; set; }
        void DoDebug();
    }
}