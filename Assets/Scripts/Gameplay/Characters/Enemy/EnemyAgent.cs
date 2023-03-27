using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Gameplay.Characters.Animations;
using Gameplay.Characters.Attack;
using Gameplay.Characters.Enemy.FSM;
using Gameplay.Characters.Enemy.FSM.States;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Gameplay.Characters.Enemy
{
    [RequireComponent(typeof(NavMeshAgent),  typeof(CapsuleCollider))]
    public class EnemyAgent : MonoBehaviour, IDamage, IEnemyAgent, IEnemyContextSwitcher
    {
        public EnemyType Type => _type;
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

        public event Action<EnemyAgent> DieEvent;


        public void Init(EnemyData config)
        {
            if (!_isInit)
            {
                _config = config;
                _navAgent = GetComponent<NavMeshAgent>();
                
                _animatorProvider = GetComponentInChildren<AnimatorProvider>();
                
                _health = new Health(_config.StartHealth);
                _health.ChangeHealthEvent += OnHealthChange;

                _stunProvider = new StunProvider();

                _attackProvider = new AttackProvider(_config.Attack, _damageTrigger);
                
                _allStates =  new List<BaseEnemyState>()
                {
                    new EnemySpawnState(this, this),
                    new EnemyWaitState(this, this),
                    new EnemyChaseTargetState(this, this),
                    new EnemyDamageState(this, this),
                    new EnemyDieState(this, this)
                };
            }

            _health.Reset(_config.StartHealth + _generation);
            
            _generation++;
            
            _navAgent.enabled = true;
            _navAgent.Warp(transform.position);
            _navAgent.avoidancePriority = 20 + Random.Range(1, 30); // 50 max
            
            _currentState = _allStates[0];

            _isInit = true;
        }

        
        private void OnHealthChange(int hp)
        {
            Debug.Log($"hp: {hp}");
        }
        

        public void SetTarget(ITarget target)
        {
            _myTarget = target;
            _currentState?.OnStart();
        }
        
        
        void IEnemyAgent.Die()
        {
            _navAgent.enabled = false;
            _currentState?.OnStop();
            
            DieEvent?.Invoke(this);
        }

        
        void IEnemyAgent.RotateViewToTarget(Vector3 lookTarget)
        {
            //_viewBody.
        }
        
        
        void IEnemyAgent.RotateViewToDirection(Vector3 dir)
        {
            //_viewBody.
        }
        

        void IDamage.TryApplyDamage(int damage, float stunTime)
        {
            _health.ApplyDamage(damage);
            _stunProvider.SetStun(stunTime);
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