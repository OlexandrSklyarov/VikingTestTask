using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "SO/Camera/CameraConfig", fileName = "CameraConfig")]
    public class CameraData : ScriptableObject
    {
        [field: SerializeField, Min(0.001f)] public float OverviewRotateSpeed { get; private set; } = 1f;
    }
}