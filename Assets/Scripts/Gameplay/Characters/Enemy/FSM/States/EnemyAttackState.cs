
namespace Gameplay.Characters.Enemy.FSM.States
{
    public class EnemyAttackState : BaseEnemyState
    {
        public EnemyAttackState(IEnemyContextSwitcher context, IEnemyAgent agent) : base(context, agent)
        {
        }

        public override void OnStart()
        {
            _agent.AnimatorProvider.AttackEvent += OnAttackExecuteHandler;
            
            _agent.Stop();

            if (IsTargetNotExist()) Wait();
        }
        

        public override void OnStop()
        {
            _agent.AnimatorProvider.AttackEvent -= OnAttackExecuteHandler;
        }

        
        private void OnAttackExecuteHandler()
        {
            _agent.AttackProvider.ApplyDamage();
        }


        public override void OnUpdate()
        {
            if (IsTargetNotExistOrFar())
            {
                Wait();
                return;
            }
            
            if (_agent.AttackProvider.IsCanAttack())
            {
                _agent.RotateViewToTarget(_agent.MyTarget.MyTransform.position);
                _agent.AnimatorProvider.PlayAttack();
                _agent.AttackProvider.StartAttack();
            }
            
        }

        private bool IsTargetNotExistOrFar()
        {
            return IsTargetNotExist() ||
                   (IsTargetFar() && !_agent.AttackProvider.IsAttackActive);
        }

        private bool IsTargetNotExist()
        {
            return _agent.MyTarget == null || !_agent.MyTarget.IsAlive;
        }


        private bool IsTargetFar()
        {
            var sqDist = (_agent.NavAgent.transform.position - _agent.MyTarget.MyTransform.position).sqrMagnitude;
            return sqDist > _agent.AttackRange * _agent.AttackRange;
        }


        private void Wait() => _context.SwitchState<EnemyWaitState>();
    }
}