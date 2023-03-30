using UnityEngine;

namespace Gameplay.UI.Screens
{
    public abstract class BaseGameScreen : MonoBehaviour
    {
        public ScreenType Type => _type;
        
        [SerializeField] private ScreenType _type;
        [Space(10f), SerializeField] private GameObject _RootElements;

        public abstract void Init(IGameProcessInfo info, IGameUIController controller);
        
        
        public void Show() => _RootElements.SetActive(true);
        
        
        public void Hide() => _RootElements.SetActive(false);
    }
}