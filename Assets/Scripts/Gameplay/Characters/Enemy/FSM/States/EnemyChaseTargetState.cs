using UnityEngine;

namespace Gameplay.Characters.Enemy.FSM.States
{
    public class EnemyChaseTargetState : BaseEnemyState
    {
        private float _curSpeed;

        public EnemyChaseTargetState(IEnemyContextSwitcher context, IEnemyAgent agent) : base(context, agent)
        {
        }

        public override void OnStart()
        {
            _agent.OnStunnedEvent += Stun;
            _curSpeed = 0f;
            _agent.Move();
        }
      

        public override void OnStop()
        {
            _agent.Stop();
            _agent.OnStunnedEvent -= Stun;
        }


        public override void OnUpdate()
        {
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
            _agent.RotateViewToDirection(_agent.NavAgent.velocity);
            _agent.UpdateNavigationPriority();

            var normSpeed = Mathf.Clamp01(_agent.NavAgent.velocity.magnitude / _agent.Config.MaxSpeed);
            
            _curSpeed = Mathf.Lerp(_curSpeed, normSpeed, 
                _agent.Config.ChangeSpeedTime * Time.deltaTime);
            _agent.AnimatorProvider.SetSpeed(_curSpeed);
        }


        private void Attack() => _context.SwitchState<EnemyAttackState>();
        
        
        private void Wait() => _context.SwitchState<EnemyWaitState>();

        
        private void Stun() => _context.SwitchState<EnemyDamageState>();
    }
}