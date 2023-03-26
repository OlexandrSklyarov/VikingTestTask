namespace Gameplay.Player.FSM.States
{
    public class WaitState : BasePlayerState
    {
        public WaitState(IPlayerContextSwitcher context, IPlayer agent) : base(context, agent)
        {
        }

        
        public override void OnStart()
        {
            StartBattle();
        }


        public override void OnStop()
        {
        }
        
        
        private void StartBattle()
        {
            _context.SwitchState<BattleState>();
        }
    }
}