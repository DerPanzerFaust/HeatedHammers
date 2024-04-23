using TimerHandler;

namespace StateMachines.States
{
    public class GameState : State
    {
        //--------------------Private--------------------//

        private InternalTimer _timerManager;
        //--------------------Functions--------------------//

        protected override void OnEnter()
        {
            _timerManager = InternalTimer.Instance;
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