using UnityEngine;

namespace Gameplay.Animations
{
    public class TrailReset : MonoBehaviour
    {
        [SerializeField] TrailRenderer[] trails;

        void OnEnable()
        {
            foreach (var item in trails)
            {
                if (!item) continue;
                item.enabled = false;
                item.Clear();
            }

            //Invoke("Clear", 0.05f);
            Invoke(nameof(EnableTrails), 0.05f);
        }

        void Clear()
        {
            foreach (var item in trails)
            {
                if (item)
                    item.Clear();
            }
        }

        void EnableTrails()
        {
            foreach (var item in trails)
                if (item)
                    item.enabled = true;
        }
    }
}