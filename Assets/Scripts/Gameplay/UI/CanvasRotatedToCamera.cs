using Gameplay.EntityManager;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Gameplay.Level.UI
{
    public class CanvasRotatedToCamera : MonoBehaviour, ITickLate
    {
        private Transform _uiCameraTransform;
        private Transform _myTransform;
        private Camera _mainCamera;
        private bool _isInit;


        private void OnEnable()
        {
            Init();
            
            _myTransform = transform;
            GameEntityManager.AddEntity(this, gameObject.GetInstanceID());
        }


        private void Init()
        {
            if (_isInit) return;

            _mainCamera = Camera.main;

            GetComponent<Canvas>().worldCamera = _mainCamera;

            _uiCameraTransform = _mainCamera.transform;
            
            _isInit = true;
        }


        private void OnDisable()
        {
            GameEntityManager.RemoveEntity(gameObject.GetInstanceID());
        }
        

        public void OnLateUpdate()
        {
            if (_myTransform == null) return;
            
            _myTransform.LookAt(_myTransform.position + _uiCameraTransform.forward);
        }
    }
}