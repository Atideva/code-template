using UnityEngine;

namespace Gameplay.Pickables
{
    public abstract class Pickable : MonoBehaviour
    {
        bool picked;
        void OnTriggerEnter2D(Collider2D col)
        {
            if (picked) return;
            picked = true;
            OnPickup();
        }

        protected abstract void OnPickup();
    }
}
