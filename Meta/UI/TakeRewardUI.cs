using System;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.UI
{
    public class TakeRewardUI : MonoBehaviour
    {
        [SerializeField] Image icon;
        [SerializeField] List<Image> highlights=new();
        [SerializeField] DOTweenAnimation glowAnim;
        [SerializeField] TextMeshProUGUI txt;
        [SerializeField] float moveDist;
        [SerializeField] float moveTime;
        public int RewardValue { get; private set; }
        public event Action<TakeRewardUI> OnCollectComplete = delegate { };

        public void Set(int rewardValue)
        {
            RewardValue = rewardValue;
            icon.enabled = true;
            txt.enabled = false;
 
        }

        [Button, DisableInEditorMode]
        public void PlayCollectAnimation()
        {
            icon.enabled = false;

            txt.text = "+" + RewardValue;
            txt.enabled = true;

            var rect = (RectTransform) transform;
            rect
                .DOAnchorPosY(moveDist, moveTime)
                .OnComplete(CollectComplete);
        }

        public void DisableHighlight()
        {
            foreach (var image in highlights)
                image.enabled = false;
            
            glowAnim.DOPause();
        }
        void CollectComplete()
        {
            OnCollectComplete(this);
        }

        public void EnableHighlight()
        {
            foreach (var image in highlights)
                image.enabled = true;
            
            glowAnim.DORestart();
            glowAnim.DOPlay();
        }

  
    }
}