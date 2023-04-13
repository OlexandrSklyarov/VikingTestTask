using System;
using UnityEngine;

namespace Gameplay.Characters.Animations
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorProvider : MonoBehaviour
    {
        public Animator Animator => _animator;

        private Animator _animator;

        public event Action AttackEvent;

        public void Init() => _animator = GetComponent<Animator>();


        private void OnAttack() => AttackEvent?.Invoke();


        public void SetSpeed(float speed, bool isImmediately = false)
        {
            if (isImmediately)
                _animator.SetFloat(ConstPrm.Animation.MOVE_SPEED_PRM, speed);
            else
                _animator.SetFloat(ConstPrm.Animation.MOVE_SPEED_PRM, speed, 0.1f, Time.deltaTime);
        }
            


        public void PlayAttack() => SetTrigger(ConstPrm.Animation.ATTACK);


        public void PlayDamage() => SetTrigger(ConstPrm.Animation.DAMAGE);


        public void PlayAlive() => _animator.SetBool(ConstPrm.Animation.DIE, false);


        public void PlayDie() => _animator.SetBool(ConstPrm.Animation.DIE, true);


        private void SetTrigger(string triggerName) => _animator.SetTrigger(triggerName);
        
        
        private bool IsPlayState(string stateName)
        {
            var state = _animator.GetCurrentAnimatorStateInfo(0);
            return state.IsName(stateName);
        }
        
        
        public bool IsPlayAttack() => IsPlayState(ConstPrm.Animation.ATTACK);

        
        public bool IsPlayDamage() => IsPlayState(ConstPrm.Animation.DAMAGE);
    }
}
