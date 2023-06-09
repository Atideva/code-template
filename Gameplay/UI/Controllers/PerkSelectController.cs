using System.Collections.Generic;
using GameManager;
using Gameplay.Units;
using Meta.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.UI.Controllers
{
    public class PerkSelectController : MonoBehaviour
    {
        readonly List<Hero> _queue = new();
        [SerializeField] [ReadOnly] Hero hero;

        void Start()
        {
            GameplayEvents.Instance.OnHeroLevelUp += ShowPerks;
            GameplayEvents.Instance.OnPerkSelected += AddPerk;
        }

        void OnDisable()
        {
            GameplayEvents.Instance.OnHeroLevelUp -= ShowPerks;
            GameplayEvents.Instance.OnPerkSelected -= AddPerk;
        }


        void ShowPerks(Hero hero, int showTimes)
        {
            if (hero.Perks.Select.AllMaxed && 
                hero.Perks.Select.NoFreeSlots) return;

            if (showTimes > 1)
            {
                for (int i = 0; i < showTimes - 1; i++)
                    _queue.Add(hero);
            }

            var perks = hero.Perks.Select.GetRandomPerks();
            foreach (var data in perks)
            {
                if (!data.so)
                {
                    Debug.LogError("ay-ya: ");
                }
            }

            SetHero(hero);
            ShowPerks(perks);
        }

        public void SetHero(Hero h)
        {
            hero = h;
        }

        public void ShowPerks(List<PerkData> perks)
        {
            Game.Instance.Pause();
            GameplayUI.Instance.PerkSelect.Show();
            GameplayUI.Instance.PerkSelect.Refresh(perks);
        }

        void AddPerk(PerkData perk)
        {
            hero.Perks.Add(perk);

            if (_queue.Count == 0)
            {
                Game.Instance.Continue();
                return;
            }

            var next = _queue[0];
            var perks = next.Perks.Select.GetRandomPerks();
            SetHero(next);
            ShowPerks(perks);

            _queue.RemoveAt(0);
        }
    }
}