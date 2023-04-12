using Data;
using UnityEngine;

namespace Gameplay.Characters.Hero
{
    public class CameraLookRotateProvider
    {
        private readonly HeroCamera _config;
        private readonly Transform _cameraFollowTarget;
        private float _cameraTargetYaw;
        private float _cameraTargetPitch;
        private Vector2 _lookDirection;
        private bool _isCurrentDeviceMouse;
        

        public CameraLookRotateProvider(HeroCamera config, Transform cameraFollowTarget)
        {
            _config = config;
            _cameraFollowTarget = cameraFollowTarget;
        }
        
        public void SetLookRotation(Vector2 lookDir, bool isCurrentDeviceMouse = true)
        {
            _lookDirection = lookDir;
            _isCurrentDeviceMouse = isCurrentDeviceMouse;
        }
        

        public void OnUpdate()
        {
            if (_lookDirection.sqrMagnitude >= _config.LookDeltaThreshold)
            {
                float deltaTimeMultiplier = _isCurrentDeviceMouse ? 1.0f : Time.deltaTime;

                _cameraTargetYaw += _lookDirection.x * deltaTimeMultiplier;
                _cameraTargetPitch += _lookDirection.y * deltaTimeMultiplier;
            }

            _cameraTargetYaw = ClampAngle(_cameraTargetYaw, float.MinValue, float.MaxValue);
            _cameraTargetPitch = ClampAngle(_cameraTargetPitch, _config.BottomClamp, _config.TopClamp);

            _cameraFollowTarget.rotation = Quaternion.Euler(_cameraTargetPitch, _cameraTargetYaw, 0.0f);
        }

        
        private float ClampAngle(float curAngle, float minValue, float maxValue)
        {
            if (curAngle < -360f) curAngle += 360f;
            if (curAngle > 360f) curAngle -= 360f;
            return Mathf.Clamp(curAngle, minValue, maxValue);
        }
    }
}