using Gameplay.Units;
using UnityEngine;

namespace Gameplay.Interface
{
    public interface IHasModel
    {
        Transform ModelContainer { get; }
        UnitModel Model { get; }
    }
}