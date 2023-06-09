using System.Collections.Generic;
using Gameplay.Perks.Active.Content;
using Gameplay.Units.UnitComponents;
using Meta.Facade;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.AudioSystem;

namespace Gameplay.Perks.Batman
{
    public class SmokeShieldPerk : ActivePerk, IKnowHitpoints
    {
        [FoldoutGroup("Prefab Setup")] [SerializeField] GameObject container;
        [FoldoutGroup("Prefab Setup")] [SerializeField] float duration;
        [FoldoutGroup("Prefab Setup")] [SerializeField]  bool isShield;
        [FoldoutGroup("Prefab Setup")] [SerializeField] SoundSO sound;
        
        [Space(20)]
        [ListDrawerSettings(Expanded = true, HideRemoveButton = true, HideAddButton = true, DraggableItems = false)]
        [ValidateInput(nameof(EqualMaxLevel), "COUNT != " + nameof(MaxLevel))]
        [SerializeField] List<SmokeShieldStats> stats = new() {new(), new(), new(), new(), new()};

        public SmokeShieldStats Stats => Level > 0 && Level <= stats.Count ? stats[Level - 1] : null;
        bool LevelError => Level <= 0 && Level > stats.Count;
        bool EqualMaxLevel() => stats.Count == MaxLevel;
  


        void FixedUpdate()
        {
            if (LevelError) return;
            if (!Hitpoints) return;

            
            if (isShield)
            {
 
                duration -= Time.fixedDeltaTime;
                if (duration <= 0)
                    DisableShield();
                else
                    Hitpoints.SetImmune(true);
            }
           
                cooldown -= Time.fixedDeltaTime;
                if (cooldown < 0)
                {
                    cooldown = Stats.cooldown;
                    CreateShield();
                }
    
        

        }


        void CreateShield()
        {
            isShield = true;
            Hitpoints.SetImmune(true);
            container.SetActive(true);

            duration = Stats.duration;
            Audio.Play(sound);
        }

        void DisableShield()
        {
            isShield = false;
            Hitpoints.SetImmune(false);
            container.SetActive(false);
        }


        public Hitpoints Hitpoints { get; private set; }
        public void SetHitpoints(Hitpoints hitpoints) => Hitpoints = hitpoints;
    }
}