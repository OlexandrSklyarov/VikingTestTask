using Data;
using UnityEngine;

namespace Gameplay.Characters.Hero
{
    public class HeroViewBodyController
    {
        private readonly Transform _viewBody;
        private readonly HeroView _config;


        public HeroViewBodyController(HeroView config, Transform viewBody)
        {
            _config = config;
            _viewBody = viewBody;
        }
        
        
        public void RotateViewToTarget(Vector3 lookTarget)
        {
            var dir = lookTarget - _viewBody.position;
            _viewBody.rotation = Util.Vector3Math.DirToQuaternion(dir);
        }


        public void RotateViewToDirection(Vector3 velocity)
        {
            if (velocity.sqrMagnitude <= _config.MinVelocityThreshold * _config.MinVelocityThreshold) return;
                
            var angle = Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;
                
            _viewBody.rotation = Quaternion.RotateTowards
            (
                _viewBody.rotation, 
                Quaternion.Euler(0f, angle, 0f), 
                _config.AngularSpeed *Time.deltaTime
            ); 
        }
    }
}