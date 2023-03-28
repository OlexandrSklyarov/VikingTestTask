using System.Collections.Generic;

namespace Gameplay.EntityManager
{
    public class GameEntityManager
    {
        private static Dictionary<int, ITick> _tickEntity = new Dictionary<int, ITick>();
        private static Dictionary<int, IFixedTick> _fixedTickEntity = new Dictionary<int, IFixedTick>();
        private static Dictionary<int, ITickLate> _tickLateEntity = new Dictionary<int, ITickLate>();

        
        public static void AddEntity(object entity, int id)
        {
            if (entity is ITick && !_tickEntity.ContainsKey(id))
            {
                _tickEntity.Add(id, entity as ITick);
            }
            
            if (entity is IFixedTick && !_fixedTickEntity.ContainsKey(id))
            {
                _fixedTickEntity.Add(id, entity as IFixedTick);
            }
            
            if (entity is ITickLate && !_tickLateEntity.ContainsKey(id))
            {
                _tickLateEntity.Add(id, entity as ITickLate);
            }
        }
        
        
        public static void RemoveEntity(int id)
        {
            if (_tickEntity.ContainsKey(id))
            {
                _tickEntity.Remove(id);
            }
            
            if (_fixedTickEntity.ContainsKey(id))
            {
                _fixedTickEntity.Remove(id);
            }
            
            if (_fixedTickEntity.ContainsKey(id))
            {
                _tickLateEntity.Remove(id);
            }
        }


        public void OnUpdate()
        {
            foreach (var entity in _tickEntity)
            {
                entity.Value.OnUpdate();
            }
        }
        
        
        public void OnFixedUpdate()
        {
            foreach (var entity in _fixedTickEntity)
            {
                entity.Value.OnFixedUpdate();
            }
        }
        
        
        public void OnLateUpdate()
        {
            foreach (var entity in _tickLateEntity)
            {
                entity.Value.OnLateUpdate();
            }
        }


        public void Clear()
        {
            _tickEntity.Clear();
            _fixedTickEntity.Clear();
            _tickLateEntity.Clear();
        }
    }
}