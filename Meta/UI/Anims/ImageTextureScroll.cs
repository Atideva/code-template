using UnityEngine;
using UnityEngine.UI;

namespace Meta.UI.Anims
{
    public class ImageTextureScroll : MonoBehaviour
    {
        [SerializeField] bool scroll;
        [SerializeField] RawImage image;
        [SerializeField] float x, y;
 
        void Update()
        {
            if (!scroll) return;
            var xSpeed = x / 100;
            var ySpeed = y / 100;
            var offset = new Vector2(xSpeed, ySpeed) * Time.deltaTime;
            var pos = image.uvRect.position;
            image.uvRect = new Rect(pos + offset, image.uvRect.size);
        }
    }
}