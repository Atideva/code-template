using DG.Tweening;
using GameManager;
using Gameplay.Units;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Gameplay.UI
{
    public class KillsCounterUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI txt;
        [SerializeField] [ReadOnly] int kills;
     
        [Header("Scale Anim")]
        [SerializeField] bool useScaleAnim;
        [SerializeField] float scaleDuration = 0.15f;
        [SerializeField] float scaleSize = 1.3f;
        bool _isScale;

        public int Total => kills;

        void Start()
        {
            GameplayEvents.Instance.OnUnitDeath += Kill;
            RefreshText();
        }
        void OnDisable()
        {
            GameplayEvents.Instance.OnUnitDeath -= Kill;
        }

        void Kill(Unit unit)
        {
            kills++;
            RefreshText();
            if(useScaleAnim)
                ScaleAnim();
        }

        void RefreshText() => txt.text = kills.ToString();
        
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