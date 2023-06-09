using Gameplay.Interface;
using UnityEngine;
using Utilities.CharacterStats;
using Utilities.MonoCache;

namespace Gameplay.Units.UnitComponents.Move
{
    public abstract class MoveEngine : MonoCache, IMoveEngine
    {
        [field: SerializeField] public bool IsMoving { get; protected set; }
        [field: SerializeField] public bool Block { get; set; }
        [SerializeField] float speed;
        [field: SerializeField] public float Speed => speed * Mult;
        [field: SerializeField] public Vector2 Direction { get; protected set; }

        [SerializeField] CharacterStat multiplier;
        public CharacterStat Multiplier => multiplier;
        float Mult => Multiplier.Value;

        protected virtual void Awake()
        {
            Multiplier.baseValue = 1;
        }

        public void SetMoveSpeed(float newSpeed) => speed = newSpeed;
        public void SetDirection(Vector3 dir) => Direction = dir;

        public void Stop()
        {
            IsMoving = false;
            Direction = Vector2.zero;
            OnStop();
        }

        protected virtual void OnStop()
        {
        }

        public abstract void Move();
    }
}