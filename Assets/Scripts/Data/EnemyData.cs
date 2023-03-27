using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "SO/Enemy/EnemyConfig", fileName = "EnemyConfig")]
    public class EnemyData : ScriptableObject
    {
        [field: SerializeField, Min(1)] public int StartHealth  { get; private set; } = 1;
        [field: SerializeField, Min(0.001f)] public float SpawnDelay  { get; private set; } = 2f;
        [field: SerializeField, Min(0.001f)] public float DeathDelay  { get; private set; } = 2f;
        [field: Space(10f),  SerializeField] public AttackData Attack { get; private set; }
    }
}