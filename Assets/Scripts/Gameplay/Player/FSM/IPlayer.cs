using Gameplay.Cameras;
using Gameplay.Characters.Hero;

namespace Gameplay.Player.FSM
{
    public interface IPlayer
    {
        IHero Hero { get; }
        ICameraController CameraController { get; }
        void Loss();
    }
}