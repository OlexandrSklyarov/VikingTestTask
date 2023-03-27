using Gameplay.Cameras;

namespace Gameplay.Player.FSM.States
{
    public class BattleState : BasePlayerState
    {
        public BattleState(IPlayerContextSwitcher context, IPlayer agent) : base(context, agent)
        {
        }

        public override void OnStart()
        {
            _agent.Hero.DieEvent += OnHeroDieHandler;
        }
        

        public override void OnStop() {}


        public override void OnFixedUpdate() => _agent.Hero.OnFixedUpdate();


        public override void OnUpdate() => _agent.Hero.OnUpdate();
        
        
        public override void OnLateUpdate() => _agent.Hero.OnLateUpdate();
        

        private void OnHeroDieHandler()
        {
            _agent.Hero.DieEvent -= OnHeroDieHandler;
            _context.SwitchState<LoseState>();
        }
    }
}