using Meta.Enums;
using UnityEngine;

namespace SO.UnitsSO
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Gameplay/Units/Enemy")]
    public class EnemySO : UnitSO
    {
 
        [SerializeField] float biteDamage;
        [SerializeField] ExperienceDropEnum experienceDrop;
        public float BiteDamage => biteDamage;
   
        public ExperienceDropEnum ExperienceDrop => experienceDrop;

    }
}