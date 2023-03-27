using System.Collections.Generic;
using Data;
using Gameplay.Characters.Enemy;
using Services.Pooling;

namespace Gameplay.Factories
{
    public class EnemyFactory : IFactoryStorage<EnemyAgent>
    {
        private readonly Dictionary<EnemyType, EntityFactory<EnemyAgent>> _factories;
        
        public EnemyFactory(EnemyPoolData[] data)
        {
            _factories = new();

            for (int i = 0; i < data.Length; i++)
            {
                var factory = new EntityFactory<EnemyAgent>(data[i].EnemyPrefab, data[i].PoolSize);
                _factories.Add(data[i].EnemyPrefab.Type, factory);
            }
        }

        
        public EnemyAgent Get(EnemyType type)
        {
            return  _factories[type].GetItem();
        }


        public void ReturnToStorage(EnemyAgent item)
        {
            _factories[item.Type].ReturnToStorage(item);
        }
    }
}