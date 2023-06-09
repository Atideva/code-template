using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.Perks
{
    public class StarUI : MonoBehaviour
    {
        [SerializeField] Image star;
        [SerializeField] float animSpeed = 2;
        bool _fade;
        float _fadeAmount;

        public void Enable()
        {
            star.enabled = true;
            SetAlpha(1);
        }

        public void Disable()
        {
            star.enabled = false;
            StopAnimation();
        }

        void OnDisable()
        {
            StopAnimation();
        }

        public void StopAnimation() => StopAllCoroutines();

        [Button, DisableInEditorMode]
        public void PlayAnimation()
        {
            StartCoroutine(Animation());
        }

        IEnumerator Animation()
        {
            Enable();

            _fade = false;
            _fadeAmount = 0;
            SetAlpha(_fadeAmount);

            while (true)
            {
                Anim();
                yield return new WaitForSecondsRealtime(0.01f);
            }
        }

        void Anim()
        {
            if (_fade)
            {
                _fadeAmount -= 0.01f * animSpeed*0.5f;
                if (_fadeAmount < 0)
                    _fade = !_fade;
            }
            else
            {
                _fadeAmount += 0.01f * animSpeed*0.5f;
                if (_fadeAmount > 0.5f)
                    _fade = !_fade;
            }

            SetAlpha(_fadeAmount);
        }

        void SetAlpha(float alpha)
        {
            var clr = star.color;
            clr.a = alpha;
            star.color = clr;
        }
    }
}