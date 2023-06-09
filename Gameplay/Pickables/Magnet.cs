using GameManager;

namespace Gameplay.Pickables
{
    public class Magnet : Pickable
    {
        protected override void OnPickup()
        {
            GameplayEvents.Instance.MagnetPickup();
            Destroy(gameObject);
        }
    
    }
}
