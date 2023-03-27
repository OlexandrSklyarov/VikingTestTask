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
        private readonly EnemyData _enemyConfig;

        private const int MIN_ENEMY_COUNT = 10;


        public EnemyManager(EnemyData enemyConfig, ITarget targetHero, EnemyFactory factory, EntityFactory<EnergyItem> lootFactory)
        {
            _enemyConfig = enemyConfig;
            _targetHero = targetHero;
            _factory = factory;
            _lootFactory = lootFactory;

            _enemies = new List<EnemyAgent>();
        }


        public void StartSpawn()
        {
            RoutineManager.Run(SpawnEnemy(MIN_ENEMY_COUNT));
        }

        
        private IEnumerator SpawnEnemy(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var enemy = _factory.Get(EnemyType.SIMPLE_MUTANT);
                enemy.transform.position = GetRandomPosition(_targetHero.MyTransform);
                enemy.Init(_enemyConfig);
                enemy.DieEvent += OnEnemyDieHandler;
                enemy.SetTarget(_targetHero);
                
                _enemies.Add(enemy);
                
                yield return null;
            }
        }
        

        private Vector3 GetRandomPosition(Transform targetHero)
        {
            var offset = Random.insideUnitSphere * 4f;
            if (offset.magnitude < 2f) offset.Scale(Vector3.one * 2f);
            
            var pos = targetHero.position + offset;
            
            pos.y = targetHero.position.y;
            return pos;
        }


        private void OnEnemyDieHandler(EnemyAgent enemy)
        {
            enemy.DieEvent -= OnEnemyDieHandler;
            _enemies.Remove(enemy);
            _factory.ReturnToStorage(enemy);

            if (_enemies.Count < MIN_ENEMY_COUNT)
            {
                var count = MIN_ENEMY_COUNT - _enemies.Count;
                RoutineManager.Run(SpawnEnemy(MIN_ENEMY_COUNT));
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