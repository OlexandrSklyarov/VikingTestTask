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
        private Vector3 _lookDirection;


        public RigidbodyEngine(HeroEngine config, Transform viewBody, Rigidbody rb)
        {
            _config = config;
            _viewBody = viewBody;
            _rb = rb;
            _myTransform = _rb.transform;
            _mainCamera = Camera.main.transform;
        }


        public void SetDirection(Vector2 dir)
        {
            _moveDirection = (dir != Vector2.zero) ? new Vector3(dir.x, 0f, dir.y) : Vector3.zero;
        }


        public void OnFixedUpdate()
        {
            var pos = _myTransform.position;
            _rb.MovePosition(pos + _config.Speed * Time.deltaTime * _moveDirection);
        }


        public void OnUpdate()
        {
            if (_moveDirection == Vector3.zero) return;
            
            var angle = Mathf.Atan2(_moveDirection.x, _moveDirection.z) * Mathf.Rad2Deg +
                        _mainCamera.transform.eulerAngles.y;
            
            _viewBody.rotation = Quaternion.RotateTowards
            (
                _viewBody.rotation, 
                Quaternion.Euler(0f, angle, 0f), 
                _config.AngularSpeed *Time.deltaTime
            );
        }
    }
}