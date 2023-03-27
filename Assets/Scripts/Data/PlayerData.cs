using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "SO/Player/PlayerConfig", fileName = "PlayerConfig")]
    public class PlayerData : ScriptableObject
    {
        [field: SerializeField, Min(0.001f)] public float StarBattleDelay  { get; private set; } = 3f;
    }
}