using GameManager;
using Gameplay.Animations;
using UnityEngine;

namespace Meta.Facade
{
    public static class VFX
    {
        static VfxPlayer _vfxPlayer;

        static void CheckPlayer()
        {
            if (_vfxPlayer) return;
            _vfxPlayer = VfxPlayer.Instance;
            Log.Warning("Vfx player has been created");
        }

        static bool Null
        {
            get
            {
                CheckPlayer();
                if (_vfxPlayer) return false;
                Log.InitError(nameof(VFX));
                return true;
            }
        }

        public static void Init(VfxPlayer vfx)
        {
            _vfxPlayer = vfx;
        }

        public static void Play(Vfx vfx, Vector3 pos)
        {
            if (Null) return;
            _vfxPlayer.Play(vfx, pos, Quaternion.identity, 0);
        }

        public static void Play(Vfx vfx, Vector3 pos, float delay)
        {
            if (Null) return;
            _vfxPlayer.Play(vfx, pos, Quaternion.identity, delay);
        }

        public static void PlayAttached(AttachedVfx vfx, Transform target, float delay = 0)
        {
            if (Null) return;
            _vfxPlayer.PlayAttached(vfx, target, delay);
        }
    }
}