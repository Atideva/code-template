using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Utilities.Extensions.UIExtensions;

namespace Gameplay.UI
{
    public class HeroLevelUI : MonoBehaviour
    {
        [SerializeField] CanvasGroup canvasGroup;
        [SerializeField] Slider slider;
        [SerializeField] TextMeshProUGUI lvlText;

        [Header("Scale Anim")]
        [SerializeField] Transform scaleTransform;
        [SerializeField] float scaleDuration = 0.3f;
        [SerializeField] float scaleSize = 1.5f;

        [Header("Flash Anim")]
        [SerializeField] Image flashImage;
        [SerializeField] float flashDuration = 0.2f;

        [Header("Highlight Anim")]
        [SerializeField] float highlightDuration = 2f;
        [SerializeField] List<Image> highlights = new();

        bool _isFlash;
        bool _isScale;
        bool _isHighlight;

        void Start()
        {
            RefreshExperience(0);
            RefreshLevel(1);
        }
        public void Show()
        {
            EnableGroup(canvasGroup);
        }

        public void Hide()
        {
            DisableGroup(canvasGroup);
        }
        public void RefreshLevel(int level)
        {
            lvlText.text = level.ToString();
            ScaleAnim();
            HighlightAnimation();
        }

        public void RefreshExperience(float value)
        {
            slider.value = value;
            FlashAnim();
        }


        void ScaleAnim()
        {
            if (_isScale) return;
            _isScale = true;

            scaleTransform
                .DOScale(scaleSize, scaleDuration / 2)
                .OnComplete(()
                    => scaleTransform
                        .DOScale(1, scaleDuration / 2)
                        .OnComplete(() => _isScale = false));
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

        void HighlightAnimation()
        {
            if (_isHighlight) return;
            _isHighlight = true;

            foreach (var img in highlights)
            {
                img.DOFade(1, 0.1f)
                    .OnComplete(()
                        => img
                            .DOFade(0, 0.1f)
                            .SetDelay(highlightDuration)
                            .OnComplete(() => _isHighlight = false));
            }
        }
    }
}