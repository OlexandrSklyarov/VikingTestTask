using Gameplay.EntityManager;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Gameplay.Level.UI
{
    public class CanvasRotatedToCamera : MonoBehaviour, ITickLate
    {
        private Transform _uiCameraTransform;
        private Transform _myTransform;
        
        
        private void OnEnable()
        {
            _uiCameraTransform = Camera.main.gameObject
                .GetComponent<UniversalAdditionalCameraData>()
                .cameraStack[0].transform;
            
            _myTransform = transform;
            GameEntityManager.AddEntity(this, gameObject.GetInstanceID());
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