using System;
using System.Collections.Generic;
using GameManager;
using MenuHeroSelect;
using Meta.Data;
using Meta.Facade;
using Meta.Save.Storage;
using Meta.UI.Buttons;
using Meta.UI.Controllers;
using Sirenix.OdinInspector;
using SO.UnitsSO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities.MonoCache.System;
using static Utilities.Extensions.UIExtensions;


namespace Meta.UI.Popups
{
    public class CharacterScanPopupUI : PopupUI
    {
        [SerializeField] PerkInfoUI prefab;
        [SerializeField] Transform container;
        [Header("Hero")]
        [SerializeField] HeroCardUI heroUI;
        [SerializeField] HeroStatUI dmgStat;
        [SerializeField] HeroStatUI hpStat;
        [SerializeField] TextMeshProUGUI lvlText;
        [Header("Buttons")]
        [SerializeField] PurchaseButtonUI levelUpButton;
        [SerializeField] PurchaseButtonUI buyButton;
        [SerializeField] CanvasGroup lvlButtonGroup;
        [SerializeField] CanvasGroup buyButtonGroup;
        [Header("Background")]
        [SerializeField] Image back;
        [SerializeField] Color backOwnedClr;
        [SerializeField] Color backNotOwnedClr;
        [Space(20)]
        [SerializeField] [ReadOnly] HeroSO hero;
        [SerializeField] [ReadOnly] List<PerkInfoUI> perksInfo = new();
        HeroesStorage _storage;
        HeroesController _controller;
        HeroSO _tryBuyHero;

        public void Init(HeroesController controller)
        {
            _controller = controller;
        }

        void Start()
        {
            _storage = Game.Instance.Storage.Heroes;
            levelUpButton.Button.onClick.AddListener(LevelUp);
            buyButton.Button.onClick.AddListener(BuyHero);
        }

        void LevelUp()
        {
            var priceData = Game.Instance.Config.Settings.GetHeroLevelUpPrice(heroUI.Data.Lvl);
            var goldPrice = priceData.GetGoldPrice();
            var gemPrice = priceData.GetGemPrice();
            
            var gold = Bank.Gold;
            var gem = Bank.Gem;
            var isEnough = gold >= goldPrice && gem>= gemPrice;
            
            if (!isEnough) return;

            Bank.RemoveGold(goldPrice);
            Bank.RemoveGem(gemPrice);
            
            var so = heroUI.Data.SO;
            _storage.LevelUp(so);

            var data = UIData(_storage.Current);
            RefreshOwnedHero(data);

            EventsUI.Instance.RefreshHeroUI();
        }


        void BuyHero()
        {
            var priceData = Game.Instance.Config.Settings.GetHeroLevelUpPrice(heroUI.Data.Lvl);
            var goldPrice = priceData.GetGoldPrice();
            var gemPrice = priceData.GetGemPrice();
            
            var gold = Bank.Gold;
            var gem = Bank.Gem;
            var isEnough = gold >= goldPrice && gem>= gemPrice;
            
            if (!isEnough) return;

            Bank.RemoveGold(goldPrice);
            Bank.RemoveGem(gemPrice);

            _controller.Select(_tryBuyHero);
            _storage.SetOwned(_tryBuyHero);

            var data = UIData(_storage.Current);
            RefreshOwnedHero(data);
        }


        public void RefreshOwnedHero(HeroCardUIData data)
        {
           
            if (!data.SO)
            {
                Log.Error("Refresh OWNed hero,Trying to add  item with SO = NULL to inventory");
            }
            
            heroUI.Refresh(data);
            hero = data.SO;
            var txt = lvlText.text;
            lvlText.text = txt + " " + data.Lvl;

            var total = hero.PerksData.Count;
            CreatePerksUI(total);
            RefreshPerksUI(hero.PerksData, total, data.Lvl);

            var bonus = data.SO.GetBonus(data.Lvl);
            var dmg = bonus.attackBonus;
            var hp = bonus.hpBonus;
            dmgStat.SetBonus(dmg);
            hpStat.SetBonus(hp);


            var priceData = Game.Instance.Config.Settings.GetHeroLevelUpPrice(data.Lvl);
            var goldPrice = priceData.GetGoldPrice();
            var gemPrice = priceData.GetGemPrice();
            var gold = Bank.Gold;
            var gem = Bank.Gem;
            var isEnough = gold >= goldPrice && gem >= gemPrice;

            levelUpButton.RefreshPrice(goldPrice, gemPrice);

            if (isEnough)
                levelUpButton.Active();
            else
                levelUpButton.Inactive();

            back.color = backOwnedClr;
            EnableGroup(lvlButtonGroup);
            DisableGroup(buyButtonGroup);
        }


        public void RefreshHeroToBUY(HeroCardUIData data)
        {
            if (!data.SO)
            {
                Log.Error("Refresh UN-OWNed hero, Trying to add  item with SO = NULL to inventory");
            }
            
            _tryBuyHero = data.SO;
            heroUI.Refresh(data);
            hero = data.SO;
            lvlText.text = "Level " + data.Lvl;

            var total = hero.PerksData.Count;
            CreatePerksUI(total);
            RefreshPerksUI(hero.PerksData, total, data.Lvl);

            var bonus = data.SO.GetBonus(data.Lvl);
            var dmg = bonus.attackBonus;
            var hp = bonus.hpBonus;
            dmgStat.SetBonus(dmg);
            hpStat.SetBonus(hp);


            var priceData = hero.BuyPrice;
            var goldPrice = priceData.GetGoldPrice();
            var gemPrice = priceData.GetGemPrice();
            var gold = Bank.Gold;
            var gem = Bank.Gem;
            var isEnough = gold >= goldPrice && gem >= gemPrice;

            buyButton.RefreshPrice(goldPrice, gemPrice);
            
            if (isEnough)
                buyButton.Active();
            else
                buyButton.Inactive();

            back.color = backNotOwnedClr;
            DisableGroup(lvlButtonGroup);
            EnableGroup(buyButtonGroup);
        }
        // ReSharper disable once InconsistentNaming


        void RefreshPerksUI(IReadOnlyList<HeroPerkData> perks, int total, int lvl)
        {
            for (var i = 0; i < total; i++)
            {
                var heroPerk = perks[i];
                var require = heroPerk.lvlRequire;
                perksInfo[i].Enable();
                perksInfo[i].Refresh(heroPerk.perk, require, lvl >= require);
            }
        }

        void CreatePerksUI(int total)
        {
            var exist = perksInfo.Count;

            if (exist < total)
            {
                for (var i = exist; i < total; i++)
                {
                    var info = Instantiate(prefab, container);
                    perksInfo.Add(info);
                }
            }

            if (exist > total)
            {
                for (var i = exist; i < total; i--)
                    perksInfo[i].gameObject.Disable();
            }

            for (int i = total; i < exist; i++)
            {
                perksInfo[i].gameObject.Disable();
            }
        }


        HeroCardUIData UIData(HeroCardData h)
            => new()
            {
                SO = h.so,
                Lvl = h.lvl,
                LvlMax = 30,
                Owned = h.owned
            };
    }
}