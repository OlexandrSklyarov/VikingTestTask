using System;
using UnityEngine;

namespace Gameplay.Characters.Hero
{
    public interface IHero
    {
        event Action DieEvent;
        Transform CameraFollowTarget { get; }
        public void OnUpdate();
        public void OnFixedUpdate();
    }
}