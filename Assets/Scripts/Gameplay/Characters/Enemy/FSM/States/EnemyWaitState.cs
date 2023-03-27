namespace Gameplay.Characters.Enemy.FSM.States
{
    public class EnemyWaitState : BaseEnemyState
    {
        public EnemyWaitState(IEnemyContextSwitcher context, IEnemyAgent agent) : base(context, agent)
        {
        }

        public override void OnStart()
        {
            _agent.NavAgent.SetDestination(_agent.NavAgent.transform.position);

            if (_agent.MyTarget != null && _agent.MyTarget.IsAlive) ChaseTarget();
        }


        public override void OnStop()
        {
        }


        private void ChaseTarget() => _context.SwitchState<EnemyChaseTargetState>();
    }
}