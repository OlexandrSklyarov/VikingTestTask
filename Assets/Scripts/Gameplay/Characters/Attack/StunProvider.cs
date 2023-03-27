
namespace Gameplay.Characters.Attack
{
    public class StunProvider
    {
        private float _currentSunTime;
        private bool _isStunned;
        private float _stunnedTimer;


        public void SetStun(float time)
        {
            _currentSunTime = time;
            _isStunned = true;
        }


        public bool IsStunned()
        {
            if (_isStunned && Util.TimeUtil.IsTimeEnd(ref _stunnedTimer, _currentSunTime))
            {
                _isStunned = false;
            }

            return _isStunned;
        }
    }
}