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
            _agent.CameraController.ActiveCamera(CameraController.CameraType.GAMEPLAY);
            _agent.CameraController.SetGameplayTarget(_agent.Hero.CameraFollowTarget);
            _agent.Hero.DieEvent += OnHeroDieHandler;
        }
        

        public override void OnStop()
        {
        }


        public override void OnFixedUpdate()
        {
            _agent.Hero.OnFixedUpdate();
        }

        
        public override void OnUpdate()
        {
            _agent.Hero.OnUpdate();
        }
        

        private void OnHeroDieHandler()
        {
            _agent.Hero.DieEvent -= OnHeroDieHandler;
            _context.SwitchState<LoseState>();
        }
    }
}