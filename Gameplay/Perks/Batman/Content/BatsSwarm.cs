using Gameplay.Perks.Active.Content;
using Gameplay.Perks.Batman;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.Pools;

public class BatsSwarm : PoolObject
{
    [SerializeField] BatsSwarmCollider flameCollider;
    [SerializeField] Transform sizeContainer;
    [Tooltip("If vfx is too slow, set this to sync 'dmg deal' and 'visual'")]
    [SerializeField] float firstDamageDelay = 0.1f;
    [SerializeField] [ReadOnly] BatsSwarmPerk perk;
    [SerializeField] [ReadOnly] float timer;

    public void Enable()
    {
        var size = perk.Stats.distance;
        sizeContainer.transform.localScale = new Vector3(size, size, size);

        flameCollider.Disable();
        timer = firstDamageDelay;
        Invoke(nameof(Destroy), firstDamageDelay + perk.LifeTime);
    }

    protected override void OnFixedUpdate()
    {
        timer -= Time.fixedDeltaTime;
        if (timer > 0) return;

        timer = perk.DmgInterval;
        EnableCollider();
    }

    void EnableCollider() => flameCollider.Enable();
    public void Destroy() => ReturnToPool();

    public void SetPerk(BatsSwarmPerk p)
    {
        perk = p;
        flameCollider.SetPerk(p);
    }

    public void SetAngle(float angle)
        => transform.localRotation = Quaternion.Euler(0, 0, angle);
}