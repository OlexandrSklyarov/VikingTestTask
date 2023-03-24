using System;
using Common.Input;
using Data;
using Gameplay.Cameras;
using Gameplay.Characters.Hero;
using Gameplay.Player;
using Gameplay.UI;
using UnityEngine;

namespace Gameplay
{
    public class GameProcess
    {
        private readonly MainConfig _config;
        private readonly IUIController _uiController;
        private readonly ICameraController _cameraController;
        private readonly PlayerController _playerController;
        private readonly InputAdaptor _inputAdaptor;

        public event Action GameCompletedEvent;
        

        public GameProcess(MainConfig config, IUIController uiController, Transform playerSpawnPoint, 
            ICameraController cameraController)
        {
            _config = config;
            _uiController = uiController;
            _cameraController = cameraController;
            _inputAdaptor = new InputAdaptor();

            _playerController = new PlayerController
            (
                _config.Player,
                SpawnHero(playerSpawnPoint),
                _inputAdaptor
            );

            _playerController.LossEvent += OnHeroLoss;
        }

       
        private HeroController SpawnHero(Transform spawnPoint)
        {
            var hero = UnityEngine.Object.Instantiate
            (
                _config.Hero.HeroPrefab,
                spawnPoint.position,
                spawnPoint.rotation
            );
            
            hero.Init(_config.Hero.Engine, _config.Hero.StartHealth);

            return hero;
        }


        public void OnUpdate()
        {
            _playerController?.OnUpdate();
        }
        
        
        public void OnFixedUpdate()
        {
            _playerController?.OnFixedUpdate();
        }
        

        public void StartProcess()
        {
            _cameraController.ActiveCamera(CameraController.CameraType.STARTUP);
            _playerController?.Enable();
        }
        
        
        public void StopProcess()
        {
            _playerController?.Disable();
        }
        

        private void OnHeroLoss()
        {
            _playerController.LossEvent -= OnHeroLoss;
            GameCompletedEvent?.Invoke();
        }
    }
}
