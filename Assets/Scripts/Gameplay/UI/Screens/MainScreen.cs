using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.Screens
{
    public class MainScreen : BaseGameScreen
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _exitButton;


        public override void Init(IGameProcessInfo info, IGameUIController controller)
        {
            _playButton.onClick.AddListener(() => controller?.Play());
            _exitButton.onClick.AddListener(() => controller?.Exit());
            
            _playButton.Select();
        }
    }
}