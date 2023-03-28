using System;
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
            if (!FindTargetsInRadius(
                    _damageTrigger.transform.position, 
                    _damageTrigger.radius,
                    _resultColliders,
                    _config.TargetLayerMask))
                return;

            var colliders = _resultColliders.Where(c => c != null);
            
            foreach(var col in colliders)
            {
                Util.DebugUtility.PrintColor($"col {col}", Color.cyan);
                if (!col.TryGetComponent(out IDamage target)) continue;
                target.TryApplyDamage(_config.Damage, _config.StunTime);
            }
        }


        private bool FindTargetsInRadius(Vector3 pos, float radius, Collider[] resultColliders, LayerMask layer)
        {
            return (Physics.OverlapSphereNonAlloc(pos, radius, resultColliders, layer) > 0);
        }


        public bool IsCanAttack()
        {
            return Time.time - _config.AttackDelay > _lastAttackTime;
        }


        public void StartAttack() => _lastAttackTime = Time.time;

        
        public void TryFindNearTarget(Vector3 pos, Vector3 lookDirection, Action<Vector3> successCallback)
        {
            if (!FindTargetsInRadius(pos, _config.ViewTargetRadius, _resultColliders, _config.TargetLayerMask)) return;

            var cols = _resultColliders
                .Where(c => c != null)
                .Select(c => c.GetComponent<IDamage>())
                .Where(c => c != null)
                .OrderBy(c => Vector3.Angle(lookDirection, c.Position - pos));

            if (!cols.Any()) return;
            
            successCallback?.Invoke(cols.First().Position);
        }
    }
}