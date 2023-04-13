using System;
using Common.Input;
using Data;
using Gameplay.Characters.Animations;
using Gameplay.Characters.Attack;
using UnityEngine;

namespace Gameplay.Characters.Hero
{
    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider), typeof(HeroInteractController))]
    public class HeroController : MonoBehaviour, IHero, IDamage, ITarget, IHeroInteract
    {
        bool IDamage.IsHasHealth => _health.CurrentHP > 0;
        bool ITarget.IsAlive => _isAlive;
        Vector3 IDamage.Position => transform.position;
        Transform ITarget.MyTransform => _myTransform;
        Transform IHero.CameraFollowTarget => _cameraFollowTarget;

        [SerializeField] private Transform _viewBody;
        [SerializeField] private Transform _cameraFollowTarget;
        [SerializeField] private SphereCollider _damageTrigger;
        
        private HeroData _config;
        private Health _health;
        private RigidbodyEngine _engine;
        private CameraLookRotateProvider _cameraLookProvider;
        private InputAdaptor _inputAdaptor;
        private AnimatorProvider _animatorProvider;
        private AttackProvider _attackProvider;
        private HeroViewBodyController _viewBodyController;
        private Transform _myTransform;
        private bool _isInit;
        private bool _isAlive;

        public event Action DieEvent;
        public event Action<float> HealthChangeEvent;
        

        public void Init(HeroData config, InputAdaptor inputAdaptor)
        {
            if (_isInit) return;

            _config = config;
            _inputAdaptor = inputAdaptor;

            _myTransform = transform;
                
            _health = new Health(_config.StartHealth);
            _health.ChangeHealthEvent += OnHealthChange;
            _health.HealthZeroEvent += OnHealthIsOver;

            _engine = new RigidbodyEngine(_config.Engine, GetComponent<Rigidbody>(), _cameraFollowTarget);
            _cameraLookProvider = new CameraLookRotateProvider(_config.Camera, _cameraFollowTarget);
            _viewBodyController = new HeroViewBodyController(_config.View, _viewBody);

            _inputAdaptor.OnMovement += OnMovementHandler;
            _inputAdaptor.OnLook += OnLookHandler;
            _inputAdaptor.OnAttack += OnAttackHandler;

            _animatorProvider = GetComponentInChildren<AnimatorProvider>();
            _animatorProvider.Init();
            _animatorProvider.PlayAlive();
            _animatorProvider.AttackEvent += OnAttackExecute;

            _attackProvider = new AttackProvider(_config.Attack, _damageTrigger);
            
            GetComponent<HeroInteractController>().Init(this);

            _isAlive = true;
            _isInit = true;
        }

        
        private void OnAttackExecute()
        {
            _attackProvider.ApplyDamage();
        }


        private void OnLookHandler(Vector2 dir)
        {
            _cameraLookProvider?.SetLookRotation(dir, _inputAdaptor.IsCurrentDeviceMouse);
        }


        private void OnMovementHandler(Vector2 dir)
        {
            if (_animatorProvider.IsPlayDamage()) 
                ResetDirection();
            else
                _engine?.SetDirection(dir);
        }


        private void OnAttackHandler()
        {
            if (_animatorProvider.IsPlayAttack()) return;

            _attackProvider.StartAttack();
            _animatorProvider.PlayAttack();
            ResetDirection();
            
            _attackProvider.TryFindNearTarget
            (
                _myTransform.position,
                _viewBody.forward,
                (target) => _viewBodyController.RotateViewToTarget(target)
            );
        }
       

        private void ResetDirection() => _engine?.SetDirection(Vector2.zero);


        private void OnHealthIsOver(int hp)
        {
            _animatorProvider.PlayDie();
            _isAlive = false;
            DieEvent?.Invoke();
        }


        private void OnHealthChange(int hp)
        {
            HealthChangeEvent?.Invoke((float)hp / _health.MaxHP);
        }


        void IHero.OnFixedUpdate()
        {
            if (!_isInit) return;
            if (_animatorProvider.IsPlayDamage()) return;
            if (_animatorProvider.IsPlayAttack()) return;

            _engine.OnFixedUpdate();
        }


        void IHero.OnUpdate()
        {
            if (!_isInit) return;
            if (_animatorProvider.IsPlayDamage()) return;
            if (_animatorProvider.IsPlayAttack()) return;

            _engine.OnUpdate();
            _animatorProvider.SetSpeed(_engine.CurrentSpeed);
            _viewBodyController.RotateViewToDirection(_engine.Velocity);
        }

        
        void IHero.OnLateUpdate()
        {
            if (!_isInit) return;
            
            _cameraLookProvider?.OnUpdate();
        }
        

        void IDamage.TryApplyDamage(int damage, float stunTime)
        {
            if (!_isAlive) return;
            
            _health.ApplyDamage(damage);
            _animatorProvider.PlayDamage();
            ResetDirection();
        }
        

        void IHeroInteract.AddHealth(int value)
        {
            _health.Heal(value);
        }
    }
}