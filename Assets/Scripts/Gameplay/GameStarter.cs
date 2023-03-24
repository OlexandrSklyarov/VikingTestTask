using Data;
using Gameplay.Cameras;
using Gameplay.Environment.Tags;
using Gameplay.UI;
using UnityEngine;

namespace Gameplay
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private MainConfig _mainConfig;
        [SerializeField] private GameUIController _uiController;
        [SerializeField] private Transform _heroSpawnPoint;
        [Space(10f), SerializeField] private CameraController _cameraController;

        private GameProcess _gameProcess;

        
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
                _uiController,
                _heroSpawnPoint,
                _cameraController
            );

            _gameProcess.StartProcess();
        }

        private void Update()
        {
            _gameProcess?.OnUpdate();
        }
    }
}
