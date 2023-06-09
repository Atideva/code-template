 
using UnityEngine;

namespace Meta.UI.Anims
{
    public class ResetRectPos : MonoBehaviour
    {
  
        void Start()
        {
            var rect = (RectTransform) transform;
            rect.anchoredPosition=Vector2.zero;
       
        }

   
    }
}
