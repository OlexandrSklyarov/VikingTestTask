using Data.DataStruct;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "SO/Enemy/EnemyManagerConfig", fileName = "EnemyManagerConfig")]
    public class EnemyManagerData : ScriptableObject
    {
        [field: SerializeField] public RangeFloatValue SpawnRadius { get; private set; }
        [field: Space(10f), SerializeField, Min(1)] public int MinEnemyCount { get; private set; } = 1;
        [field: Space(10f), SerializeField] public EnemyData Enemy { get; private set; }  
    }
}