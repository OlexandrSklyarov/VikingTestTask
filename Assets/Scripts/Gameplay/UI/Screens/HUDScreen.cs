using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.Screens
{
    public class HUDScreen : BaseGameScreen
    {
        [SerializeField] private Image _hpBar;
        [SerializeField] private TextMeshProUGUI _scoreText;
        
        
        public override void Init(IGameProcessInfo info, IGameUIController controller)
        {
            info.HeroChangeHealthEvent += SetHpBar;
            info.ChangeScoreEvent += SetScore;
        }

        
        private void SetScore(int value)
        {
            _scoreText.text = $"Score: {value}";
        }


        private void SetHpBar(float value)
        {
            _hpBar.DOFillAmount(value, 0.5f);
        }
    }
}