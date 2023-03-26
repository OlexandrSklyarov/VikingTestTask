using Data;
using UnityEngine;

namespace Gameplay.Characters.Hero
{
    public class RigidbodyEngine
    {
        private readonly Rigidbody _rb;
        private readonly HeroEngine _config;
        private readonly Transform _myTransform;
        private readonly Transform _mainCamera;
        private readonly Transform _viewBody;

        private Vector3 _moveDirection;
        private Vector3 _smoothVelocity;


        public RigidbodyEngine(HeroEngine config, Rigidbody rb, Transform viewBody)
        {
            _config = config;
            _rb = rb;
            _viewBody = viewBody;
            _myTransform = _rb.transform;
            _mainCamera = Camera.main.transform;
        }

        public float CurrentSpeed => (_moveDirection.sqrMagnitude > 0.01f) ? 1f : 0f;


        public void SetDirection(Vector2 inputDir)
        {
            var dir = (inputDir != Vector2.zero) ? new Vector3(inputDir.x, 0f, inputDir.y) : Vector3.zero;
            _moveDirection = _mainCamera.TransformDirection(dir);
        }

        
        public void OnFixedUpdate() => MoveBody();


        public void OnUpdate() => RotateBody();

        private void MoveBody()
        {
            var pos = _myTransform.position;
            var targetPos = pos + _config.Speed * Time.deltaTime * _moveDirection;
            
            _rb.MovePosition(Vector3.SmoothDamp
            (
                pos,
                targetPos,
                ref _smoothVelocity,
                _config.SmoothMovementTime
            ));
        }
        
        
        private void RotateBody()
        {
            if (_moveDirection == Vector3.zero) return;

            var angle = Mathf.Atan2(_moveDirection.x, _moveDirection.z) * Mathf.Rad2Deg;
            
            _viewBody.rotation = Quaternion.RotateTowards
            (
                _viewBody.rotation, 
                Quaternion.Euler(0f, angle, 0f), 
                _config.AngularSpeed *Time.deltaTime
            );
        }
    }
}