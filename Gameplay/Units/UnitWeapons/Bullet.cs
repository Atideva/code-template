using Gameplay.Interface;
using Gameplay.Units.UnitComponents;
using Gameplay.Units.UnitComponents.Move;
using NaughtyAttributes;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.Pools;
 
namespace Gameplay.Units.UnitWeapons
{
    public class Bullet : PoolObject, ICanMove, ICanDamage, IHasLifetime, IHasTeam
    {
        [InfoBox("TODO: remove extra shit in script, added during test optimization")]
        [SerializeField] [Tag] protected string team;
        [SerializeField] [Tag] string targets;
        [SerializeField] float damage;
        [SerializeField] MoveEngine movement;
        [SerializeField] [Required] LifeTime lifeTime;
        [SerializeField] CircleCollider2D triggerCollider;
        [SerializeField] Rigidbody2D rb;
        [SerializeField] SpriteRenderer sprite;
        float _timer;
        bool _letsDoDamage;
        
        void Awake()
        {
            lifeTime.OnExpire += Dispose;
        }

        protected override void OnEnabled()
        {
            _timer = 0.1f;
            triggerCollider.enabled = false;
            _letsDoDamage = false;
        }

        bool colActive;

        protected override void OnFixedUpdate()
        {
            _timer -= Time.fixedDeltaTime;
            if (_timer > 0) return;
            if (!_letsDoDamage)
            {
                _letsDoDamage = true;
                colActive = Random.value > 0.5f;
            }
            else
            {
                colActive = !colActive;
                triggerCollider.enabled = colActive;
                if (rb) rb.simulated = colActive;
            }
        }

        public void Dispose()
        {
            gameObject.SetActive(false);
            ReturnToPool();
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.CompareTag(targets))
            {
                return;
            }
            if (!_letsDoDamage)
            {
                _letsDoDamage = true;
            }

            var unit =
                Scene.Instance.Units.Get(targets, col.transform);
            if (unit)
            {
                DealDamage(unit);
                gameObject.SetActive(false);
            }
        }


        public float Damage => damage;

        public void SetDamage(float dmg)
            => damage = dmg;

        public void DealDamage(IHasHitpoints hp)
            => hp.TakeDamage(damage);


        public MoveEngine Movement => movement;

        public void SetMoveSpeed(float speed)
            => movement.SetMoveSpeed(speed);


        public LifeTime LifeTime => lifeTime;

        public void SetLifeTime(float duration)
            => lifeTime.Set(duration);


        public string Team => team;

        public void SetColor(Color clr)
        {
            if(!sprite) return;
            sprite.color = clr;
        }

        public void SetLayer(string layer)
        {
            gameObject.layer = LayerMask.NameToLayer(layer);
        }
        public void SetTeam(string newTeamTag)
            => team = newTeamTag;
        
        public void SetEnemies(string newTargets)
            => targets = newTargets;
    }
}