 
using UnityEngine;
using Utilities.MonoCache.System;

namespace Gameplay.BonusChests
{
    public abstract class BonusChest: MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D col)
        {
             Pickup();
        }

        public void Pickup()
        {
            OnPickup();
            gameObject.Disable();
        }

        protected abstract void OnPickup();
    }
    
    
}