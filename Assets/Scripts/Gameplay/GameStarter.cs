using Data;
using Gameplay.Cameras;
using Gameplay.Environment.Tags;
using Gameplay.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private MainConfig _mainConfig;
        [SerializeField] private GameUIController _uiController;
        [SerializeField] private Transform _heroSpawnPoint;
        [Space(10f), SerializeField] private CameraController _cameraController;

        private GameProcess _gameProcess;
        private bool _isRunning;

        
        private void OnValidate()
        {
            _uiController ??= FindObjectOfType<GameUIController>();
            _heroSpawnPoint ??= FindObjectOfType<HeroSpawnPointTag>().transform;
        }

        private void Start()
        {
            OnValidate();
            
            _cameraController.Init(_mainConfig.Camera, _heroSpawnPoint);
            
            _gameProcess = new GameProcess
            (
                _mainConfig, 
                _heroSpawnPoint,
                _cameraController
            );
            
            _cameraController.ActiveCamera(CameraController.CameraType.STARTUP);

            _uiController.Init(_gameProcess);
            _uiController.ClickPlayButtonEvent += OnPlayHandler;
            _uiController.ClickRestartButtonEvent += OnRestartHandler;
            _uiController.ClickExitButtonEvent += OnExitHandler;
            _uiController.ShowScreen(ScreenType.MAIN_MENU);

            _gameProcess.GameCompletedEvent += OnGameCompleted;

            if (SaveDataProvider.IsGameRestarted)
            {
                SaveDataProvider.SetGameRestarted(false);
                OnPlayHandler();
            }
        }

        
        private void OnPlayHandler()
        {
            _uiController.ShowScreen(ScreenType.HUD);
            _gameProcess.StartProcess();
            _isRunning = true;
        }

        
        private void OnRestartHandler()
        {
            SaveDataProvider.SetGameRestarted(true);
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }

        
        private void OnExitHandler()
        {
            Application.Quit();
        }


        private void OnGameCompleted()
        {
            _gameProcess.GameCompletedEvent -= OnGameCompleted;
            _gameProcess.StopProcess();
            _isRunning = false;

            _uiController.ShowScreen(ScreenType.LOSS);
        }

        
        private void Update()
        {
            if (!_isRunning) return;
            _gameProcess?.OnUpdate();
        }
        
        
        private void FixedUpdate()
        {
            if (!_isRunning) return;
            _gameProcess?.OnFixedUpdate();
        }
        
        
        private void LateUpdate()
        {
            if (!_isRunning) return;
            _gameProcess?.OnLateUpdate();
        }
        

        private void OnDestroy()
        {
            _gameProcess?.Clear();
        }
    }
}
