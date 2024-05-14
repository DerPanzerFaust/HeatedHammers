using StateMachines.States;
using System;
using UnityEngine;

namespace StateMachines.GlobalStateMachine
{
    public class StateMachine : SingletonBehaviour<StateMachine>
    {
        //--------------------Private--------------------//
        private State _currentState;

        private LobbyState _lobbyStateInstance = new LobbyState();
        private GameState _gameStateInstance = new GameState();
        private GameOverState _gameOverStateInstance = new GameOverState();
        private ResetState _resetStateInstance = new ResetState();

        //--------------------Public--------------------//
        public State CurrentState => _currentState;

        public LobbyState LobbyStateInstance => _lobbyStateInstance;
        public GameState GameStateInstance => _gameStateInstance;
        public GameOverState GameOverStateInstance => _gameOverStateInstance;
        public ResetState ResetStateInstance => _resetStateInstance;


        //--------------------Functions--------------------//
        private void Start() => SetState(LobbyStateInstance);

        /// <summary>
        /// Set the current state of the state machine to the given state
        /// </summary>
        /// <param name="state">The state to set the state machine to</param>
        public void SetState(State state)
        {
            if(_currentState == state) 
                return;

            if(_currentState != null)
                _currentState.OnExitState();

            _currentState = state;

            _currentState.OnEnterState(this);
        }
    }
}