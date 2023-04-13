using Data;
using UnityEngine;

namespace Gameplay.Characters.Hero
{
    public class RigidbodyEngine
    {
        public float CurrentSpeed => (_inputDirection.sqrMagnitude > 0.01f) ? 1f : 0f;
        public Vector3 Velocity => _rb.velocity;
        
        private readonly Rigidbody _rb;
        private readonly HeroEngine _config;
        private readonly Transform _myTransform;
        private readonly Transform _orientation;

        private Vector3 _inputDirection;
        private Vector3 _smoothVelocity;
        private bool _isGrounded;


        public RigidbodyEngine(HeroEngine config, Rigidbody rb, Transform orientation)
        {
            _config = config;
            _orientation = orientation;
            _rb = rb;
            _myTransform = _rb.transform;
            _rb.freezeRotation = true;
        }

        
        public void SetDirection(Vector2 inputDir) => _inputDirection = inputDir;

        
        public void OnFixedUpdate() => MoveBody();


        public void OnUpdate()
        {
            SpeedLimited();
            GroundCheckProcess();
        }

        
        private void SpeedLimited()
        {
            var curVelocity = _rb.velocity;
            var horVelocity = new Vector3(curVelocity.x, 0f, curVelocity.z);
            
            if (horVelocity.sqrMagnitude <= _config.Speed * _config.Speed) return;

            var limitedVelocity = horVelocity.normalized * _config.Speed;
            _rb.velocity = new Vector3(limitedVelocity.x, curVelocity.y, limitedVelocity.z);
        }


        private void GroundCheckProcess()
        {
             _isGrounded = Physics.Raycast(_myTransform.position, Vector3.down, 
                 _config.GroundCheckDistance, _config.GroundLayerMask);
            
             _rb.drag = (_isGrounded) ? _config.GroundDrag : 0f;
        }

        
        private void MoveBody()
        {
            var viewDir = _orientation.forward * _inputDirection.y + _orientation.right * _inputDirection.x;
           var moveDirection = new Vector3(viewDir.x, 0f, viewDir.z).normalized;
            _rb.AddForce(moveDirection * _config.Speed * _config.SpeedMultiplier, ForceMode.Force);
        }
    }
}