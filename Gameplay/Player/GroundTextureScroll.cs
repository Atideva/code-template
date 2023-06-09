using UnityEngine;

namespace Gameplay.Player
{
    public class GroundTextureScroll : MonoBehaviour
    {
        public Transform pivot;
        public MeshRenderer ground;
        Material _material;
        float _x, _y;
        Vector2 _scroll;
        public float scrollMultiplier;

        void Start()
        {
            _material = ground.material;
        }

        void Update()
        {
            var pos = pivot.transform.position;
            var x = pos.x;
            var y = pos.y;
            var move = new Vector2(x - _x, y - _y) * scrollMultiplier;
            _x = x;
            _y = y;

            _scroll += move;
            _material.mainTextureOffset = _scroll;
        }
    }
}