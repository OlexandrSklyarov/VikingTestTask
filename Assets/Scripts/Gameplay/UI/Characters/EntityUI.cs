using System;
using Gameplay.Characters.Enemy.FSM;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.Characters
{
    public class EntityUI : MonoBehaviour
    {
        [SerializeField] private GameObject _rootElements;
        [SerializeField] private Image _hpBar;
        [SerializeField] private TextMeshProUGUI _stateInfo;


        public void SetHP(float value)
        {
            _hpBar.fillAmount = value;
        }


        public void SetState(BaseEnemyState state)
        {
            _stateInfo.text = (state != null) ? $"{state}" : string.Empty;
        }
        

        public void Show() => _rootElements.SetActive(true);

        
        public void Hide() => _rootElements.SetActive(false);
    }
}
