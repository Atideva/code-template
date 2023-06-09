using Utilities.Pools;

namespace Gameplay.Animations
{
    public class Vfx : PoolObject
    {
        protected override void OnDisabled()
        {
            ReturnToPool();
        }
    }
}