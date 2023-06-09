using Gameplay.AI;
using SO.UnitsSO;
 

namespace Gameplay.Units
{
    public class Boss : Unit
    {
        protected override void OnInit(UnitSO unit)
        {
            if (unit is not BossSO so) return;
            if (!HasAI || AI is not BossAI ai) return;
            
            foreach (var ability in so.Abilities)
            {
                ai.Add(ability);
            }

            ai.InitAbilities();
        }
    }
}