using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Perks.Active.Content
{
    public class ElectricBall : MonoBehaviour
    {
        [SerializeField] [ReadOnly] ElectricBallsPerk perk;
        [SerializeField] ElectricBallCollider ballCollider;
        [SerializeField] ParticleSystem particle;
        [SerializeField] Transform container;
        public bool IsInit { get; private set; }

        public void Init(ElectricBallsPerk ballsPerk, float dmgInterval)
        {
            IsInit = true;
            perk = ballsPerk;
            ballCollider.SetPerk(ballsPerk);
            ballCollider.SetInterval(dmgInterval);
        }

        public void SetBulletBlock(bool isBlockBullets)
        {
            ballCollider.SetBulletBlock(isBlockBullets);
        }

        public void Enable(float appearTime)
        {
            container.gameObject.SetActive(true);
            container.transform.localScale = Vector3.zero;
            container.transform.DOScale(perk.Stats.ballSize, appearTime);
        }

        public void SetLifeTime(float lifeTime, float hideTime)
        {
            Invoke(nameof(Hide), lifeTime - hideTime);
            Invoke(nameof(Disable),lifeTime);
        }
        public void Hide(float appearTime)
        {
            container.transform.DOScale(0, appearTime);
        }

        public void Disable()
        {
            container.gameObject.SetActive(false);
        }
    }
}