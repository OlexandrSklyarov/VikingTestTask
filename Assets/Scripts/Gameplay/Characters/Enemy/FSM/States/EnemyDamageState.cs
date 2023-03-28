namespace Gameplay.Characters.Enemy.FSM.States
{
    public class EnemyDamageState : BaseEnemyState
    {
        private bool _isDie;
        
        
        public EnemyDamageState(IEnemyContextSwitcher context, IEnemyAgent agent) : base(context, agent)
        {
        }

        public override void OnStart()
        {
            _agent.StunProvider.OnStunnedEvent += Stun;
            
            _isDie = false;
            _agent.Stop();
            Stun();
        }

        
        public override void OnStop()
        {
            _agent.StunProvider.OnStunnedEvent -= Stun;
        }

        private void Stun()
        {
            _agent.AnimatorProvider.PlayDamage();
            
            if (_agent.Health.CurrentHP <= 0)
            {
                _isDie = true;
                _context.SwitchState<EnemyDieState>();
            }
        }


        public override void OnUpdate()
        {
            if (_isDie) return;
            
            if (!_agent.StunProvider.IsStunned())
            {
                _context.SwitchState<EnemyChaseTargetState>();
            }
        }
    }
}