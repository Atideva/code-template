using System;
using System.Collections.Generic;
using DG.Tweening;
using I2.Loc;
using Meta.Data;
using Meta.UI.Anims;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Utilities.Extensions.UIExtensions;

namespace Gameplay.UI
{
    public class AchievementUI : MonoBehaviour
    {
        [Header("Reward")]
        [SerializeField] Image icon;
        [SerializeField] Image defaultIcon;
        [SerializeField] Image fill;
        [SerializeField] TextMeshProUGUI fillTxt;
        [SerializeField] LocalizedString rewardLoc;
        [SerializeField] CanvasGroup rewardGroup;

        [Header("Reward anim")]
        [SerializeField] DOTweenAnimation sunRaysRotate;
        [SerializeField] Image sunRays;
        [SerializeField] float rewardAnimTime;
        [SerializeField] float rewardAnimDelay = 0.9f;

        [Space(20)]
        [SerializeField] RectTransform container;
        [SerializeField] float appearTime = 0.6f;
        [SerializeField] float showTime = 2f;

        [Space(20)]
        [SerializeField] Localize descriptionLoc;
        [SerializeField] LocalizationParamsManager descriptionParams;
        [SerializeField] bool useColor;
        [SerializeField] bool useShake;
        [SerializeField] ColorNumbers descriptionNumberColor;
        [SerializeField] ShakingNumbers shakeNumbers;

        [Space(20)]
        [ListDrawerSettings(Expanded = true)]
        [SerializeField] List<SlotUI> slots = new();

        //readonly float[] _fillValues = {1f, 0.7f, 0.5f, 0.3f, 0f};
        readonly float[] _fillValues = {1f, 1f, 1f, 1f, 0f};
        readonly string[] _fillStrings = {"20%", "40%", "60%", "80%", "Reward"};
        public int SlotsCount => slots.Count;

        public IReadOnlyList<SlotUI> Slots => slots;

        public event Action<AchievementUI> OnHide = delegate { };

        void Hide()
        {
            sunRaysRotate.DOPause();
            OnHide(this);
        }

        public void Refresh(AchievementUIData data)
        {
            if (data.Stage.CompletedSteps >= slots.Count)
                data.Stage.CompletedSteps = slots.Count;

            RefreshReward(data.RewardIcon, data.Stage.CompletedSteps);
            RefreshDescription(data.DescriptionLocalize.mTerm, (int) data.Stage.TotalRequire);
            ShowAchievedSlotInfo(data.Stage.CompletedSteps, data.SlotInfoLocalize.mTerm, (int) data.Stage.SumRequire);
            RefreshFill(data.Stage.CompletedSteps);
            RefreshSlots(data.Stage.CompletedSteps, data.FullSlotSprite, data.EmptySlotSprite);
        }

        void RefreshFill(int complete)
        {
            fillTxt.gameObject.SetActive(complete == slots.Count);
            
            var id = complete - 1;
            if (id < 0) return;

            var localizeTrigger = _fillStrings.Length - 1;

            fill.fillAmount = _fillValues[id];
            fillTxt.text = id < localizeTrigger
                ? _fillStrings[id]
                : rewardLoc;

        }

        void RefreshReward(Sprite newIcon, int complete)
        {
            sunRays.enabled = false;

            if (newIcon)
            {
                icon.sprite = newIcon;
                fill.sprite = newIcon;

                EnableGroup(rewardGroup);
                defaultIcon.enabled = false;
            }
            else
            {
                DisableGroup(rewardGroup);
                defaultIcon.enabled = true;
            }

            if (complete >= slots.Count)
                Invoke(nameof(HighlightReward), rewardAnimDelay);
        }

        void RefreshDescription(string localizeTerm, int value)
        {
            descriptionLoc.SetTerm(localizeTerm);
            descriptionParams.SetParameterValue("VALUE", value.ToString());
            if (useColor) Invoke(nameof(ApplyColor), 0.2f);
        }

        public void ShakeNumbers()
        {
            if (useShake) Invoke(nameof(ApplyShake), 0.1f);
        }

        void ApplyColor() => descriptionNumberColor.ApplyColor();
        void ApplyShake() => shakeNumbers.ApplyShake();

        void HighlightReward()
        {
            //   slots[4].StopHighlight();

            sunRays.enabled = true;
            sunRaysRotate.DORestart();
            sunRays.DOPlay();

            rewardGroup.transform
                .DOScale(2, rewardAnimTime / 2)
                .OnComplete(() =>
                    rewardGroup.transform.DOScale(1, rewardAnimTime / 2));
        }

        public void MoveInAnimation()
        {
            var offsetX = -container.rect.width - 100;
            container.anchoredPosition = new Vector2(offsetX, 0);

            container
                .DOAnchorPosX(0, appearTime)
                .OnComplete(()
                    => container
                        .DOAnchorPosX(offsetX, appearTime * 1.5f)
                        .SetEase(Ease.InQuad)
                        .SetDelay(showTime)
                        .OnComplete(Hide));
        }

        public void HighlightAchievedSlot(int complete)
        {
            if (complete == 0 ||
                complete > slots.Count) return;

            var achievedSlot = slots[complete - 1];
            achievedSlot.AppearAnim();
            achievedSlot.ScaleAnim();
            achievedSlot.Highlight();
            achievedSlot.ShowInfo();
        }

        public void ShowAchievedSlotInfo(int complete, string term, int value)
        {
            if (complete == 0 ||
                complete > slots.Count) return;
            
            var achievedSlot = slots[complete - 1];
            achievedSlot.RefreshInfo(term, value);
        }

        public void PlaySlotsScaleAnimation()
        {
            foreach (var slot in slots)
            {
                slot.AppearAnim();
                slot.ScaleAnim();
            }
        }

        void RefreshSlots(int complete, Sprite completeSprite, Sprite emptySprite)
        {
            for (var i = 0; i < slots.Count; i++)
            {
                var slot = slots[i];
                slot.Set(completeSprite, emptySprite);

                if (i < complete)
                    slot.Full();
                else
                    slot.Empty();
            }
            //
            // if (complete < slots.Count)
            // {
            //     var lastSlot = slots[complete - 1];
            //     lastSlot.AppearAnim();
            //     lastSlot.ScaleAnim();
            //     lastSlot.Highlight();
            // }
            // else
            // {
            //     foreach (var slot in slots)
            //     {
            //         slot.AppearAnim();
            //         slot.ScaleAnim();
            //     }
            // }
        }
    }
}