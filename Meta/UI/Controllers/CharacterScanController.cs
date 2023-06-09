using GameManager;
using MenuHeroSelect;
using Meta.Data;
using Meta.Save.Storage;
using Meta.UI.Popups;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.UI.Controllers
{
    public class CharacterScanController : MonoBehaviour
    {
        [SerializeField] CharacterScanPopupUI popup;
        [SerializeField] Button openButton;
        HeroesStorage _storage;

        public void Init(HeroesController controller)
        {
            popup.Init(controller);
        }

        void Start()
        {
            _storage = Game.Instance.Storage.Heroes;
            openButton.onClick.AddListener(ShowSelected);
        }

        public void ShowSelected()
        {
            var selected = _storage.Current;
            var data = UIData(selected);
            popup.RefreshOwnedHero(data);
            popup.Show();
        }

        public void ShowBuyWindow(HeroCardData hero)
        {
            var data = UIData(hero);
            popup.RefreshHeroToBUY(data);
            popup.Show();
        }

        HeroCardUIData UIData(HeroCardData hero)
            => new()
            {
                SO = hero.so,
                Lvl = hero.lvl,
                LvlMax = 30,
                Owned = hero.owned
            };
    }
}