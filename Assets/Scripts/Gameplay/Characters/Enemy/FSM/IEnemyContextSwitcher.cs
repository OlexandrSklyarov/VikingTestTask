namespace Gameplay.Characters.Enemy.FSM
{
    public interface IEnemyContextSwitcher
    {
        void SwitchState<T>() where T : BaseEnemyState;
    }
}