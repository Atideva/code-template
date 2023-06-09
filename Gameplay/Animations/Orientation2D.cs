using System;
using UnityEngine;
using Utilities.MonoCache;
using static Utilities.Extensions.VectorExtensions;

namespace Gameplay.Animations
{
    public class Orientation2D : MonoCache
    {
        enum Orientation
        {
            Right,
            Left
        }

        public bool IsLeft => orientation == Orientation.Left;
        public bool IsRight => orientation == Orientation.Right;

        [SerializeField] Orientation orientation;
        [field: SerializeField] public float Angle { get; private set; }

        public event Action OnLookRight = delegate { };
        public event Action OnLookLeft = delegate { };

        public void Refresh(Vector2 moveDirection)
        {
            Angle = GetAngle(moveDirection);
            if (Angle is > 0 and < 90 or > 270 and < 360)
            {
                orientation = Orientation.Right;
                OnLookRight();
            }
            else
            {
                orientation = Orientation.Left;
                OnLookLeft();
            }  
        }

        public void Reset() => inited = false;
        bool inited;
        
        public void Check(Vector2 moveDirection)
        {
            Angle = GetAngle(moveDirection);
            if (Angle is > 0 and < 90 or > 270 and < 360)
            {
                if (orientation == Orientation.Right && inited) return;
                inited = true;
                orientation = Orientation.Right;
                OnLookRight();
            }
            else
            {
                if (orientation == Orientation.Left && inited) return;
                inited = true;
                orientation = Orientation.Left;
                OnLookLeft();
            }
        }
    }
}