using System.Collections;
using StateMachines.GlobalStateMachine;
using UnityEngine; 


namespace TimerHandler
{
    public class InternalTimer : SingletonBehaviour<InternalTimer>
    {
        //--------------------Public--------------------//
        public bool isPaused;


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

        //--------------------Functions-----------------//

        private void Start()
        {
            _gameLength = _minutes * 60 + _seconds;
            _stateMachine = StateMachine.Instance;
        }

        private void Update()
        {
            if(_currentTime < _gameLength && _started)
                _currentTime += Time.deltaTime;
            else if(_started)
                GoGameOver();

        }


        /// <summary>
        /// 
        /// </summary>
        public void TimerStart()
        {
            _started = true;
            _startTime = Time.time;
        }

        public void GoGameOver()
        {
            _stateMachine.SetState(_stateMachine.GameOverStateInstance);
        }


        public void PausingGame()
        {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0 : 1;  
        }

    }
}