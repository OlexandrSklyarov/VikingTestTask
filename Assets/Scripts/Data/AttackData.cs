using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "SO/Character/AttackConfig", fileName = "AttackConfig")]
    public class AttackData : ScriptableObject 
    {
        [field: SerializeField, Min(1)] public int Damage { get; private set; } = 1;        
        [field: SerializeField, Min(0.5f)] public float StunTime { get; private set; } = 2f;        
        [field: SerializeField, Min(0.5f)] public float AttackDelay { get; private set; } = 2f; 
        [field: SerializeField, Min(0.5f)] public float ViewTargetRadius { get; private set; } = 2f; 
        [field: SerializeField] public LayerMask TargetLayerMask { get; private set; }
    }
}