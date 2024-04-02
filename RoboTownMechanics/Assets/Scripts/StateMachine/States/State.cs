using StateMachines.GlobalStateMachine;

namespace StateMachines.States
{
    public abstract class State
    {
        //--------------------Private--------------------//
        private StateMachine _currentStateMachine;

        //--------------------Functions--------------------//
        /// <summary>
        /// Function to call when entering a state
        /// </summary>
        /// <param name="statemachine">Give a statemachine to set this state's currentStateMachine to</param>
        public void OnEnterState(StateMachine statemachine)
        {
            _currentStateMachine = statemachine;
            OnEnter();
        }

        protected virtual void OnEnter()
        {
            
        }

        /// <summary>
        /// Function to call when updating a state
        /// </summary>
        public void OnUpdateState()
        {
            OnUpdate();
        }

        protected virtual void OnUpdate()
        {

        }

        /// <summary>
        /// Function to call when exiting the state
        /// </summary>
        public void OnExitState()
        {
            OnExit();
        }

        protected virtual void OnExit()
        {

        }
    }
}