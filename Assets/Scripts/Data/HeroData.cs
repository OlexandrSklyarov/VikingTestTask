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
        [field: Space(10f),  SerializeField] public AttackData Attack { get; private set; }

        }

    [Serializable]
    public class HeroEngine
    {
        [field: SerializeField, Min(0.001f)] public float Speed  { get; private set; } = 5f;
        [field: SerializeField, Min(0.001f)] public float AngularSpeed  { get; private set; } = 360f;
        [field: SerializeField, Min(0.001f)] public float SmoothMovementTime  { get; private set; } = 0.1f;
    }
    
    [Serializable]
        public class HeroCamera
        {
            [field: SerializeField, Min(0.01f)] public float LookDeltaThreshold { get; private set; } = 0.01f;
            [field: SerializeField, Range(-360f, 360f)] public float BottomClamp  { get; private set; } = 30f;
            [field: SerializeField, Range(-360f, 360f)] public float TopClamp  { get; private set; } = 70f;
            [field: SerializeField, Range(0f, 360f)] public float AngleOverride  { get; private set; }
        }
}