using System;
using Gameplay.Environment.Items;
using UnityEngine;

namespace Gameplay.Characters.Hero
{
    public class HeroInteractController : MonoBehaviour
    {
        private IHeroInteract _hero;
        

        public void Init(IHeroInteract hero)
        {
            _hero = hero;
        }
        
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out EnergyItem energy))
            {
                _hero.AddHealth(energy.HealthValue);
                energy.Hide();
            }
        }
    }
}