using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.Characters
{
    public class EntityUI : MonoBehaviour
    {
        [SerializeField] private Image _hpBar;


        public void SetHP(float value)
        {
            _hpBar.fillAmount = value;
        }
    }
}
