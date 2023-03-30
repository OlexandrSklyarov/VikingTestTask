using System;

namespace Gameplay
{
    public interface IGameProcessInfo
    {
        event Action<int> ChangeScoreEvent;
        event Action<float> HeroChangeHealthEvent;
    }
}