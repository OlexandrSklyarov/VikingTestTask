using Common.Routine;
using Gameplay.Cameras;

namespace Gameplay.Player.FSM.States
{
    public class WaitState : BasePlayerState
    {
        public WaitState(IPlayerContextSwitcher context, IPlayer agent) : base(context, agent)
        {
        }

        
        public override void OnStart()
        {
            StartBattleWithDelay();
        }


        public override void OnStop()
        {
        }
        
        
        
        private void StartBattleWithDelay()
        {
            _agent.CameraController.ActiveCamera(CameraController.CameraType.GAMEPLAY);
            _agent.CameraController.SetGameplayTarget(_agent.Hero.CameraFollowTarget);

            RoutineManager.RunWithDelay
            (
                () => _context.SwitchState<BattleState>(),
                _agent.Config.StarBattleDelay
            );
        }
    }
}