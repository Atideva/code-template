using System;
using Meta.Enums;
using Meta.UI.Anims;
using UnityEngine;

namespace Meta.Data
{
    [Serializable]
    public class MagnetVfxData
    {
        public BankCurrencyEnum type;
        public BankVFX vfxPrefab;
        public ParticleSystemForceField magnetPrefab;
        public RectTransform magnetPosition;
        public ParticleSystemForceField Magnet { get; set; }
    }
}