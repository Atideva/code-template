using System;
using GameManager;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.CharacterStats;

namespace Gameplay.Units.UnitComponents
{
    public class Hitpoints : MonoBehaviour
    {
        [SerializeField] bool isDead;
        [ProgressBar(0, nameof(Max), ColorGetter = nameof(GetHealthBarColor), Height = 20)]
        [LabelText("Current")]
        [SerializeField] float hp;
        [SerializeField] CharacterStat maxHp;
        [SerializeField] CharacterStat damageMultiplier;
        public CharacterStat MaxHitpoints => maxHp;
        public CharacterStat DamageMultiplier => damageMultiplier;
        float DmgMult => damageMultiplier.Value;
        public float Current => hp;
        public float Max => maxHp.Value;
        public bool Immune { get; private set; }
        public bool IsFull => hp >= Max;
        public bool IsAlive => hp > 0;
        public float Float => Current / Max;



        void Awake()
        {
            damageMultiplier.baseValue = 1;
        }

        public event Action OnDeath = delegate { };
        public event Action<float> OnHeal;
        public event Action<float> OnDamage;
        public event Action<float> OnImmuneDamage;


        public void SetMaxHp(float max)
        {
            maxHp.baseValue = max;
        }

        Color GetHealthBarColor(float value)
            => Color.Lerp(Color.red, Color.green, Mathf.Pow(value / 100f, 2));

        public void Full()
        {
            hp = maxHp.Value;
            isDead = false;
        }

        public void Heal(float value)
        {
            if (isDead) return;

            hp += value;
            OnHeal?.Invoke(value);

            if (hp > Max)
                hp = maxHp.Value;
        }


        public void SetImmune(bool immune)
        {
            Immune = immune;
        }


        public void Damage(float value)
        {
            if (isDead) return;
            if (Immune)
            {
                OnImmuneDamage?.Invoke(value);
            }
            else
            {
                hp -= value * DmgMult;
                OnDamage?.Invoke(value);
                GameplayEvents.Instance.DamageTaken(transform, value);
            }


            if (hp < 0)
                Dead();
        }

        void Dead()
        {
            hp = 0;
            isDead = true;
            OnDeath();
        }
    }
}