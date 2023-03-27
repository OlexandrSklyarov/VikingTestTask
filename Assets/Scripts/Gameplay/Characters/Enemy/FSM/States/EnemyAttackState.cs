using System;
using UnityEngine;

namespace Gameplay.Characters.Enemy.FSM.States
{
    public class EnemyAttackState : BaseEnemyState
    {
        private readonly float _minSqAttackDistance;
        

        public EnemyAttackState(IEnemyContextSwitcher context, IEnemyAgent agent) : base(context, agent)
        {
            var attackDistance = _agent.NavAgent.radius * 2f;
            _minSqAttackDistance = attackDistance * attackDistance;
        }

        public override void OnStart()
        {
            _agent.NavAgent.SetDestination(_agent.NavAgent.transform.position);

            if (_agent.MyTarget == null) Wait();
        }
        

        public override void OnStop()
        {
        }


        public override void OnUpdate()
        {
            if (_agent.MyTarget == null || !_agent.MyTarget.IsAlive || IsTargetFar()) Wait();
            
            if (_agent.AttackProvider.IsCanAttack())
            {
                _agent.RotateViewToTarget(_agent.MyTarget.MyTransform.position);
                _agent.AnimatorProvider.PlayAttack();
                _agent.AttackProvider.StartAttack();
            }
            
        }

        
        private bool IsTargetFar()
        {
            var sqDist = (_agent.NavAgent.transform.position - _agent.MyTarget.MyTransform.position).sqrMagnitude;
            return sqDist > _minSqAttackDistance;
        }


        private void Wait() => _context.SwitchState<EnemyWaitState>();
    }
}