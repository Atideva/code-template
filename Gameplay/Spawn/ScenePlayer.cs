using System.Collections.Generic;
using GameManager;
using Gameplay.Units;
using Meta.Data;
using NaughtyAttributes;
using SO.PerksSO;
using SO.UnitsSO;
using UnityEngine;

namespace Gameplay.Spawn
{
    public class ScenePlayer : MonoBehaviour
    {
        [SerializeField] Hero hero;
        [SerializeField] [Tag] string playerTag;

        public IReadOnlyList<PerkData> ActivePerks => hero.Perks.Actives;
        public IReadOnlyList<PerkData> PassivePerks => hero.Perks.Passive;

        public Hero Hero => hero;
        public Vector3 Position => Hero.transform.position;

        public void Init(HeroSO so, int lvl)
        {
            hero.Hitpoints.OnDeath += ShowLose;
            var selected = Game.Instance.Storage.Heroes.Current;
            hero.Init(selected.so);
            var bonus = selected.so.GetBonus(selected.lvl);
            hero.SetBonuses(bonus);
            InitPerks(hero, so, lvl);
        }

        void ShowLose()
        {
        }

        void InitPerks(Hero unit, HeroSO so, int lvl)
        {
            if (!unit) return;
            var active = Game.Instance.Storage.Perks.ActivePerks;
            var passive = Game.Instance.Storage.Perks.PassivePerks;
            var starting = GetHeroPerks(so, lvl);
            hero.Perks.Set(active, passive, starting);
        }

        IReadOnlyList<ActivePerkSO> GetHeroPerks(HeroSO so, int lvl)
        {
            List<ActivePerkSO> perks = new();
            foreach (var data in so.PerksData)
            {
                if (lvl < data.lvlRequire) continue;
                if (data.perk is ActivePerkSO p)
                    perks.Add(p);
            }

            return perks;
        }
    }
}