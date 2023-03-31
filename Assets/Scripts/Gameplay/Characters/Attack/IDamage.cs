
using UnityEngine;

namespace Gameplay.Characters.Attack
{
    public interface IDamage
    {
        bool IsAlive { get; }
        Vector3 Position { get; }
        void TryApplyDamage(int damage, float stunTime);
    }
}