using UnityEngine;

namespace Gameplay.Characters
{
    public interface ITarget
    {
        bool IsAlive { get; }
        Transform MyTransform { get; }
    }
}