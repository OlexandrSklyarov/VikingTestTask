using System.Collections;
using System.Collections.Generic;
using Common.Routine;
using Data;
using Gameplay.Environment.Items;
using Gameplay.Factories;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Characters.Enemy
{
    public class EnemyManager
    {
        private readonly ITarget _targetHero;
        private readonly EnemyFactory _factory;
        private readonly EntityFactory<EnergyItem> _lootFactory;
        private readonly List<EnemyAgent> _enemies;
        private readonly EnemyManagerData _config;


        public EnemyManager(EnemyManagerData config, ITarget targetHero, EnemyFactory factory, EntityFactory<EnergyItem> lootFactory)
        {
            _config = config;
            _targetHero = targetHero;
            _factory = factory;
            _lootFactory = lootFactory;

            _enemies = new List<EnemyAgent>();
        }


        public void StartSpawn()
        {
            RoutineManager.Run(SpawnEnemy(_config.MinEnemyCount));
        }

        
        private IEnumerator SpawnEnemy(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var enemy = _factory.Get(EnemyType.SIMPLE_MUTANT);
                enemy.transform.position = GetRandomPosition(_targetHero.MyTransform);
                enemy.DieEvent += OnEnemyDieHandler;
                enemy.Init(_config.Enemy);
                enemy.SetTarget(_targetHero);
                
                _enemies.Add(enemy);
                
                yield return null;
            }
        }
        

        private Vector3 GetRandomPosition(Transform targetHero)
        {
            var offset = Random.insideUnitSphere * _config.SpawnRadius.Max;
            if (offset.magnitude < _config.SpawnRadius.Min) offset = offset.normalized * _config.SpawnRadius.Min;
            
            var pos = targetHero.position + offset;
            
            pos.y = targetHero.position.y;
            return pos;
        }


        private void OnEnemyDieHandler(EnemyAgent enemy)
        {
            enemy.DieEvent -= OnEnemyDieHandler;
            _enemies.Remove(enemy);
            _factory.ReturnToStorage(enemy);

            if (_enemies.Count < _config.MinEnemyCount)
            {
                var count = _config.MinEnemyCount - _enemies.Count;
                RoutineManager.Run(SpawnEnemy(count));
            }
        }


        public void Stop()
        {
            
        }
        

        public void OnUpdate()
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                _enemies[i].OnUpdate();
            }
        }
    }
}