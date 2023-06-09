using System;
using Gameplay.Units;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.Pools;

namespace Gameplay.Drop
{
    public class ExperienceDrop : PoolObject
    {
        [ReadOnly] [SerializeField] float experience;

        public Transform Transform { get; private set; }
        public Vector3 Position => Transform.position;
        void Awake() => Transform = transform;

        public void Set(float exp)
            => experience = exp;

        public void Pickup(Hero hero)
        {
            hero.Level.AddExperience(experience);
            ReturnToPool();
        }


        // protected override void OnDisabled()
        // {
        //     ReturnToPool();
        // }
    }
}