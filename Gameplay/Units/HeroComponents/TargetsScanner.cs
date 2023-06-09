using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;


// ReSharper disable InvertIf

namespace Gameplay.Units.HeroComponents
{
    public class TargetsScanner : MonoBehaviour
    {
        [Tag] public string targets;
        [SerializeField] CircleCollider2D triggerCol;
        [SerializeField] List<Transform> targetsInRange=new();
        [SerializeField] [Sirenix.OdinInspector.ReadOnly] bool scanRequested;
        Transform me;
        public event Action OnScan = delegate { };
        public bool NoTargets => targetsInRange.Count == 0;
        // [SerializeField] float scanDistance = 3;

        void Start()
        {
            me = transform;
            triggerCol.enabled = false;
            //    col.size = new Vector2(scanDistance, scanDistance * 1.15f);
        }
 
        public void Scan()
        {
            if (scanRequested) return;
            
            scanRequested = true;
            targetsInRange.Clear();
            triggerCol.enabled = true;

            Invoke(nameof(ScanComplete), Time.fixedDeltaTime);
        }

 
        void ScanComplete()
        {
            OnScan();
            scanRequested = false;
            triggerCol.enabled = false;
        }

 
        void OnTriggerEnter2D(Collider2D enter)
        {
            if (enter.CompareTag(targets))
            {
                if (!targetsInRange.Contains(enter.transform))
                    targetsInRange.Add(enter.transform);
            }
        }

        void OnTriggerExit2D(Collider2D exit)
        {
            if (exit.CompareTag(targets))
            {
                if (targetsInRange.Contains(exit.transform))
                    targetsInRange.Remove(exit.transform);
            }
        }

        public Transform GetRandomTarget()
        {
            if (NoTargets) return null;

            var r = Random.Range(0, targetsInRange.Count);
            return targetsInRange[r];
        }

        public Transform GetClosestTarget()
        {
            if (NoTargets) return null;

            Transform closest = null;
            var min = float.MaxValue;
            var pos = me.position;

            foreach (var target in targetsInRange)
            {
                if ((target.position - pos).sqrMagnitude < min * min)
                {
                    min = (target.position - pos).magnitude;
                    closest = target;
                }
            }

            return closest;
        }
    }
}