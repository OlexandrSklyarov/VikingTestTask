
using UnityEngine;

namespace Gameplay.Characters.Attack
{
    public interface IDamage
    {
        Vector3 Position { get; }
        void TryApplyDamage(int damage, float stunTime);
    }
}