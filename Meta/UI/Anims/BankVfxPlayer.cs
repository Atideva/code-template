using System;
using System.Collections.Generic;
using GameManager;
using Gameplay.Animations;
using Meta.Data;
using Meta.Enums;
using Meta.Facade;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.Pools;

namespace Meta.UI.Anims
{
    public class BankVfxPlayer : MonoBehaviour
    {
        [SerializeField] BankStatsAnimator bankAnimator;

        [SerializeField] List<MagnetVfxData> magnets = new();
        readonly Dictionary<BankCurrencyEnum, MagnetVfxData> _magnetFields = new();
        readonly Dictionary<Vfx, VFXPool> _pools = new();

        #region TEST

        [DisableInEditorMode] [Button(ButtonSizes.Large )]
        void TestGold100()
        {
            EventsUI.Instance.PlayBankVFX(BankCurrencyEnum.Gold, 100, new Vector3(0,-3,90));
        }

        [DisableInEditorMode] [Button(ButtonSizes.Large )]
        void TestGem10()
        {
            EventsUI.Instance.PlayBankVFX(BankCurrencyEnum.GEM, 10, new Vector3(0,-3,90));
        }

        #endregion


        void Start()
        {
            foreach (var data in magnets)
            {
                var magnet = Instantiate(data.magnetPrefab, data.magnetPosition);
                data.Magnet = magnet;
                _magnetFields.Add(data.type, data);
            }

            EventsUI.Instance.OnBankVFX += PlayVfx;
        }

        void OnDisable()
        {
            EventsUI.Instance.OnBankVFX -= PlayVfx;
        }

        void PlayVfx(BankCurrencyEnum type, int amount, Vector3 fromPos)
        {
            switch (type)
            {
                case BankCurrencyEnum.Gold:

                    var goldData = _magnetFields[type];
                    var goldVfx = Pool(goldData.vfxPrefab).Get();
                    goldVfx.transform.position = fromPos;

                    if (goldVfx is BankVFX b)
                    {
                        var externalForces = b.Particle.externalForces;
                        externalForces.enabled = true;
                        var forceField = goldData.Magnet;
                        forceField.gameObject.SetActive(true);
                        externalForces.AddInfluence(forceField);
                    }

                    break;

                case BankCurrencyEnum.GEM:

                    var gemData = _magnetFields[type];
                    var gemVfx = Pool(gemData.vfxPrefab).Get();
                    gemVfx.transform.position = fromPos;

                    if (gemVfx is BankVFX g)
                    {
                        var externalForces = g.Particle.externalForces;
                        externalForces.enabled = true;
                        var forceField = gemData.Magnet;
                        forceField.gameObject.SetActive(true);
                        externalForces.AddInfluence(forceField);
                    }

                    break;

                case BankCurrencyEnum.Energy:
                    break;
                default:
                    Log.Error("Miss " + type);
                    break;
            }

            bankAnimator.IncrementAnimation(type, amount);
        }


        VFXPool Pool(Vfx vfx)
        {
            if (_pools.ContainsKey(vfx)) return _pools[vfx];

            var container = new GameObject {name = "Pool: " + vfx.name};
            container.transform.SetParent(transform);

            var pool = container.AddComponent<VFXPool>();
            pool.SetPrefab(vfx);

            _pools.Add(vfx, pool);
            return _pools[vfx];
        }
    }
}