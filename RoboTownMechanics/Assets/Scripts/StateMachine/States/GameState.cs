using TimerHandler;
using PartsHandler;

namespace StateMachines.States
{
    public class GameState : State
    {
        //--------------------Private--------------------//

        private InternalTimer _timerManager;
        private PartHandler _partHandler;
        //--------------------Functions--------------------//

        protected override void OnEnter()
        {
            _timerManager = InternalTimer.Instance;
            _timerManager.TimerStart();

            _partHandler = PartHandler.Instance;

            _partHandler._repaired = true;
        }

        protected override void OnUpdate()
        {

        }

        protected override void OnExit()
        {

        }
    }
}