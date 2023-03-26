
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Gameplay.Player.FSM.States
{
    public class WaitState : BasePlayerState
    {
        private CancellationTokenSource _cts;
        
        
        public WaitState(IPlayerContextSwitcher context, IPlayer agent) : base(context, agent)
        {
        }

        
        public override void OnStart()
        {
            _cts = new CancellationTokenSource();
            
            try
            {
                StartBattleAsync(_cts.Token);
            }
            catch (TaskCanceledException e) {}
        }


        public override void OnStop()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }
        
        
        private async void StartBattleAsync(CancellationToken token)
        {
            await Task.Delay(TimeSpan.FromSeconds(2f), token);
            if (token.IsCancellationRequested) return;
            
            _context.SwitchState<BattleState>();
        }
    }
}