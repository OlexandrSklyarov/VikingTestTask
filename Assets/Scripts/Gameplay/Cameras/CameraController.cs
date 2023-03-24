using System;
using System.Collections;
using Cinemachine;
using Common.Routine;
using Data;
using UnityEngine;

namespace Gameplay.Cameras
{
    [Serializable]
    public class CameraController : ICameraController
    {
        public enum CameraType
        {
            STARTUP,
            GAMEPLAY
        }

        [SerializeField] private CinemachineVirtualCamera _startupVC;
        [SerializeField] private CinemachineVirtualCamera _gameplayVC;

        private Transform _startupCameraRoot;
        private Transform _startupCameraLookTarget;
        private Coroutine _overviewRoutine;
        private CameraData _config;
        private bool _overviewProcessRunning;

        public void Init(CameraData config, Transform startupCameraLookTarget)
        {
            _config = config;
            
            _startupCameraLookTarget = startupCameraLookTarget;
            _startupCameraRoot = new GameObject("[CAMERA_STARTUP_ROOT]").transform;
            _startupCameraRoot.transform.position = _startupVC.transform.position;
            
            _startupVC.transform.SetParent(_startupCameraRoot);
            _startupVC.m_LookAt = _startupCameraLookTarget;
        }

        
        public void ActiveCamera(CameraType type)
        {
            ActiveCameraByType(type);

            StopOverviewMap();
            if (type == CameraType.STARTUP) StartCircularOverviewMap();
        }

        private void StopOverviewMap()
        {
            _overviewProcessRunning = false;
            if (_overviewRoutine != null) RoutineManager.Stop(_overviewRoutine);
        }


        private void StartCircularOverviewMap()
        {
            _overviewProcessRunning = true;
            _overviewRoutine = RoutineManager.Run(CircularOverviewMap());
        }

        
        private IEnumerator CircularOverviewMap()
        {
            while (_overviewProcessRunning)
            {
                _startupCameraRoot.RotateAround
                (
                    _startupCameraLookTarget.position, 
                    Vector3.up, 
                    _config.OverviewRotateSpeed * Time.deltaTime
                );
                yield return null;
            }
        }


        private void ActiveCameraByType(CameraType type)
        {
            _startupVC.Priority = (type == CameraType.STARTUP) ? 10 : 0;
            _gameplayVC.Priority = (type == CameraType.GAMEPLAY) ? 10 : 0;
        }
    }
}