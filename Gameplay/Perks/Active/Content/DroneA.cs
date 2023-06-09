using Gameplay.Animations;
using Gameplay.Units.UnitWeapons;
using Meta.Facade;
using NaughtyAttributes;
using UnityEngine;
using Utilities.Extensions;
using Utilities.MonoCache;


namespace Gameplay.Perks.Active.Content
{
    public class DroneA : MonoCache
    {
        /*
        [SerializeField] DronaALaser prefab;
        [SerializeField] DronaALaserPool pool;
        [SerializeField] float laserLifetime = 0.2f;
        [SerializeField] [ReadOnly] float cooldown;
        [SerializeField] [ReadOnly] DroneAPerk perk;

        void Awake()
        {
            pool.SetPrefab(prefab, transform);
        }

        public void SetPos(Vector2 pos)
        {
            transform.position = pos;
        }

        public void SetPerk(DroneAPerk p)
        {
            perk = p;
        }

        protected override void OnFixedUpdate()
        {
            if (perk.LevelError) return;
            if (!perk.Scanner) return;

            cooldown -= Time.fixedDeltaTime;
            if (cooldown > 0) return;

            perk.Scanner.Scan();
            perk.Scanner.OnScan -= Shoot;
            perk.Scanner.OnScan += Shoot;
        }

        float Cooldown => perk.Stats.cooldown * perk.Multipliers.Cooldown;

        void Shoot()
        {
            perk.Scanner.OnScan -= Shoot;
            if (perk.Scanner.NoTargets) return;

            cooldown = Cooldown;

            for (int i = 0; i < perk.Stats.attackSpeed; i++)
            {
                var target = perk.Scanner.GetRandomTarget();

                var laser = pool.Get();
                laser.transform.position = target.position;
                laser.Activate(transform, target, laserLifetime * perk.Multipliers.Duration);

                var unit = Scene.Instance.Units.Get(perk.Targets, target);
                if (unit) unit.TakeDamage(perk.Damage * perk.Multipliers.Duration);
            }
        }
        */

        [SerializeField] Bullet bulletPrefab;
        [SerializeField] Transform model;
        [SerializeField] [Tag] string bulletTeam;
        [SerializeField] [Layer] string bulletsLayer;
        [SerializeField] [Sirenix.OdinInspector.ReadOnly] float cooldown;
        [SerializeField] DroneFollow follow;

        DroneAPerk _perk;
        Bullet Spawn() => Scene.Instance.Bullets.Spawn(bulletPrefab, bulletTeam, _perk.Targets, bulletsLayer);

        public void SetPerk(DroneAPerk perk)
        {
            _perk = perk;
        }

        public void SetPos(Vector2 pos)
        {
            transform.position = pos;
        }

        public void SetTarget(Transform t)
        {
            follow.SetTarget(t);
        }

        int Count => _perk.Stats.count + _perk.Multipliers.CountAdd;
        float Cooldown =>   _perk.Stats.cooldown * _perk.Multipliers.Cooldown;

        [SerializeField, ReadOnly] float _burstTimer;
       [SerializeField, ReadOnly] int _burstCount;
        bool _oneMoreShoot;
        bool _isScaning;

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
            bullet.transform.position = model.position;
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