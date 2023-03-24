using System;
using Gameplay.Characters.Hero;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "SO/HeroData/HeroConfig", fileName = "HeroConfig")]
    public class HeroData : ScriptableObject
    {
        [field: SerializeField] public HeroController HeroPrefab { get; private set; }
        [field: Space(10f), SerializeField, Min(1)] public int StartHealth { get; private set; } = 20;
        [field: Space(10f), SerializeField] public HeroEngine Engine { get; private set; }
    }

    [Serializable]
    public class HeroEngine
    {
        [field: SerializeField, Min(0.001f)] public float Speed  { get; set; } = 5f;
        [field: SerializeField, Min(0.001f)] public float AngularSpeed  { get; set; } = 360f;
    }
}