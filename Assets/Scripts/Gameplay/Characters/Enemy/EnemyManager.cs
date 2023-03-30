using System;
using System.Collections;
using System.Collections.Generic;
using Common.Routine;
using Data;
using Gameplay.Environment.Items;
using Gameplay.Factories;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
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
        private int _dieCount;

        public event Action<int> DieEntityEvent;


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
        

        private  Vector3 GetRandomPosition(Transform targetHero)
        {
            var rndDir = Random.insideUnitSphere * _config.SpawnRadius.Max;
            rndDir.y = 0f;
            if (rndDir.magnitude < _config.SpawnRadius.Min) rndDir = rndDir.normalized * _config.SpawnRadius.Min;
            var spawnPos = targetHero.position + rndDir;
            
            if (NavMesh.SamplePosition(spawnPos, out var hit, _config.SpawnRadius.Max * 2f, 1))
            {
                spawnPos = hit.position;
            }

            return spawnPos;
        }


        private void OnEnemyDieHandler(EnemyAgent enemy)
        {
            SpawnLoot(enemy.transform.position);
            
            enemy.DieEvent -= OnEnemyDieHandler;
            _enemies.Remove(enemy);
            _factory.ReturnToStorage(enemy);

            if (_enemies.Count < _config.MinEnemyCount)
            {
                var count = _config.MinEnemyCount - _enemies.Count;
                RoutineManager.Run(SpawnEnemy(count));
            }

            _dieCount++;
            DieEntityEvent?.Invoke(_dieCount);
        }

        
        private void SpawnLoot(Vector3 pos)
        {
            var loot = _lootFactory.GetItem();
            loot.transform.position = pos + Vector3.up;;
        }


        public void Stop()
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                _enemies[i].StopHunt();
            }
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