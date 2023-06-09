using Gameplay.Units.HeroComponents;

namespace Gameplay.Perks.Active.Content
{
    public interface IKnowExpMagnet
    {
        HeroExperienceMagnet ExpMagnet { get; }
        void SetExperienceMagnet(HeroExperienceMagnet expMagnet);
    }
}