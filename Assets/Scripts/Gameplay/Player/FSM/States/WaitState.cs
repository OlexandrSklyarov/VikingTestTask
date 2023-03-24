namespace Gameplay.Player.FSM.States
{
    public class WaitState : BasePlayerState
    {
        public WaitState(IPlayerContextSwitcher context, IPlayer agent) : base(context, agent)
        {
        }

        
        public override void OnStart()
        {
        }

        public override void OnStop()
        {
        }
    }
}