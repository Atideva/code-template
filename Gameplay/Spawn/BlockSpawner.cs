using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Spawn
{
    public class BlockSpawner : MonoBehaviour
    {
        //   [SerializeField] Block prefab;
        [SerializeField] List<GameObject> containers;

        [Button]
        public void SpawnBox(Vector3 center, Block prefab, float width, float height)
        {
            var cont = new GameObject {name = "Blocks Container"};
            containers.Add(cont);

            var blockCont = cont.AddComponent<BlockContainer>();

            var startX = center.x - width / 2;
            var startY = center.y + height / 2;
            //var startPos = new Vector3(startX, startY );

            var x = startX;
            var y = startY;

            var horizontalAmount = (int) (width / prefab.Size);
            var verticalAmount = (int) (height / prefab.Size);

            for (var i = 0; i < horizontalAmount; i++)
            {
                var newBlock = Instantiate(prefab, cont.transform);
                newBlock.transform.position = new Vector2(x, y);
                blockCont.blocks.Add(newBlock);
                x += prefab.Size;
            }

            for (var i = 0; i < verticalAmount; i++)
            {
                var newBlock = Instantiate(prefab, cont.transform);
                newBlock.transform.position = new Vector2(x, y);
                blockCont.blocks.Add(newBlock);
                y -= prefab.Size;
            }

            for (var i = 0; i < horizontalAmount; i++)
            {
                var newBlock = Instantiate(prefab, cont.transform);
                newBlock.transform.position = new Vector2(x, y);
                blockCont.blocks.Add(newBlock);
                x -= prefab.Size;
            }

            for (var i = 0; i < verticalAmount; i++)
            {
                var newBlock = Instantiate(prefab, cont.transform);
                newBlock.transform.position = new Vector2(x, y);
                blockCont.blocks.Add(newBlock);
                y += prefab.Size;
            }
        }

        public void RemoveBox()
        {
            foreach (var go in containers)
            {
                Destroy(go);
            }
        }
    }
}