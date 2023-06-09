using DG.Tweening;
using UnityEngine;
using static Utilities.Extensions.UIExtensions;

namespace Meta.UI
{
    public class BossWarningUI : MonoBehaviour
    {
        public CanvasGroup group;
        public float fadeTime = 0.3f;
 
        public DOTweenAnimation textAnim;
        
 
        public void Show()
        {
            group.DOFade(1, fadeTime);
            textAnim.DOPlay();
        }

        public void Hide()
        {
            group.DOFade(0, fadeTime).OnComplete(Disable);
            textAnim.DOPause();
        }

        public  void Disable() => DisableGroup(group);
    }
}