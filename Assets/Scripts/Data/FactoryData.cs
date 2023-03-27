using System;
using Gameplay.Characters.Enemy;
using Gameplay.Environment.Items;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "SO/Factory/FactoryConfig", fileName = "FactoryConfig")]
    public class FactoryData : ScriptableObject
    {
        [field: SerializeField] public EnemyPoolData[] EnemyPoolData { get; private set; }
        [field: Space(20f), SerializeField] public EnergyPoolData EnergyPoolData { get; private set; }
    }


    [Serializable]
    public class EnemyPoolData
    {
        [field: SerializeField] public EnemyAgent EnemyPrefab { get; private set; }
        [field: SerializeField, Min(1)] public int PoolSize { get; private set; } = 16;
    }
    
    
    [Serializable]
    public class EnergyPoolData
    {
        [field: SerializeField] public EnergyItem EnergyItemPrefab { get; private set; }
        [field: SerializeField, Min(1)] public int PoolSize { get; private set; } = 16;
    }
}