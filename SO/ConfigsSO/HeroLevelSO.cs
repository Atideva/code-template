using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SO.ConfigsSO
{
    [CreateAssetMenu(fileName = "Hero Level", menuName = "Config/Hero Level")]
    public class HeroLevelSO : ScriptableObject
    {

     
        [Space(20)]
        [SerializeField] float expPerKill;

        [Space(20)]
        [SerializeField] float expPerLevel;
        [SerializeField] float multPerLevel;
        [SerializeField] int maxLevel = 50;
        [ReadOnly] [SerializeField] List<float> table = new();
        //
        // [Header("CURVE")]
        // [SerializeField] float maxLevelExpirience;
        // [SerializeField] AnimationCurve curve;
        // [ReadOnly] [SerializeField] List<float> curveTable = new();

        public IReadOnlyList<float> Table => table;

        public float ExpPerKill => expPerKill;

        void OnValidate()
        {
            table = new List<float>();
            for (int i = 0; i < maxLevel; i++)
            {
                var exp = expPerLevel * (i + 1) + expPerLevel * (i * multPerLevel);
                table.Add(exp);
            }

            // curveTable = new List<float>();
            // for (int i = 0; i < 50; i++)
            // {
            //     var ex = curve.Evaluate((float) (i + 1) / total);
            //     var exp = maxLevelExpirience * ex;
            //     curveTable.Add(exp);
            // }
        }
    }
}