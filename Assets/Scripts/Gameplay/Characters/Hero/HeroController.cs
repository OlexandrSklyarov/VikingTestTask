using System;
using Common.Input;
using Data;
using Gameplay.Characters.Animations;
using Gameplay.Characters.Attack;
using UnityEngine;

namespace Gameplay.Characters.Hero
{
    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
    public class HeroController : MonoBehaviour, IHero, IDamage
    {
        Transform IHero.CameraFollowTarget => _cameraFollowTarget;

        [SerializeField] private Transform _viewBody;
        [SerializeField] private Transform _cameraFollowTarget;
        [SerializeField] private SphereCollider _damageTrigger;
        
        private Health _health;
        private RigidbodyEngine _engine;
        private CameraLookRotateProvider _cameraLookProvider;
        private InputAdaptor _inputAdaptor;
        private AnimatorProvider _animatorProvider;
        private AttackProvider _attackProvider;
        private StunProvider _stunProvider;

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

            _attackProvider = new AttackProvider(config.Attack, _damageTrigger);

            _stunProvider = new StunProvider();
        }

        
        private void OnAttackExecute()
        {
            Debug.Log("Check hit");
            _attackProvider.ApplyDamage();
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
            if (!_attackProvider.IsCanAttack()) return;

            _attackProvider.StartAttack();
            _animatorProvider.PlayAttack();
            ResetDirection();
        }


        private void ResetDirection() => _engine?.SetDirection(Vector2.zero);


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
            if (_stunProvider.IsStunned()) return;

            _engine?.OnFixedUpdate();
        }


        void IHero.OnUpdate()
        {
            if (_stunProvider.IsStunned()) return;

            _engine?.OnUpdate();
            _animatorProvider.SetSpeed(_engine.CurrentSpeed);
        }

        
        void IHero.OnLateUpdate()
        {
            _cameraLookProvider?.OnUpdate();
        }
        

        void IDamage.TryApplyDamage(int damage, float stunTime)
        {
            _health.ApplyDamage(damage);
            _animatorProvider.PlayDamage();
            ResetDirection();

            _stunProvider.SetStun(stunTime);
        }        
    }
}