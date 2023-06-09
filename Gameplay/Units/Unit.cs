using GameManager;
using Gameplay.Animations;
using Gameplay.Interface;
using Gameplay.Player;
using Gameplay.Units.UnitComponents;
using Gameplay.Units.UnitComponents.Move;
using NaughtyAttributes;
using Sirenix.OdinInspector;
using SO.EquipmentSO;
using SO.UnitsSO;
using UnityEngine;
using Utilities.Pools;


namespace Gameplay.Units
{
    public class Unit : PoolObject, ICanMove, IHasModel, IHasOrientation2D, IHasHitpoints, IHasTeam, IHasWeapons
    {
        [Tag]
        [SerializeField] protected string team;
        [SerializeField] [Layer] string bulletsLayer;
        [SerializeField] UnitModel model;

        [Space(20)]
        [SerializeField] [Required] Hitpoints hitpoints;
        [SerializeField] [Required] Weapons weapons;
        [SerializeField] [Required] Orientation2D orientation;
        [SerializeField] [Required] MoveEngine move;
        [SerializeField] [Required] Transform modelContainer;

        [Header("Options")]
        [SerializeField] bool hasAI;
        [Sirenix.OdinInspector.ShowIf(nameof(hasAI))]
        [SerializeField] [Required] AI.AI ai;

        #region Accessors

        public string Team => team;
        public Hitpoints Hitpoints => hitpoints;
        public Weapons Weapons => weapons;
        public Orientation2D Orientation => orientation;
        public MoveEngine Movement => move;
        public Transform ModelContainer => modelContainer;
        public UnitModel Model => model;

        public bool HasAI => hasAI;
        public AI.AI AI => ai;

        public UnitSO SO => _unit;

        // public bool HasBite => hasBite;

        #endregion

        void Awake()
        {
            orientation.OnLookLeft += LookLeft;
            orientation.OnLookRight += LookRight;
            hitpoints.OnDeath += Dead;
        }

        void Dead()
        {
            if (_unit)
            {
                GameplayEvents.Instance.UnitDeath(this);
                VfxPlayer.Instance.Play(_unit.DeathVFX, transform.position);
            }

            model.Death();
            gameObject.SetActive(false);
        }

        protected override void OnDisabled()
        {
            ReturnToPool();
        }

        public void Init(UnitSO unit)
        {
            _unit = unit;
            move.SetMoveSpeed(unit.MoveSpeed);
            hitpoints.SetMaxHp(unit.Hitpoints);
            hitpoints.Full();
            weapons.SetTeam(team);
            weapons.SetBulletsLayers(bulletsLayer);

            if (model) Destroy(model.gameObject);
            model = Instantiate(unit.Model, modelContainer);
            model.transform.localPosition = Vector3.zero;

            if (HasAI)
                ai.Init(this);

            if (unit.HasWeapons)
            {
                foreach (var w in unit.Weapons)
                    weapons.AddWeapon(w);
            }

            OnInit(unit);
        }

        protected virtual void OnInit(UnitSO unit)
        {
        }

        public void Reset()
        {
            hitpoints.Full();
            orientation.Reset();
        }

        public void Kill()
        {
            hitpoints.Damage(float.MaxValue);
        }

        // protected override void OnEnabled()
        // {
        //     // colActive = Random.value > 0.5f;
        // }

        bool colActive;
        //  [Space(20)]
        //    [InfoBox("Dont forget to handle it, was added during performance test", InfoMessageType.Warning)]
        // public BoxCollider2D collider2D;

        protected override void OnFixedUpdate()
        {
            if (move.IsMoving)
            {
                orientation.Check(move.Direction);

                weapons.Container.up = move.Direction;
                if (orientation.IsLeft)
                    weapons.Container.Rotate(Vector3.up, 180);

                weapons.SetAim(orientation.Angle);

                if (hitpoints.IsAlive)
                {
                    model.Move();
                }
            }
            else
            {
                if (hitpoints.IsAlive)
                {
                    model.Idle();
                }
            }

            // if (!collider2D) return;
            // colActive = !colActive;
            // collider2D.enabled = colActive;
        }


        public void Heal(float value)
        {
            if (!Hitpoints) return;
            Hitpoints.Heal(value);
        }

        public void TakeDamage(float value)
        {
            if (!Hitpoints) return;
            Hitpoints.Damage(value);
            
            if (model && Hitpoints.IsAlive) 
                model.Hit();
        }

        // public void SetBulletsLayers(string bulletLayer) => weapons.SetBulletsLayers(bulletLayer);
        public void SetTeam(string teamTag) => team = teamTag;
        public void SetMoveSpeed(float speed) => move.SetMoveSpeed(speed);

        #region TEST WEAPON

        [FoldoutGroup("TEST")]
        [Space(20)]
        [PropertyOrder(100)] [InlineEditor]
        public WeaponSO testWeapon;
        UnitSO _unit;

        [FoldoutGroup("TEST")]
        [PropertyOrder(101)]
        [DisableInEditorMode] [Sirenix.OdinInspector.Button(ButtonSizes.Gigantic)]
        void AddWeapon()
        {
            Weapons.AddWeapon(testWeapon);
        }

        [FoldoutGroup("TEST")]
        [PropertyOrder(100)]
        [DisableInEditorMode] [Sirenix.OdinInspector.Button(ButtonSizes.Gigantic)]
        void ChangeWeapon()
        {
            Weapons.ChangeWeapon(testWeapon);
        }

        #endregion

        #region TEST ORIENTATION

        [FoldoutGroup("TEST")]
        [Sirenix.OdinInspector.Button(ButtonSizes.Large)]
        public void LookLeft()
        {
            modelContainer.right = -Vector2.left;
        }


        [FoldoutGroup("TEST")]
        [Sirenix.OdinInspector.Button(ButtonSizes.Large)]
        public void LookRight()
        {
            modelContainer.right = -Vector2.right;
        }

        #endregion
    }
}