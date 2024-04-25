using StateMachines.GlobalStateMachine;
using System;
using UnityEngine; 


namespace TimerHandler
{
    public class GameTimer : SingletonBehaviour<GameTimer>
    {
        //--------------------Private--------------------//
        private float _gameLength;
        private StateMachine _stateMachine;

        private float _startTime;
        private float _currentTime;

        private bool _started;

        [SerializeField]
        private int _minutes = 0;
        [SerializeField]
        private int _seconds = 0;

        //--------------------Public--------------------//
        public Action<float> OnCurrentTimeChanged;

        public float GameLength => _gameLength;

        //--------------------Functions-----------------//

        private void Start()
        {
            _gameLength = _minutes * 60 + _seconds;
            _stateMachine = StateMachine.Instance;
        }

        private void Update()
        {
            if(_currentTime < _gameLength && _started)
            {
                _currentTime += Time.deltaTime;
                OnCurrentTimeChanged?.Invoke(_currentTime);
            }
            else if (_started)
            {
                GoGameOver();
            }
        }

        /// <summary>
        /// This starts the timer when its needed
        /// </summary>
        public void TimerStart()
        {
            _started = true;
            _startTime = Time.time;
        }

        private void GoGameOver()
        {
            _stateMachine.SetState(_stateMachine.GameOverStateInstance);
        }

    }
}