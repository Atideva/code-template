using Gameplay.Perks.Active.Content;
using Meta.Data;
using SO.PerksSO;
using UnityEngine;

namespace Gameplay.Units.HeroComponents
{
    public class HeroPerksContainer : MonoBehaviour
    {
        [SerializeField] Transform active;
        [SerializeField] Transform passive;
        Hero _hero;

        public void Init(Hero hero)
        {
            _hero = hero;
        }

        public void Create(PerkData data)
        {
            var container = data.so switch
            {
                ActivePerkSO => active,
                PassivePerkSO => passive,
                _ => null
            };

            Create(data, container);
        }

        void Create(PerkData data, Transform container)
        {
            if(!_hero)return;
            
            var perk = Instantiate(data.so.PerkPrefab, container);

            perk.SetMultipliers(_hero.Perks.Multipliers);
            perk.name = "[Perk] " + data.so.name;
            data.perk = perk;

            if (perk is IKnowTargetScanner scan)
                scan.SetScanner(_hero.Scanner);

            if (perk is IKnowMovement mov)
                mov.SetMovement(_hero.Movement);

            if (perk is IKnowHitpoints hp)
                hp.SetHitpoints(_hero.Hitpoints);

            if (perk is IKnowExpMagnet expMgn)
                expMgn.SetExperienceMagnet(_hero.ExperienceMagnet);
        }
        
    }
}