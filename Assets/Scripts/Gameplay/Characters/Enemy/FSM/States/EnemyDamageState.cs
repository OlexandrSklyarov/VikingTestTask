namespace Gameplay.Characters.Enemy.FSM.States
{
    public class EnemyDamageState : BaseEnemyState
    {
        public EnemyDamageState(IEnemyContextSwitcher context, IEnemyAgent agent) : base(context, agent)
        {
        }

        public override void OnStart()
        {
            _agent.Stop();
            _agent.AnimatorProvider.PlayDamage();
        }

        
        public override void OnStop()
        {
        }


        public override void OnUpdate()
        {
            if (_agent.Health.CurrentHP <= 0)
            {
                _context.SwitchState<EnemyDieState>();
            }
            else if (!_agent.StunProvider.IsStunned())
            {
                _context.SwitchState<EnemyChaseTargetState>();
            }
        }
    }
}