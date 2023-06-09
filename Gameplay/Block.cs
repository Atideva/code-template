using UnityEngine;

namespace Gameplay
{
    public class Block : MonoBehaviour
    {
        [SerializeField] float colliderSize=1;
        // [SerializeField] BoxCollider2D collider2D;


        public float Size => colliderSize;
    }
}
