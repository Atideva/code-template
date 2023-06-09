using UnityEngine;

namespace Gameplay.GridMap
{
    public class GridSystem : MonoBehaviour
    {
        public int x;
        public int y;
        public float cellSize;
        public Transform player;
        Grid<bool> grid;

        void Start()
        {
            var offset = new Vector3(-x / 2 * cellSize, -y / 2 * cellSize, 0);
            grid = new Grid<bool>(x, y, cellSize, offset, (_, _, _) => false);
        }

        void FixedUpdate()
        {
            grid.originPosition = player.position;
        }
    }
}