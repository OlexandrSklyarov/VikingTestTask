using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.Characters
{
    public class EntityUI : MonoBehaviour
    {
        [SerializeField] private GameObject _rootElements;
        [SerializeField] private Image _hpBar;
        [SerializeField] private TextMeshProUGUI _info;


        public void SetHP(float value)
        {
            _hpBar.fillAmount = value;
        }


        public void SetInfo(string msg) => _info.text = $"{msg}";
        

        public void Show() => _rootElements.SetActive(true);

        
        public void Hide() => _rootElements.SetActive(false);
    }
}
