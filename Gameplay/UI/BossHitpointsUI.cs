using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using static Utilities.Extensions.UIExtensions;

namespace Gameplay.UI
{
    public class BossHitpointsUI : MonoBehaviour
    {
        [SerializeField] Slider slider;
        [SerializeField] CanvasGroup canvasGroup;
        [Header("Flash Anim")]
        [SerializeField] Image flashImage;
        [SerializeField] float flashDuration = 0.2f;

        bool _isFlash;

        public void Show()
        {
            EnableGroup(canvasGroup);
        }

        public void Hide()
        {
            DisableGroup(canvasGroup);
        }

        public void Refresh(float value)
        {
            slider.value = value;
            FlashAnim();
        }

        void FlashAnim()
        {
            if (!flashImage) return;

            if (_isFlash) return;
            _isFlash = true;

            flashImage
                .DOFade(1, flashDuration / 2)
                .OnComplete(()
                    => flashImage
                        .DOFade(0, flashDuration / 2)
                        .OnComplete(() => _isFlash = false));
        }
    }
}