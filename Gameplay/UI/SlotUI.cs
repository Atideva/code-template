using System.Collections.Generic;
using DG.Tweening;
using I2.Loc;
using Meta.UI.Anims;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Gameplay.UI
{
    public class SlotUI : MonoBehaviour
    {
        [SerializeField] Image background;
        [SerializeField] Image icon;
        [SerializeField] Material fillMaterial;
 
        [Header("Animation")]
        [SerializeField] List<Image> highlights = new();
        [SerializeField] DOTweenAnimation rotateAnim;
        [SerializeField] CanvasGroup containerGroup;
        [SerializeField] float appearDelay = 0.5f;
        [SerializeField] float appearTime = 0.3f;
        [SerializeField] float scaleSize = 2f;
        [SerializeField] float scaleTime = 0.3f;

        [Header("Step info")]
        [SerializeField] CanvasGroup infoGroup;
        [SerializeField] Localize infoLoc;
        [SerializeField] TextMeshProUGUI infoText;
        [SerializeField] LocalizationParamsManager infoParams;
        [SerializeField] bool useShake;
        [SerializeField] ShakingNumbers infoShake;
        
        public void RefreshInfo(string term, int value)
        {
            // infoLoc.enabled = true;
            // infoParams.enabled = true;
            //
            infoLoc.SetTerm(term);
            infoParams.SetParameterValue("VALUE", value.ToString());
            
            if(useShake) infoShake.ApplyShake();
        }

        public void RefreshInfo(string txt,Color clr)
        {
            // infoLoc.enabled = false;
            // infoParams.enabled = false;
            //
            infoText.text = txt;
            infoText.color = clr;
            
            if(useShake) infoShake.ApplyShake();
        }
        public void Set(Sprite complete, Sprite empty)
        {
            background.sprite = empty;
            icon.sprite = complete;
            Reset();
        }

        public void ShowIcon()
        {
            icon.enabled = true;
        }

        public void HideIcon()
        {
            icon.enabled = false;
        }

        public void SetFill(float value)
        {
            icon.fillAmount = value;
            icon.material =   fillMaterial  ;
        }

        public void DisableFill()
        {
            icon.fillAmount = 1;
            icon.material =   null  ; 
        }
        void Reset()
        {
            ShowIcon();
            StopHighlight();
            HideInfo();
        }

        public void Empty()
        {
            icon.enabled = false;
        }

        public void Full()
        {
            icon.enabled = true;
        }

        public void ScaleAnim()
        {
            var t = containerGroup.transform;
            t
                .DOScale(scaleSize, scaleTime / 2)
                .SetDelay(appearDelay)
                .OnComplete(()
                    => t.DOScale(1, scaleTime / 2));
        }

        public void AppearAnim()
        {
            containerGroup.alpha = 0;
            containerGroup
                .DOFade(1, appearTime)
                .SetDelay(appearDelay);
        }

        public void ShowInfo()
        {
            infoGroup.alpha = 1;
        }

        public void HideInfo()
        {
            infoGroup.alpha = 0;
        }

        public void Highlight()
        {
            foreach (var img in highlights)
                img.enabled = true;

            rotateAnim.DORestart();
            rotateAnim.DOPlay();
        }

        public void StopHighlight()
        {
            foreach (var img in highlights)
                img.enabled = false;
        }
    }
}