using System.Collections.Generic;
using Gameplay.Interface;
using Gameplay.Units.UnitWeapons;
using Meta.Facade;
using NaughtyAttributes;
using Sirenix.OdinInspector;
using SO.EquipmentSO;
using UnityEngine;

namespace Gameplay.Player
{
    public class Weapons : MonoBehaviour, IHasTeam
    {
        [SerializeField] [Required] Transform container;
        [SerializeField] [Tag] [Sirenix.OdinInspector.ReadOnly] string team;
        [SerializeField] [Tag] string targets;
          string _bulletsLayer;

        [Header("Current")]
        [SerializeField] List<Weapon> currentWeapons;

        public void SetAim(float angle)
        {
            foreach (var weapon in currentWeapons)
                weapon.SetAim(angle);
        }

        public string Team => team;

        public void SetTeam(string newTeamTag)
            => team = newTeamTag;
        public void SetEnemies(string newEnemies)
            => targets = newEnemies;
        public void SetBulletsLayers(string bulletLayer)
            => _bulletsLayer = bulletLayer;

        public Transform Container => container;

        public void ChangeWeapon(WeaponSO config, int id = 0)
        {
            if (currentWeapons.Count == 0)
            {
                AddWeapon(config);
                return;
            }

            if (id >= currentWeapons.Count)
            {
                Log.Error("Incorrect weapon change id ");
                return;
            }

            RemoveWeapon(id);
            AddWeaponAt(config, id);
        }

        public void AddWeapon(WeaponSO config)
        {
            var weapon = CreateWeapon(config);
            currentWeapons.Add(weapon);
        }

        public void RemoveWeapon(int id = 0)
        {
            if (id < currentWeapons.Count) return;

            var current = currentWeapons[id];
            Destroy(current.gameObject);
            currentWeapons.RemoveAt(id);
        }

        Weapon CreateWeapon(WeaponSO config)
        {
            var newWeapon = Instantiate(config.Prefab, container);
            newWeapon.SetConfig(config);
            newWeapon.SetTeam(team);
            newWeapon.SetTargets(targets);
            newWeapon.SetBulletsLayers(_bulletsLayer);
            
            return newWeapon;
        }

        void AddWeaponAt(WeaponSO config, int id = -1)
        {
            if (id >= currentWeapons.Count)
            {
                Log.Error("Incorrect weapon add id ");
                return;
            }

            var weapon = CreateWeapon(config);
            currentWeapons.Insert(id, weapon);
        }
    }
}