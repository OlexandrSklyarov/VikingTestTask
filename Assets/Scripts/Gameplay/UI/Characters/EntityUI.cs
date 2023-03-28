using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.Characters
{
    public class EntityUI : MonoBehaviour
    {
        [SerializeField] private GameObject _rootElements;
        [SerializeField] private Image _hpBar;


        public void SetHP(float value)
        {
            _hpBar.fillAmount = value;
        }
        

        public void Show() => _rootElements.SetActive(true);

        
        public void Hide() => _rootElements.SetActive(false);
    }
}
