using System;
using DG.Tweening;
using Gameplay.Perks.Active.Content;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Utilities.MonoCache.System;

namespace Gameplay.Pickables
{
    public class NukeBomb : Pickable
    {
        protected override void OnPickup()
        {
            Debug.LogError("PICKED");
            Activate();
        }

        void Awake()
        {
            nuke.Disable();
            // blindCanvas.enabled = false;
        }

        [SerializeField] GameObject nuke;
        [SerializeField] GameObject bombSprite;
        [SerializeField] NukeBombCollider nukeCollider;
        [SerializeField] float damageDelay;
        [SerializeField] float vfxLifeTime;
        [SerializeField] Image blindImage;
        [SerializeField] float blindDelay;
        [SerializeField] float blindMaxAlpha;
        [SerializeField] float blindAppearTime;
        [SerializeField] float blindFadeTime;
        [SerializeField] float blindDuration;
        [SerializeField] Canvas blindCanvas;
        [SerializeField] float damage;
        [NaughtyAttributes.Tag] [SerializeField] string targets = "Enemy";
        public string Targets => targets;

        public void Activate()
        {
            nuke.Enable();
            bombSprite.Disable();
            
            nukeCollider.SetPerk(this);
            FlashScreen();
            Invoke(nameof(DoDamage), damageDelay);
            Invoke(nameof(Disable), vfxLifeTime);
        }

        void DoDamage() => nukeCollider.Enable(0.1f);

        void Disable()
        {
            Destroy(gameObject);
        }

        [Button(ButtonSizes.Large), DisableInEditorMode]
        void FlashScreen()
        {
            blindCanvas.enabled = true;
            blindImage.DOFade(0, 0);
            blindImage
                .DOFade(blindMaxAlpha, blindAppearTime)
                .SetDelay(blindDelay)
                .OnComplete(()
                    => blindImage
                        .DOFade(0, blindFadeTime)
                        .SetDelay(blindDuration));
        }

        public float Damage => damage;
    }
}