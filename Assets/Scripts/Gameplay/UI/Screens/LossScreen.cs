using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Gameplay.UI.Screens
{
    public class LossScreen : BaseGameScreen
    {
        [SerializeField] private Button _restertButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private TextMeshProUGUI _scoreText;
        
        
        public override void Init(IGameProcessInfo info, IGameUIController controller)
        {
            info.ChangeScoreEvent += SetScore;
            
            _restertButton.onClick.AddListener(() => controller?.Restart());
            _exitButton.onClick.AddListener(() => controller?.Exit());

            _restertButton.Select();
        }
        
        
        private void SetScore(int value)
        {
            _scoreText.text = $"Score: {value}";
        }
    }
}