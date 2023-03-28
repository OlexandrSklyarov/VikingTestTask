namespace Gameplay.Player.FSM.States
{
    public class LoseState : BasePlayerState
    {
        public LoseState(IPlayerContextSwitcher context, IPlayer agent) : base(context, agent)
        {
        }

        public override void OnStart()
        {
            _agent.Loss();
        }

        public override void OnStop()
        {
        }
    }
}