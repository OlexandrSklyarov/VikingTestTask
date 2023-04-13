namespace Gameplay.Characters.Enemy.FSM.States
{
    public class EnemyWaitState : BaseEnemyState
    {
        public EnemyWaitState(IEnemyContextSwitcher context, IEnemyAgent agent) : base(context, agent)
        {
        }

        public override void OnStart()
        {
            _agent.Stop();
            if (_agent.IsTargetExist) ChaseTarget();
        }


        public override void OnStop()
        {
        }


        private void ChaseTarget() => _context.SwitchState<EnemyChaseTargetState>();
    }
}