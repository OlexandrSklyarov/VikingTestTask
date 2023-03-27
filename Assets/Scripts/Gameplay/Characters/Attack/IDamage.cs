
namespace Gameplay.Characters.Attack
{
    public interface IDamage
    {
        void TryApplyDamage(int damage, float stunTime);
    }
}