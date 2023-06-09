using Gameplay.Units.UnitComponents;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Units.HeroComponents
{
    // ReSharper disable once InconsistentNaming
    public class HP_Bar : MonoBehaviour
    {
        [SerializeField] Slider slider;
        [SerializeField] Image barImage;
        [SerializeField] Gradient color;
        Hitpoints _hp;

        public void Init(Hitpoints hp)
        {
            _hp = hp;
            _hp.OnDamage += Damage;
            _hp.OnHeal += Heal;

            Refresh();
        }

        void Damage(float dmg) => Refresh();
        void Heal(float heal) => Refresh();


        void Refresh()
        {
            var value = _hp.Float;
            slider.value = value;
            barImage.color = color.Evaluate(value);
        }
    }
}