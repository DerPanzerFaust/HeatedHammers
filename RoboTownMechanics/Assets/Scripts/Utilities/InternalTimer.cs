using System.Collections;
using StateMachines.GlobalStateMachine;
using UnityEngine; 


namespace TimerHandler
{
    public class InternalTimer : SingletonBehaviour<InternalTimer>
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

            Debug.Log(_currentTime);
        }
        public void TimerStart()
        {
            _started = true;
            Debug.Log("Timer Started");
            _startTime = Time.time;
           //StartCoroutine(PlayTimer());
        }

        public void GoGameOver()
        {
            Debug.Log("GameOver");
            _stateMachine.SetState(_stateMachine.GameOverStateInstance);
        }

        private IEnumerator PlayTimer()
        {
            Debug.Log("Timer Start");
            while (_gameLength > 0)
            {
                yield return new WaitForSeconds(_gameLength);
                _gameLength--;
            }

            GoGameOver();
        }

    }
}