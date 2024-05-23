using Robot.List;
using Robot.Spawn;
using UnityEngine;

namespace Robot.Timer
{
    public class BotPartBreakTimer : MonoBehaviour
    {
        //--------------------Private--------------------//
        [SerializeField]
        private float _initialTime;
        [SerializeField]
        private float _currentTime;
        [SerializeField]
        private BotPartList _brokenBotList;
        [SerializeField]
        private BotPartHandler _brokenBotSpawn;

        private bool _isRunning;

        //--------------------Public--------------------//
        public bool IsRunning
        {
            get => _isRunning;
            set => _isRunning = value;
        }

        //--------------------Functions--------------------//
        private void Start() => _currentTime = _initialTime;

        private void Update()
        {
            if (_isRunning)
            { 
                DoTimer();
            }
        }
        public void DoTimer()
        {
            _currentTime -= Time.deltaTime;
            if (_currentTime <= 0f)
            {
                _brokenBotSpawn.BreakPart();
                ResetTime();
            }
        }
        private void ResetTime() => _currentTime = _initialTime;
    }
}
