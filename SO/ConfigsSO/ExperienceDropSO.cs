using Meta.Data;
using Meta.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SO.ConfigsSO
{
    [CreateAssetMenu(fileName = "Experience Drop", menuName = "Config/Experience Drop")]
    public class ExperienceDropSO : ScriptableObject
    {
        [Space(20)]
        [SerializeField] [HideLabel]
        ExperienceDropData small;

        [Space(20)]
        [SerializeField] [HideLabel]
        ExperienceDropData medium;

        public ExperienceDropData GetDrop(ExperienceDropEnum type) =>
            type switch
            {
                ExperienceDropEnum.Small => small,
                ExperienceDropEnum.Medium => medium,
                _ => null
            };
    }
}