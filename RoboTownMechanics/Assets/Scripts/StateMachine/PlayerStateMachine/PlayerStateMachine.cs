using UnityEngine;
using Utilities;

namespace Player.StateMachine
{
    public class PlayerStateMachine : MonoBehaviour
    {
        //--------------------Private--------------------//
        private PlayerState _currentPlayerState;

        //--------------------Public--------------------//
        public PlayerState CurrentPlayerState
        {
            get => _currentPlayerState;
            set => _currentPlayerState = value;
        }

        //--------------------Functions--------------------//
        private void Awake() => _currentPlayerState = PlayerState.WALKING;
    }
}