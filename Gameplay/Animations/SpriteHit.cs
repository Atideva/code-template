using System;
using System.Collections;
using UnityEngine;

namespace Gameplay.Animations
{
    public class SpriteHit : MonoBehaviour
    {
        [SerializeField] bool enable = true;
        [SerializeField] Material originalMaterial;
        [SerializeField] Material flashMaterial;
        [SerializeField] float duration;
        [SerializeField] float interval;
        [SerializeField] SpriteRenderer[] sprites;
        bool _isFlash;

        void OnEnable()
        {
            foreach (var sprite in sprites)
                sprite.material = originalMaterial;
        }

        public void Play()
        {
            if (!enable) return;
            if (_isFlash) return;
            StartCoroutine(FlashRoutine());
        }

        IEnumerator FlashRoutine()
        {
            _isFlash = true;

            foreach (var sprite in sprites)
            {
                sprite.material = flashMaterial;
            }

            yield return new WaitForSeconds(duration);

            foreach (var sprite in sprites)
            {
                sprite.material = originalMaterial;
            }

            yield return new WaitForSeconds(interval);

            _isFlash = false;
        }
    }
}