using System;
using Gameplay.Interface;
using Meta.Facade;
using NaughtyAttributes;
using SO.EquipmentSO;
using UnityEngine;
using Utilities.MonoCache;

namespace Gameplay.Units.UnitWeapons
{
    public abstract class Weapon : MonoCache, IHasTeam
    {
        [SerializeField] [Tag] [Sirenix.OdinInspector.ReadOnly] string team;
        [SerializeField] [Tag] string targets;
        protected WeaponSO Config;
        [field: SerializeField] public float AimAngle { get; private set; }
        protected string BulletsLayer;
        public void SetAim(float angle) => AimAngle = angle;

        public string Team => team;

        public void SetTeam(string newTeamTag)
            => team = newTeamTag;

        public void SetBulletsLayers(string bulletLayer) 
            => BulletsLayer = bulletLayer;

        protected float AttackSpeed => Config.AttackSpeed;
        protected float Damage => Config.Damage;
        [Sirenix.OdinInspector.ReadOnly] [SerializeField]
        float cooldown;
        protected event Action OnAttackReady = delegate { };
        public bool IsReady => cooldown <= 0;
        public bool IsCooldown => cooldown > 0;

        public string Targets => targets;

        public void SetTargets(string tagString) => targets = tagString;
        public abstract void SetConfig(WeaponSO setConfig);


        protected void Cooldown()
        {
            Log.WeaponShoot(gameObject.name, gameObject);
            cooldown = 1 / Config.AttackSpeed;
        }

        protected override void OnFixedUpdate()
        {
            if (Config.AttackSpeed <= 0) return;
            cooldown -= Time.fixedDeltaTime;
            if (cooldown <= 0)
            {
                OnAttackReady();
            }
        }
    }
}