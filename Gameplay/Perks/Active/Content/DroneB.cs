using Gameplay.Animations;
using Gameplay.Units.UnitWeapons;
using Meta.Facade;
using NaughtyAttributes;
using UnityEngine;
using Utilities.Extensions;
using Utilities.MonoCache;

namespace Gameplay.Perks.Active.Content
{
    public class DroneB : MonoCache
    {
        [SerializeField] Bullet bulletPrefab;
        [SerializeField] [Tag] string bulletTeam;
        [SerializeField] [Layer] string bulletsLayer;
        [SerializeField] [Sirenix.OdinInspector.ReadOnly] float cooldown;
        [SerializeField] DroneFollow follow;
        DroneBPerk _perk;
        Bullet Spawn() => Scene.Instance.Bullets.Spawn(bulletPrefab, bulletTeam, _perk.Targets, bulletsLayer);
        float _burstTimer;
        int _burstCount;
        bool _oneMoreShoot;
        bool _isScaning;
        public float Cooldown => _perk.Stats.cooldown * _perk.Multipliers.Cooldown;
        public int Count => _perk.Stats.count + _perk.Multipliers.CountAdd;

        public void SetPerk(DroneBPerk perk)
        {
            _perk = perk;
        }

        public void SetTarget(Transform t)
        {
            follow.SetTarget(t);
        }

        public void SetPos(Vector2 pos)
        {
            transform.position = pos;
        }


        protected override void OnFixedUpdate()
        {
            if (_perk.LevelError) return;
            if (!_perk.Scanner) return;

            cooldown -= Time.fixedDeltaTime;
            if (cooldown > 0) return;

            if (!_oneMoreShoot)
            {
                _oneMoreShoot = true;
                _burstCount = Count;
            }
            else
            {
                if (_burstTimer > 0)
                {
                    _burstTimer -= Time.fixedDeltaTime;
                }
                else
                {
                    if (!_isScaning)
                    {
                        _perk.Scanner.Scan();
                        _perk.Scanner.OnScan -= Shoot;
                        _perk.Scanner.OnScan += Shoot;
                        _isScaning = true;
                    }
                }
            }
        }

        void Shoot()
        {
            _perk.Scanner.OnScan -= Shoot;
            _isScaning = false;

            if (_perk.Scanner.NoTargets) return;

            var target = _perk.Scanner.GetRandomTarget();
            var dir = (target.position - transform.position).normalized;
            var angle = VectorExtensions.GetAngle(dir);

            var bullet = Spawn();
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            bullet.SetDamage(_perk.Damage);
            bullet.SetMoveSpeed(_perk.BulletSpeed);
            bullet.SetLifeTime(25 / _perk.BulletSpeed);

            Audio.Play(_perk.Sound);

            _burstCount--;
            _burstTimer = _perk.BurstInterval;
            if (_burstCount <= 0)
            {
                _oneMoreShoot = false;
                cooldown = Cooldown;
            }
        }
    }
}