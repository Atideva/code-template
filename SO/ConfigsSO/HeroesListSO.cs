using System.Collections.Generic;
using Sirenix.OdinInspector;
using SO.UnitsSO;
using UnityEngine;

namespace SO.ConfigsSO
{
    [CreateAssetMenu(fileName = "Heroes List", menuName = "Config/Heroes List")]
    public class HeroesListSO : ScriptableObject
    {
        [InfoBox("Please, add heroes here manually / Незабудь добавить сюда героев")]
        [Space(20)]
        [ListDrawerSettings(Expanded = true), InlineEditor]
        [SerializeField] List<HeroSO> list = new();

        public IReadOnlyList<HeroSO> List => list;
    }
}
