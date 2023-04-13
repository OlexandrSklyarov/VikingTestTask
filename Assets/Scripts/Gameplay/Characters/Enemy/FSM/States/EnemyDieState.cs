using Common.Routine;

namespace Gameplay.Characters.Enemy.FSM.States
{
    public class EnemyDieState : BaseEnemyState
    {
        public EnemyDieState(IEnemyContextSwitcher context, IEnemyAgent agent) : base(context, agent)
        {
        }

        public override void OnStart()
        {
            _agent.PrepareForDie();

            RoutineManager.RunWithDelay
            (
                () => _agent.Die(),
                _agent.Config.DeathDelay
            );
        }

        public override void OnStop()
        {
        }
    }
}