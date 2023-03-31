using System;
using System.Linq;
using Data;
using UnityEngine;
using Debug = Util.Debug;

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
        

        public void ApplyDamage()
        {
            var count = FindTargetsInRadius(
                    _damageTrigger.transform.position, 
                    _damageTrigger.radius,
                    _resultColliders,
                    _config.TargetLayerMask);
            
            if (count <= 0) return;

            for (int i = 0; i < count; i++)
            {
                var col = _resultColliders[i];
                if (col == null) continue;
                if (!col.TryGetComponent(out IDamage damage)) continue;
                
                damage.TryApplyDamage(_config.Damage, _config.StunTime);
            }
        }


        private int FindTargetsInRadius(Vector3 pos, float radius, Collider[] resultColliders, LayerMask layer)
        {
            var count = (Physics.OverlapSphereNonAlloc(pos, radius, resultColliders, layer));
            return count;
        }
        

        public void StartAttack() => _lastAttackTime = Time.time;

        
        public void TryFindNearTarget(Vector3 pos, Vector3 lookDirection, Action<Vector3> successCallback)
        {
            var count = FindTargetsInRadius(pos, _config.ViewTargetRadius, _resultColliders, _config.TargetLayerMask);
            if (count <= 0) return;

            var damageUnits = _resultColliders
                .Take(count)
                .Where(c => c != null)
                .Select(c => c.GetComponent<IDamage>())
                .Where(c => c != null && c.IsAlive)
                .OrderBy(c => (c.Position - pos).sqrMagnitude)
                .OrderBy(c => Vector3.Angle(lookDirection, c.Position - pos));

            if (!damageUnits.Any()) return;
            
            successCallback?.Invoke(damageUnits.First().Position);
        }
    }
}