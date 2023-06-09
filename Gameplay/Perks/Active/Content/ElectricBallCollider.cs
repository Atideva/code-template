using System.Collections.Generic;
using Gameplay.Units;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Perks.Active.Content
{
    public class ElectricBallCollider : MonoBehaviour
    {
        [SerializeField] BoxCollider2D boxCollider2D;
        [SerializeField] [ReadOnly] bool blockBullets;
        [SerializeField] [ReadOnly] float interval;
        [SerializeField] [ReadOnly] float cooldown;
        [SerializeField] [ReadOnly] bool scan;
        [SerializeField] [ReadOnly] List<Unit> targetsInRange = new();
        [SerializeField] [ReadOnly] ElectricBallsPerk perk;
        public float Damage => perk.Stats.dps / interval;

        void FixedUpdate()
        {
            if (scan)
            {
                DamageTargets();
                StopScan();
            }

            cooldown -= Time.fixedDeltaTime;
            if (cooldown > 0) return;

            ScanForTargets();
        }

        public void SetBulletBlock(bool isBlockBullets)
        {
            blockBullets = isBlockBullets;
        }

        public void SetInterval(float dmgInterval)
        {
            interval = dmgInterval;
        }

        public void SetPerk(ElectricBallsPerk ballsPerk)
        {
            perk = ballsPerk;
        }

        public void Enable()
        {
            boxCollider2D.enabled = true;
        }

        public void Disable()
        {
            boxCollider2D.enabled = false;
        }


        void OnTriggerEnter2D(Collider2D enemy) => Check(enemy);
        void OnTriggerStay2D(Collider2D enemy) => Check(enemy);

        void Check(Collider2D enemy)
        {
            var unit = Scene.Instance.Units.Get(perk.Targets, enemy.transform);
            if (unit)
            {
                if (targetsInRange.Contains(unit)) return;
                targetsInRange.Add(unit);
               
                return;
            }

            var bullet = Scene.Instance.Bullets.Get(perk.Targets, enemy.transform);
            if (bullet)
            {
                bullet.Dispose();
            }
        }

        void ScanForTargets()
        {
            scan = true;
            targetsInRange.Clear();
            boxCollider2D.enabled = true;
        }

        void StopScan()
        {
            scan = false;
            boxCollider2D.enabled = false;
            cooldown = interval;
        }

        void DamageTargets()
        {
            foreach (var unit in targetsInRange)
            {
                unit.TakeDamage(Damage);
            }
        }
    }
}