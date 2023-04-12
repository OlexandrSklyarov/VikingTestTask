using System;
using Gameplay.Characters.Hero;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "SO/HeroData/HeroConfig", fileName = "HeroConfig")]
    public class HeroData : ScriptableObject
    {
        [field: Space(10f), SerializeField, Min(1)] public int StartHealth { get; private set; } = 20;
        [field: Space(10f), SerializeField] public HeroEngine Engine { get; private set; }    
        [field: Space(10f), SerializeField] public HeroCamera Camera { get; private set; }
        [field: Space(10f), SerializeField] public HeroView View { get; private set; }
        [field: Space(10f),  SerializeField] public AttackData Attack { get; private set; }
    }

    [Serializable]
    public class HeroEngine
    {
        [field: SerializeField, Min(0.001f)] public float Speed  { get; private set; } = 5f;
        [field: SerializeField, Min(0.001f)] public float SpeedMultiplier  { get; private set; } = 10f;
        [field: SerializeField, Min(0.001f)] public float SmoothMovementTime  { get; private set; } = 0.1f;
        [field: SerializeField, Min(0.001f)] public float GroundDrag  { get; private set; } = 5f;
        [field: SerializeField, Min(0.001f)] public float GroundCheckDistance  { get; private set; } = 0.2f;
        [field: SerializeField] public LayerMask GroundLayerMask { get; private set; }
    }
    
    [Serializable]
    public class HeroCamera
    {
        [field: SerializeField, Min(0.01f)] public float LookDeltaThreshold { get; private set; } = 0.01f;
        [field: SerializeField, Range(-360f, 360f)] public float BottomClamp  { get; private set; } = 30f;
        [field: SerializeField, Range(-360f, 360f)] public float TopClamp  { get; private set; } = 70f;
    }
    
    
    [Serializable]
    public class HeroView
    {
        [field: SerializeField, Min(0.001f)] public float AngularSpeed  { get; private set; } = 360f;
        [field: SerializeField, Min(0.001f)] public float MinVelocityThreshold  { get; private set; } = 0.2f;
    }
}