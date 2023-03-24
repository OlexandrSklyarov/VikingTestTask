using UnityEngine;

namespace Gameplay.UI
{
    public class GameUIController : MonoBehaviour, IUIController
    {
        public void Init(){}
    
    
        void IUIController.ShowScreen(ScreenType screenType)
        {
            Debug.Log($"Show screen {screenType}");
        }
    }
}
