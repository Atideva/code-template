using System.Collections.Generic;
using Gameplay.AI.BossAbilities;
using Meta.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SO.UnitsSO
{
    [CreateAssetMenu(fileName = "New Boss", menuName = "Gameplay/Units/Boss")]
    public class BossSO : UnitSO
    {
        [Space(20)]
        [SerializeField] MoveBlockData moveBlockSettings;
        [Space(20)]
        [InlineEditor()]
        [SerializeField] List<BossAbility> abilities = new();
        public IReadOnlyList<BossAbility> Abilities => abilities;

        public MoveBlockData MoveBlockSettings => moveBlockSettings;
    }
}