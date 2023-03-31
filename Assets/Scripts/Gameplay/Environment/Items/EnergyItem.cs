using Services.Pooling;
using UnityEngine;

namespace Gameplay.Environment.Items
{
    public class EnergyItem : MonoBehaviour
    {
        public int HealthValue => _healthValue;
        
        [SerializeField, Min(1)] private int _healthValue = 1;
        
        private IFactoryStorage<EnergyItem> _storage;


        public void Hide() => _storage.ReturnToStorage(this);
        

        public void Init(IFactoryStorage<EnergyItem> storage)
        {
            _storage ??= storage;
        }
    }
}