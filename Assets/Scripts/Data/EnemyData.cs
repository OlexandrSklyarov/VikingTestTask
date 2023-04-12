using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "SO/Enemy/EnemyConfig", fileName = "EnemyConfig")]
    public class EnemyData : ScriptableObject
    {
        [field: SerializeField, Min(1)] public int StartHealth { get; private set; } = 1;
        [field: SerializeField, Min(0.001f)] public float SpawnDelay { get; private set; } = 2f;
        [field: SerializeField, Min(0.001f)] public float DeathDelay { get; private set; } = 2f;
        [field: SerializeField, Min(0.001f)] public float RotateViewSpeed { get; private set; } = 60f;
        [field: SerializeField, Min(0.001f)] public float ChangeSpeedTime { get; private set; } = 30f;
        [field: SerializeField, Min(0.001f)] public float MinVelocity { get; private set; } = 0.3f;
        [field: SerializeField, Min(0.001f)] public float MaxSpeed { get; private set; } = 3f;
        [field: SerializeField, Min(0.001f)] public float MaxAngularSpeed { get; private set; } = 120f;
        [field: SerializeField, Range(1, 99)] public int MaxAgentPriority { get; private set; } = 20;
        [field: Space(10f), SerializeField] public AttackData Attack { get; private set; }
    }
}