using Gameplay;
using Gameplay.Units.UnitWeapons;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SO.EquipmentSO
{
    [CreateAssetMenu(fileName = "New Gun", menuName = "Equipment/Gun")]
    public class GunSO : WeaponSO
    {
        public override Weapon Prefab => gunPrefab;



        public Bullet BulletPrefab => bulletPrefab;

        public float BulletSpeed => bulletSpeed;

        public bool BurstMode => burstMode;

        public int BurstCount => burstCount;

        public float BurstTime => burstTime;

        public bool ShotgunMode => shotgunMode;

        public int BulletsPerShot => bulletsPerShot;

        public Vector2 AngleSpread => angleSpread;

        [Space(20)]
        //      [HorizontalLine]
        [LabelText("[Gun]")] [PropertyOrder(1)]
        [SerializeField] [Required] Gun gunPrefab;
      
        [LabelText("[Bullet]")] [PropertyOrder(1)]
        [SerializeField] [Required] Bullet bulletPrefab;

        [PropertyOrder(5)]
        [SerializeField] float bulletSpeed = 1f;

        [PropertyOrder(6)] [Space(10)]
        [SerializeField] bool burstMode;
      
        [PropertyOrder(6)]
        [ShowIf(nameof(burstMode), "true")] [Min(2)]
        [SerializeField] int burstCount = 2;
      
        [PropertyOrder(6)]
        [ShowIf(nameof(burstMode), "true")] [Min(0.05f)]
        [SerializeField] float burstTime = 0.1f;

        [PropertyOrder(7)]
        [Space(10)]
        [SerializeField] bool shotgunMode;
        [PropertyOrder(7)]
        [ShowIf(nameof(shotgunMode), "true")] [Min(2)]
        [SerializeField] int bulletsPerShot = 2;
        [PropertyOrder(7)]
        [ShowIf(nameof(shotgunMode), "true")]
        [MinMaxSlider(0f, 360f, true)]
        [SerializeField] Vector2 angleSpread;
    }
}