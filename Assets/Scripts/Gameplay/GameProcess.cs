using Data;
using Gameplay.Cameras;
using Gameplay.UI;
using UnityEngine;

namespace Gameplay
{
    public class GameProcess
    {
        private readonly IUIController _uiController;
        private readonly ICameraController _cameraController;
        private readonly MainConfig _config;

        public GameProcess(MainConfig config, IUIController uiController, Transform playerSpawnPoint, 
            ICameraController cameraController)
        {
            _config = config;
            _uiController = uiController;
            _cameraController = cameraController;
        }

        
        public void OnUpdate()
        {
        }

        public void StartProcess()
        {
            _cameraController.ActiveCamera(CameraController.CameraType.STURTUP);
        }
        
        
        public void StopProcess()
        {
            
        }
    }
}
