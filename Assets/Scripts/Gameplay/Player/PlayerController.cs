using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Gameplay.Cameras;
using Gameplay.Characters.Hero;
using Gameplay.Player.FSM;
using Gameplay.Player.FSM.States;

namespace Gameplay.Player
{
    public class PlayerController : IPlayer, IPlayerContextSwitcher
    {
        PlayerData IPlayer.Config => _config;
        IHero IPlayer.Hero => _hero;
        ICameraController IPlayer.CameraController => _cameraController;

        private readonly PlayerData _config;
        private readonly HeroController _hero;
        private readonly ICameraController _cameraController;
        private readonly List<BasePlayerState> _allStates;
        private BasePlayerState _currentState;
        private bool _isActive;

        public event Action LossEvent;


        public PlayerController(PlayerData config, HeroController hero, ICameraController cameraController)
        {
            _config = config;
            _hero = hero;
            _cameraController = cameraController;
            
            _allStates =  new List<BasePlayerState>()
            {
                new WaitState(this, this),
                new BattleState(this, this),
                new LoseState(this, this)
            };

            _currentState = _allStates[0];
        }


        public void Enable()
        {
            if (_isActive) return;

            _currentState?.OnStart(); 
            _isActive = true;
        }


        public void Disable()
        {
            if (!_isActive) return;

            _currentState?.OnStop(); 
            _isActive = false;
        }


        public void SwitchState<T>() where T : BasePlayerState
        {
            var state = _allStates.FirstOrDefault(s => s is T);

            _currentState?.OnStop();
            _currentState = state;
            _currentState?.OnStart();
        }


        public void OnUpdate()
        {
            if (!_isActive) return;

            _currentState?.OnUpdate();
        }


        public void OnFixedUpdate()
        {
            if (!_isActive) return;
            
            _currentState?.OnFixedUpdate();
        }
        
        
        public void OnLateUpdate()
        {
            if (!_isActive) return;

            _currentState?.OnLateUpdate();
        }
        

        void IPlayer.Loss() => LossEvent?.Invoke();
    }
}