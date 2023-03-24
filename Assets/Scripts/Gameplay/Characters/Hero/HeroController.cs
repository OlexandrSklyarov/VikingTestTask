using Data;
using UnityEngine;

namespace Gameplay.Characters.Hero
{
    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
    public class HeroController : MonoBehaviour
    {
        private Health _health;
        

        public void Init(HeroEngine engineConfig, int startHealth)
        {
            _health = new Health(startHealth);
            _health.ChangeHealthEvent += OnHealthChange;
            _health.HealthZeroEvent += OnHealthIsOver;
        }

        
        private void OnHealthIsOver(int hp)
        {
            Debug.Log($"hp is over{hp}");
        }


        private void OnHealthChange(int hp)
        {
            Debug.Log($"hp {hp}");
        }
    }
}