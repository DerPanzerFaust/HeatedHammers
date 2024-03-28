using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachines.GlobalStateMachine;

namespace StateMachines.States
{
    public abstract class State
    {
        //--------------------Private--------------------//
        private StateMachine _currentStateMachine;

        //--------------------Functions--------------------//
        public void OnEnterState(StateMachine statemachine)
        {
            _currentStateMachine = statemachine;
            OnEnter();
        }

        protected virtual void OnEnter()
        {
            
        }

        public void OnUpdateState()
        {
            OnUpdate();
        }

        protected virtual void OnUpdate()
        {

        }

        public void OnExitState()
        {
            OnExit();
        }

        protected virtual void OnExit()
        {

        }
    }
}