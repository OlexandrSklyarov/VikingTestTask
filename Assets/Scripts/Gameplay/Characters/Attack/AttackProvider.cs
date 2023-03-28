using System.Linq;
using Data;
using UnityEngine; 

namespace Gameplay.Characters.Attack
{
    public class AttackProvider
    {
        private readonly AttackData _config;
        private readonly SphereCollider _damageTrigger;
        private readonly Collider[] _resultColliders;
        private float _lastAttackTime;

        public AttackProvider(AttackData config, SphereCollider damageTrigger)
        {
            _config = config;
            _damageTrigger = damageTrigger;
            _resultColliders = new Collider[10];
        }

        public bool IsAttackActive => !IsCanAttack();


        public void ApplyDamage()
        {   
            if (Physics.OverlapSphereNonAlloc
            (
                _damageTrigger.transform.position,
                _damageTrigger.radius,
                _resultColliders,
                _config.TargetLayerMask
            ) <= 0) return;

            var colliders = _resultColliders.Where(c => c != null);
            
            foreach(var col in colliders)
            {
                Util.DebugUtility.PrintColor($"col {col}", Color.cyan);
                if (!col.TryGetComponent(out IDamage target)) continue;
            }
        }


        public bool IsCanAttack()
        {
            return Time.time - _config.AttackDelay > _lastAttackTime;
        }


        public void StartAttack() => _lastAttackTime = Time.time;
    }
}