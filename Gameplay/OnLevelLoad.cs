using GameManager;
using UnityEngine;

namespace Gameplay
{
    public class OnLevelLoad : MonoBehaviour
    {
        void Start() => Game.Instance.OnLevelLoad();
    }
}
