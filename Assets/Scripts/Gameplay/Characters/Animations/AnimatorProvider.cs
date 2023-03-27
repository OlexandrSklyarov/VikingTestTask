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
        
        
        public void SetSpeed(float speed) => 
                _animator.SetFloat(ConstPrm.Animation.MOVE_SPEED_PRM, speed, 0.1f, Time.deltaTime);

        
        public void PlayAttack() => SetTrigger(ConstPrm.Animation.ATTACK);
        
        
        public void PlayDamage() => SetTrigger(ConstPrm.Animation.DAMAGE);
        
        
        public void PlayDie() => SetTrigger(ConstPrm.Animation.DIE);
        

        private void SetTrigger(string triggerName) => _animator.SetTrigger(triggerName);
    }
}
