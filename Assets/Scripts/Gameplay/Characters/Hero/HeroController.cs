using System;
using Common.Input;
using Data;
using UnityEngine;

namespace Gameplay.Characters.Hero
{
    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
    public class HeroController : MonoBehaviour, IHero
    {
        Transform IHero.CameraFollowTarget => _cameraFollowTarget;

        [SerializeField] private Transform _viewBody;
        [SerializeField] private Transform _cameraFollowTarget;
        
        private Health _health;
        private RigidbodyEngine _engine;
        
        public event Action DieEvent;
        

        public void Init(HeroEngine engineConfig, InputAdaptor inputAdaptor, int startHealth)
        {
            _health = new Health(startHealth);
            _health.ChangeHealthEvent += OnHealthChange;
            _health.HealthZeroEvent += OnHealthIsOver;

            _engine = new RigidbodyEngine
            (
                engineConfig,
                _viewBody,
                GetComponent<Rigidbody>()
            );

            inputAdaptor.OnMovement += OnMovementHandler;
            inputAdaptor.OnLook += OnLookHandler;
            inputAdaptor.OnAttack += OnAttackHandler;
        }

        
        private void OnLookHandler(Vector2 dir)
        {
            
        }


        private void OnMovementHandler(Vector2 dir)
        {
            _engine?.SetDirection(dir);
        }


        private void OnAttackHandler()
        {
            Debug.Log("Attack");
        }
        

        private void OnHealthIsOver(int hp)
        {
            Debug.Log($"hp is over{hp}");
        }


        private void OnHealthChange(int hp)
        {
            Debug.Log($"hp {hp}");
        }


        void IHero.OnFixedUpdate()
        {
            _engine?.OnFixedUpdate();
        }
        

        void IHero.OnUpdate()
        {
            _engine?.OnUpdate();
        }
    }
}