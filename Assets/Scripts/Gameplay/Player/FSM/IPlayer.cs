using Data;
using Gameplay.Cameras;
using Gameplay.Characters.Hero;

namespace Gameplay.Player.FSM
{
    public interface IPlayer
    {
        PlayerData Config { get; }
        IHero Hero { get; }
        ICameraController CameraController { get; }
        void Loss();
    }
}