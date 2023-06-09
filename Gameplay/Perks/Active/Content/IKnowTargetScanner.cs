using Gameplay.Spawn;
using Gameplay.Units.HeroComponents;

namespace Gameplay.Perks.Active.Content
{
    public interface IKnowTargetScanner
    {
        TargetsScanner Scanner { get; }
        void SetScanner(TargetsScanner scanner);
    }
    
    public interface IKnowPlayer
    {
        ScenePlayer Player { get; }
        void SetPlayer(ScenePlayer player);
    }
    
}