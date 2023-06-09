using System;
using DG.Tweening;
using GameManager;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Gameplay.UI
{
    public class GoldCounterUI : MonoBehaviour
    {
 
        [SerializeField] TextMeshProUGUI txt;
        [SerializeField] [ReadOnly] int gold;
     
        [Header("Scale Anim")]
        [SerializeField] bool useScaleAnim;
        [SerializeField] float scaleDuration = 0.15f;
        [SerializeField] float scaleSize = 1.3f;
        bool _isScale;
        
        void Start()
        {
            GameplayEvents.Instance.OnAddGold += Add;
            RefreshText();
        }

        void OnDisable()
        {
            GameplayEvents.Instance.OnAddGold -= Add;
        }

        void Add(int amount)
        {
            gold++;
            RefreshText();
            if(useScaleAnim)
                ScaleAnim();
        }

        void RefreshText() => txt.text = gold.ToString();
        
        void ScaleAnim()
        {
            if (_isScale) return;
            _isScale = true;

            txt.transform
                .DOScale(scaleSize, scaleDuration / 2)
                .OnComplete(()
                    => txt.transform
                        .DOScale(1, scaleDuration / 2)
                        .OnComplete(() => _isScale = false));
        }

 
    }
}
