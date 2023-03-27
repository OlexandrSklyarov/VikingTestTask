namespace Gameplay.Characters.Enemy.FSM.States
{
    public class EnemyChaseTargetState : BaseEnemyState
    {
        private readonly float _attackDistance;

        public EnemyChaseTargetState(IEnemyContextSwitcher context, IEnemyAgent agent) : base(context, agent)
        {
            _attackDistance = _agent.NavAgent.radius * 2f;
        }

        public override void OnStart()
        {
            _agent.NavAgent.SetDestination(_agent.NavAgent.transform.position);
            _agent.StunProvider.OnStunnedEvent += OnStunHandler;
        }
      

        public override void OnStop()
        {
            _agent.AnimatorProvider.SetSpeed(0f);
            _agent.StunProvider.OnStunnedEvent -= OnStunHandler;
        }


        public override void OnUpdate()
        {
            if (_agent.MyTarget == null || !_agent.MyTarget.IsAlive) Wait();

            MoveToTarget();

            if (_agent.NavAgent.remainingDistance <= _attackDistance) Attack();
        }

        
        private void MoveToTarget()
        {
            _agent.NavAgent.SetDestination(_agent.MyTarget.MyTransform.position);
            _agent.AnimatorProvider.SetSpeed(1f);
            _agent.RotateViewToDirection(_agent.MyTarget.MyTransform.position + _agent.NavAgent.velocity.normalized);
        }


        private void Attack() => _context.SwitchState<EnemyAttackState>();
        
        
        private void Wait() => _context.SwitchState<EnemyWaitState>();

        
        private void OnStunHandler() => _context.SwitchState<EnemyDamageState>();
    }
}