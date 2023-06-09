using System.Collections;
using Sirenix.OdinInspector;
using SO.EquipmentSO;
using UnityEngine;
 

namespace Gameplay.Units.UnitWeapons
{
    public class Gun : AutoAttackWeapon
    {
        [ReadOnly] [InlineEditor] [SerializeField] GunSO gun;
        [SerializeField] Transform firePos;
        Bullet Spawn() => Scene.Instance.Bullets.Spawn(gun.BulletPrefab, Team,Targets, BulletsLayer);

        public override void SetConfig(WeaponSO setConfig)
        {
            Config = setConfig;
            gun = (GunSO) setConfig;
        }

        protected override void Attack()
        {
            StartCoroutine(Shoot());
        }

        IEnumerator Shoot()
        {
            var times = gun.BurstMode ? gun.BurstCount : 1;
            var burstInterval = gun.BurstTime / times;


            for (int burst = 0; burst < times; burst++)
            {
                var bullets = gun.ShotgunMode ? gun.BulletsPerShot : 1;
                //  bool isSpread = Math.Abs(config.AngleSpread.x - config.AngleSpread.y) > 1f;
                var spread = Random.Range(gun.AngleSpread.x, gun.AngleSpread.y);

                var pos = (Vector2) firePos.position;
                var targetPos = (Vector2) (firePos.position + firePos.forward);
                var dir = targetPos - pos;

                var angle = AimAngle;
                dir.Normalize();

                if (bullets > 1) angle -= spread / 2;
                var step = bullets > 1 ? spread / (bullets - 1) : 0;

                for (var shotgun = 0; shotgun < bullets; shotgun++)
                {
                    var bullet = Spawn();
                    bullet.transform.position = firePos.transform.position;
                    bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);
                    bullet.SetDamage(Damage);
                    bullet.SetMoveSpeed(gun.BulletSpeed);
                    bullet.SetLifeTime(25 / gun.BulletSpeed);
                    //   var angleStep = step;
                    // if (isSpread)
                    //     angleStep *= Random.Range(0.5f, 1.5f);
                    angle += step;
                }

                yield return new WaitForSeconds(burstInterval);
            }
        }
    }
}