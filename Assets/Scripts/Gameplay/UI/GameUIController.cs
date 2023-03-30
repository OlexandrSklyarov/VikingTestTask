using System;
using System.Linq;
using Gameplay.UI.Screens;
using UnityEngine;

namespace Gameplay.UI
{
    public class GameUIController : MonoBehaviour, IGameUIController
    {
        private BaseGameScreen[] _allScreens;
        
        public event Action ClickPlayButtonEvent;
        public event Action ClickRestartButtonEvent;
        public event Action ClickExitButtonEvent;
        
        
        public void Init(IGameProcessInfo gameInfo)
        {
            _allScreens = GetComponentsInChildren<BaseGameScreen>();
            Array.ForEach(_allScreens, s => s.Init(gameInfo, this));
        }
    
    
        public void ShowScreen(ScreenType screenType)
        {
            Array.ForEach(_allScreens, s => s.Hide());
            var screen = _allScreens.FirstOrDefault(s => s.Type == screenType);
            
            if (screen == null) return;
            
            screen.Show();
        }
        

        void IGameUIController.Play() => ClickPlayButtonEvent?.Invoke();
        

        void IGameUIController.Restart() => ClickRestartButtonEvent?.Invoke();
        

        void IGameUIController.Exit() => ClickExitButtonEvent?.Invoke();
    }
}
