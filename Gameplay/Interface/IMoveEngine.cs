using UnityEngine;

namespace Gameplay.Interface
{
    public interface IMoveEngine
    {
 
        bool IsMoving{ get; }
        float Speed { get; }
        Vector2 Direction{ get; }
        void SetDirection(Vector3 dir);
        void SetMoveSpeed(float speed);
        void Stop( );
        void Move( );


    }
}