using System;
using UnityEngine;

namespace Gameplay.Characters
{
    public class Health
    {
        public int CurrentHP
        {
            get => _hp;
            private set
            {
                _hp = Mathf.Max(0, value);
                ChangeHealthEvent?.Invoke(_hp);
                if (_hp <= 0) HealthZeroEvent?.Invoke(_hp);
            }
        }
        
        private int _hp;

        public event Action<int> ChangeHealthEvent;
        public event Action<int> HealthZeroEvent;
        
        
        public Health(int startHealth)
        {
            _hp = startHealth;
        }


        public void ApplyDamage(int damage)
        {
            if (damage < 0) throw new ArgumentException($"Damage {damage} < 0!!!");
            CurrentHP -= damage;
        }
    }
}