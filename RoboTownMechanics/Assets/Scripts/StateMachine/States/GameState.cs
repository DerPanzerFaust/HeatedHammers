using TimerHandler;


namespace StateMachines.States
{
    public class GameState : State
    {
        //--------------------Private--------------------//

        private GameTimer _timerManager;
        //--------------------Functions--------------------//

        protected override void OnEnter()
        {
            _timerManager = GameTimer.Instance;
            _timerManager.TimerStart();
        }

        protected override void OnUpdate()
        {

        }

        protected override void OnExit()
        {

        }
    }
}