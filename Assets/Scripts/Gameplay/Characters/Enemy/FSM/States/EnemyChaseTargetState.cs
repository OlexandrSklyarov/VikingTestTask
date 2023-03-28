using UnityEngine;

namespace Gameplay.Characters.Enemy.FSM.States
{
    public class EnemyChaseTargetState : BaseEnemyState
    {
        public EnemyChaseTargetState(IEnemyContextSwitcher context, IEnemyAgent agent) : base(context, agent)
        {
        }

        public override void OnStart()
        {
            _agent.Stop();
            _agent.StunProvider.OnStunnedEvent += Stun;
        }
      

        public override void OnStop()
        {
            _agent.Stop();
            _agent.StunProvider.OnStunnedEvent -= Stun;
        }


        public override void OnUpdate()
        {
            if (_agent.StunProvider.IsStunned()) return;
         
            if (!IsExist())
            {
                Wait();
                return;
            }

            if (IsTargetClose())
            {
                Attack();
                return;
            }
            
            MoveToTarget();
        }

        
        private bool IsExist()
        {
            return _agent.MyTarget != null && _agent.MyTarget.IsAlive;
        }


        private bool IsTargetClose()
        {
            var sqDist = (_agent.NavAgent.transform.position - _agent.MyTarget.MyTransform.position).sqrMagnitude;
            return sqDist <= _agent.AttackRange * _agent.AttackRange;
        }


        private void MoveToTarget()
        {
            _agent.NavAgent.SetDestination(_agent.MyTarget.MyTransform.position);
            _agent.AnimatorProvider.SetSpeed(Mathf.Clamp01(_agent.NavAgent.velocity.magnitude));
            _agent.RotateViewToDirection(_agent.NavAgent.velocity);
        }


        private void Attack() => _context.SwitchState<EnemyAttackState>();
        
        
        private void Wait() => _context.SwitchState<EnemyWaitState>();

        
        private void Stun() => _context.SwitchState<EnemyDamageState>();
    }
}