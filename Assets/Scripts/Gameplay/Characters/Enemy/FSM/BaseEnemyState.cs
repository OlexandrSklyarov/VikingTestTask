namespace Gameplay.Characters.Enemy.FSM
{
    public abstract class BaseEnemyState
    {
        protected readonly IEnemyContextSwitcher _context;
        protected readonly IEnemyAgent _agent;

        
        protected BaseEnemyState(IEnemyContextSwitcher context, IEnemyAgent agent)
        {
            _context = context;
            _agent = agent;
        }

        public abstract void OnStart();
        public abstract void OnStop();
        public virtual void OnUpdate() {}
        public virtual void OnLateUpdate() {}
        public virtual void OnFixedUpdate() {}
    }
}