using Common.Routine;

namespace Gameplay.Characters.Enemy.FSM.States
{
    public class EnemySpawnState : BaseEnemyState
    {
        public EnemySpawnState(IEnemyContextSwitcher context, IEnemyAgent agent) : base(context, agent)
        {
        }

        public override void OnStart()
        {
            RoutineManager.RunWithDelay
            (
                () => _context.SwitchState<EnemyWaitState>(),
                _agent.Config.SpawnDelay
            );
        }

        public override void OnStop()
        {
        }
    }
}