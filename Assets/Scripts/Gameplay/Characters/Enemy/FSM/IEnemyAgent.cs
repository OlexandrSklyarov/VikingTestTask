using Data;
using Gameplay.Characters.Animations;
using Gameplay.Characters.Attack;
using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.Characters.Enemy.FSM
{
    public interface IEnemyAgent
    {
        ITarget MyTarget { get; }
        NavMeshAgent NavAgent { get; }
        StunProvider StunProvider { get; }
        Health Health { get; }
        EnemyData Config { get; }
        AnimatorProvider AnimatorProvider { get; }
        AttackProvider AttackProvider { get; }
        float AttackRange { get; }

        void Die();
        void RotateViewToTarget(Vector3 lookTarget);
        void RotateViewToDirection(Vector3 dir);
        void Stop();
        void PrepareForDie();
    }
}