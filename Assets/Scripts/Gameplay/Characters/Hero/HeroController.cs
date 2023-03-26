using System;
using Common.Input;
using Data;
using Gameplay.Characters.Animations;
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
        private CameraLookRotateProvider _cameraLookProvider;
        private InputAdaptor _inputAdaptor;
        private AnimatorProvider _animatorProvider;

        public event Action DieEvent;
        

        public void Init(HeroData config, InputAdaptor inputAdaptor)
        {
            _inputAdaptor = inputAdaptor;
            
            _health = new Health(config.StartHealth);
            _health.ChangeHealthEvent += OnHealthChange;
            _health.HealthZeroEvent += OnHealthIsOver;

            _engine = new RigidbodyEngine
            (
                config.Engine,
                GetComponent<Rigidbody>(),
                _viewBody
            );

            _cameraLookProvider = new CameraLookRotateProvider(config.Camera, _cameraFollowTarget);

            _inputAdaptor.OnMovement += OnMovementHandler;
            _inputAdaptor.OnLook += OnLookHandler;
            _inputAdaptor.OnAttack += OnAttackHandler;

            _animatorProvider = GetComponentInChildren<AnimatorProvider>();
            _animatorProvider.Init();
            _animatorProvider.AttackEvent += OnAttackExecute;
        }

        
        private void OnAttackExecute()
        {
            Debug.Log("Check hit");
        }


        private void OnLookHandler(Vector2 dir)
        {
            _cameraLookProvider?.SetLookRotation(dir, _inputAdaptor.IsCurrentDeviceMouse);
        }


        private void OnMovementHandler(Vector2 dir)
        {
            _engine?.SetDirection(dir);
        }


        private void OnAttackHandler()
        {
            Debug.Log("Attack");
            _animatorProvider.PlayAttack();
            _engine?.SetDirection(Vector2.zero);
        }
        

        private void OnHealthIsOver(int hp)
        {
            Debug.Log($"hp is over{hp}");
            DieEvent?.Invoke();
        }


        private void OnHealthChange(int hp)
        {
            Debug.Log($"hp {hp}");
        }


        void IHero.OnFixedUpdate()
        {
            _engine?.OnFixedUpdate();
        }

        
        void IHero.OnLateUpdate()
        {
            _cameraLookProvider?.OnUpdate();
        }


        void IHero.OnUpdate()
        {
            _engine?.OnUpdate();
            _animatorProvider.SetSpeed(_engine.CurrentSpeed);
        }
    }
}