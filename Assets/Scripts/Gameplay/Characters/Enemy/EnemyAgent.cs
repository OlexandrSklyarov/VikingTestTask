using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Gameplay.Characters.Animations;
using Gameplay.Characters.Attack;
using Gameplay.Characters.Enemy.FSM;
using Gameplay.Characters.Enemy.FSM.States;
using Gameplay.UI.Characters;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Gameplay.Characters.Enemy
{
    [RequireComponent(typeof(NavMeshAgent),  typeof(CapsuleCollider), typeof(Rigidbody))]
    public class EnemyAgent : MonoBehaviour, IDamage, IEnemyAgent, IEnemyContextSwitcher
    {
        public EnemyType Type => _type;
        float IEnemyAgent.AttackRange => _navAgent.radius * 4f;
        Vector3 IDamage.Position => transform.position;
        ITarget IEnemyAgent.MyTarget => _myTarget;
        NavMeshAgent IEnemyAgent.NavAgent => _navAgent;
        StunProvider IEnemyAgent.StunProvider => _stunProvider;
        Health IEnemyAgent.Health => _health;
        EnemyData IEnemyAgent.Config => _config;
        AnimatorProvider IEnemyAgent.AnimatorProvider => _animatorProvider;
        AttackProvider IEnemyAgent.AttackProvider => _attackProvider;

        [SerializeField] private EnemyType _type;
        [SerializeField] private Transform _viewBody;
        [SerializeField] private SphereCollider _damageTrigger;
        [SerializeField] private EntityUI _entityUI;

        private EnemyData _config;
        private NavMeshAgent _navAgent;
        private AnimatorProvider _animatorProvider;
        private AttackProvider _attackProvider;
        private Health _health;
        private ITarget _myTarget;
        private StunProvider _stunProvider;
        private List<BaseEnemyState> _allStates;
        private BaseEnemyState _currentState;
        private int _generation;
        private bool _isInit;
        private CapsuleCollider _collider;

        public event Action<EnemyAgent> DieEvent;


        public void Init(EnemyData config)
        {
            if (!_isInit)
            {
                _config = config;
                _navAgent = GetComponent<NavMeshAgent>();

                GetComponent<Rigidbody>().isKinematic = true;

                _collider = GetComponent<CapsuleCollider>();
                
                _animatorProvider = GetComponentInChildren<AnimatorProvider>();
                _animatorProvider.Init();

                _health = new Health(_config.StartHealth);
                _health.ChangeHealthEvent += OnHealthChange;

                _stunProvider = new StunProvider();

                _attackProvider = new AttackProvider(_config.Attack, _damageTrigger);
                
                _allStates =  new List<BaseEnemyState>()
                {
                    new EnemySpawnState(this, this),
                    new EnemyWaitState(this, this),
                    new EnemyChaseTargetState(this, this),
                    new EnemyAttackState(this, this),
                    new EnemyDamageState(this, this),
                    new EnemyDieState(this, this)
                };
            }
            
            _animatorProvider.PlayAlive();

            _entityUI.Show();
            
            _collider.enabled = true;
                
            _health.Reset(_config.StartHealth + _generation);
            
            _generation++;
            
            _navAgent.enabled = true;
            _navAgent.Warp(transform.position);
            _navAgent.avoidancePriority = 50 + Random.Range(1, 49); // 99 max
            
            _currentState = _allStates[0];

            _isInit = true;
        }

        
        private void OnHealthChange(int hp)
        {
            _entityUI.SetHP((float)hp / _health.MaxHP);
        }
        

        public void SetTarget(ITarget target)
        {
            _myTarget = target;
            SwitchState<EnemySpawnState>();
        }


        public void StopHunt()
        {
            _animatorProvider.SetSpeed(0f);
            _currentState?.OnStop();
            _currentState = null;
            _navAgent.enabled = false;
        }
        
        
        void IEnemyAgent.Die()
        {
            DieEvent?.Invoke(this);
        }
        
        
        void IEnemyAgent. PrepareForDie()
        {
            _entityUI.Hide();
            _collider.enabled = false;
            StopHunt();
        }

        
        void IEnemyAgent.RotateViewToTarget(Vector3 lookTarget)
        {
            var dir = lookTarget - transform.position;
            _viewBody.rotation = Util.Vector3Math.DirToQuaternion(dir);
        }
        
        
        void IEnemyAgent.RotateViewToDirection(Vector3 dir)
        {
            _viewBody.rotation = Util.Vector3Math.DirToQuaternion(dir);
        }
        

        void IDamage.TryApplyDamage(int damage, float stunTime)
        {
            _health.ApplyDamage(damage);
            _stunProvider.SetStun(stunTime);
        }


        void IEnemyAgent.Stop()
        {
            if (_navAgent.enabled)
                _navAgent.SetDestination(_navAgent.transform.position);
            
            _animatorProvider.SetSpeed(0f);
        }

        
        public void SwitchState<T>() where T : BaseEnemyState
        {
            var state = _allStates.FirstOrDefault(s => s is T);
            
            _currentState?.OnStop();
            _currentState = state;
            _currentState?.OnStart();            
        }

        
        public void OnUpdate()
        {
            _currentState?.OnUpdate();
        }
    }
}