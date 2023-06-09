using System.Collections;
using Gameplay.Perks.Active.Content;
using Gameplay.Spawn;
using Gameplay.Units.UnitWeapons;
using NaughtyAttributes;
using Sirenix.OdinInspector;
using SO.EquipmentSO;
using UnityEngine;
using Utilities.Extensions;

namespace Gameplay.AI.BossAbilities
{
    public class Shoot : BossAbility, IKnowPlayer
    {
        
        [SerializeField] [Tag] string team;
        [SerializeField] [Tag] string targets;
        [SerializeField] [Layer] string bulletsLayer;
        [SerializeField] float shootDelay;
        [SerializeField] float firePosOffset;
        [InlineEditor] [SerializeField] GunSO gun;


        public override void Use()
        {
            StartCoroutine(ShootBullet());
        }

        Bullet Spawn() => Scene.Instance.Bullets.Spawn(gun.BulletPrefab, team, targets, bulletsLayer);


        IEnumerator ShootBullet()
        {
            Started();
            yield return new WaitForSeconds(shootDelay);

            var times = gun.BurstMode ? gun.BurstCount : 1;
            var burstInterval = gun.BurstTime / times;

            for (int burst = 0; burst < times; burst++)
            {
                var bullets = gun.ShotgunMode ? gun.BulletsPerShot : 1;
                //  bool isSpread = Math.Abs(config.AngleSpread.x - config.AngleSpread.y) > 1f;
                var spread = Random.Range(gun.AngleSpread.x, gun.AngleSpread.y);

                var pos = (Vector2) transform.position;
                var targetPos = (Vector2) (Player.Hero.transform.position);
                var dir = targetPos - pos;
                dir.Normalize();
               
                var firePosa = pos + dir * firePosOffset;
                var angle = VectorExtensions.GetAngle(dir);
       
                if (bullets > 1) angle -= spread / 2;
                var step = bullets > 1 ? spread / (bullets - 1) : 0;

                for (var shotgun = 0; shotgun < bullets; shotgun++)
                {
                    var bullet = Spawn();
                    bullet.transform.position = firePosa;
                    bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);
                    bullet.SetDamage(gun.Damage);
                    bullet.SetMoveSpeed(gun.BulletSpeed);
                    bullet.SetLifeTime(25 / gun.BulletSpeed);
                    //   var angleStep = step;
                    // if (isSpread)
                    //     angleStep *= Random.Range(0.5f, 1.5f);
                    angle += step;
                }

                yield return new WaitForSeconds(burstInterval);
            }
            
            Finish();
        }

        public ScenePlayer Player { get; private set; }

        public void SetPlayer(ScenePlayer player) => Player = player;
    }
}