using System;
using System.Threading;
using System.Threading.Tasks;
using Gameplay.Cameras;

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
            _agent.CameraController.ActiveCamera(CameraController.CameraType.GAMEPLAY);
            _agent.CameraController.SetGameplayTarget(_agent.Hero.CameraFollowTarget);
            
            await Task.Delay(TimeSpan.FromSeconds(_agent.Config.StarBattleDelay), token);
            if (token.IsCancellationRequested) return;
            
            _context.SwitchState<BattleState>();
        }
    }
}