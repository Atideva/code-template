using Gameplay.Units.UnitWeapons;
using Meta.Static;
using Sirenix.OdinInspector;
 
using SO.UnitsSO;
using UnityEngine;

namespace Gameplay.Units
{
    public class BiteUnit : Unit
    {
        [SerializeField] [Required] Bite bite;
        protected override void OnInit(UnitSO unit)
        {
            if (bite && unit is EnemySO so)
            {
                var enemies = Tags.GetEnemy(team);
                bite.SetTargets(enemies);
                bite.SetDamage(so.BiteDamage);
            }
        }
    }
}