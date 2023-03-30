using System;
using Common.Input;
using Data;
using Gameplay.Cameras;
using Gameplay.Characters.Enemy;
using Gameplay.Characters.Hero;
using Gameplay.EntityManager;
using Gameplay.Environment.Items;
using Gameplay.Factories;
using Gameplay.Player;
using UnityEngine;

namespace Gameplay
{
    public class GameProcess : IGameProcessInfo
    {
        private readonly MainConfig _config;
        private readonly ICameraController _cameraController;
        private readonly PlayerController _playerController;
        private readonly EnemyManager _enemyManager;
        private readonly InputAdaptor _inputAdaptor;
        private readonly GameEntityManager _entityManager;

        public event Action<int> ChangeScoreEvent;
        public event Action<float> HeroChangeHealthEvent;
        public event Action GameCompletedEvent;
        

        public GameProcess(MainConfig config, Transform playerSpawnPoint, ICameraController cameraController)
        {
            _config = config;
            _cameraController = cameraController;

            _entityManager = new GameEntityManager();
            
            _inputAdaptor = new InputAdaptor();

            var hero = SpawnHero(playerSpawnPoint, _inputAdaptor);

            _playerController = new PlayerController(_config.Player, hero, cameraController);
            _playerController.HeroHealthChangeEvent += (hpProgress) => HeroChangeHealthEvent?.Invoke(hpProgress);
            _playerController.LossEvent += OnHeroLoss;

            _enemyManager = new EnemyManager(_config.EnemyManager, hero, GetEnemyFactory(), GetEnergyLootFactory());
            _enemyManager.DieEntityEvent += (count) => ChangeScoreEvent?.Invoke(count);
            
            _inputAdaptor.Enable();
        }

        
        private EnemyFactory GetEnemyFactory()
        {
            return new EnemyFactory(_config.Factory.EnemyPoolData);
        }


        private EntityFactory<EnergyItem> GetEnergyLootFactory()
        {
            var data = _config.Factory.EnergyPoolData;
            return new EntityFactory<EnergyItem>(data.EnergyItemPrefab, data.PoolSize);
        }


        private HeroController SpawnHero(Transform spawnPoint, InputAdaptor inputAdaptor)
        {
            var hero = UnityEngine.Object.Instantiate
            (
                _config.HeroPrefab,
                spawnPoint.position,
                spawnPoint.rotation
            );
            
            hero.Init(_config.Hero, inputAdaptor);

            return hero;
        }


        public void OnUpdate()
        {
            _entityManager?.OnUpdate();
            _playerController?.OnUpdate();
            _enemyManager?.OnUpdate();
        }
        
        
        public void OnFixedUpdate()
        {
            _entityManager?.OnFixedUpdate();
            _playerController?.OnFixedUpdate();
        }
        
        
        public void OnLateUpdate()
        {
            _entityManager?.OnLateUpdate();
            _playerController?.OnLateUpdate();
        }
        

        public void StartProcess()
        {
            _playerController?.Enable();
            _enemyManager?.StartSpawn();
        }
        
        
        public void StopProcess()
        {
            _inputAdaptor?.Disable();
            _playerController?.Disable();
            _enemyManager?.Stop();
        }


        public void Clear()
        {
            _entityManager?.Clear();
        }
        

        private void OnHeroLoss()
        {
            Util.Debug.PrintColor($"Game end", Color.red);
            _playerController.LossEvent -= OnHeroLoss;
            GameCompletedEvent?.Invoke();
        }
    }
}
