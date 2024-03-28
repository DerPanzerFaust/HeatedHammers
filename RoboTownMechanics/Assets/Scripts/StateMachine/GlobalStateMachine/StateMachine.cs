using StateMachines.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachines.GlobalStateMachine
{
    public class StateMachine : SingletonBehaviour<StateMachine>
    {
        //--------------------Private--------------------//
        private State _currentState;
        [SerializeField]
        private State[] _states;

        //--------------------Public--------------------//
        public State CurrentState => _currentState;

        //--------------------Functions--------------------//
        public void SetState(State state)
        {
            if(_currentState == state) 
                return;
            _currentState.OnExitState();

            _currentState = state;

            _currentState.OnEnterState(this);
        }
    }
}