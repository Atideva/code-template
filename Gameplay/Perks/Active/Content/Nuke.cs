using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Pools;

namespace Gameplay.Perks.Active.Content
{
    public class Nuke : PoolObject
    {
        [SerializeField] NukeCollider nukeCollider;
        [SerializeField] float damageDelay;
        [SerializeField] float vfxLifeTime;
        [SerializeField] Image blindImage;
        [SerializeField] float blindDelay;
        [SerializeField] float blindMaxAlpha;
        [SerializeField] float blindAppearTime;
        [SerializeField] float blindFadeTime;
        [SerializeField] float blindDuration;
        [SerializeField] Canvas blindCanvas;


        public void Activate(NukePerk perk)
        {
            nukeCollider.SetPerk(perk);
            FlashScreen();
            Invoke(nameof(Damage), damageDelay);
            Invoke(nameof(Disable), vfxLifeTime);
        }

        void Damage() => nukeCollider.Enable(0.1f);

        void Disable()
        {
            blindCanvas.enabled = false;
            ReturnToPool();
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
    }
}