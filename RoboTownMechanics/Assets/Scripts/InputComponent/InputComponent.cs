using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputNameSpace
{
    public class InputComponent : MonoBehaviour
    {
        //--------------------Private--------------------//
        private GameInput _gameInput;
        private InputAction _onMoveInputAction;
        private InputAction _interact;
        //--------------------Public--------------------//
        public InputAction OnMoveInputAction
        {
            get => _onMoveInputAction;
            set => _onMoveInputAction = value;
        }
        
        public GameInput GameInput
        {
            get => _gameInput;
            set => _gameInput = value;
        }

        public InputAction Interact
        {
            get => _interact;
            set => _interact = value;
        }

        public Action OnMoveAction;
        //--------------------Functions--------------------//
        private void Awake()
        {
            _gameInput = new GameInput();
            _onMoveInputAction = _gameInput.Player.Move;
            _interact = _gameInput.Player.Interact;
        }



        private void OnEnable()
        {
            _onMoveInputAction.Enable();
            _interact.Enable();
        }

        private void OnDisable()
        {
            _onMoveInputAction.Disable();
            _interact.Disable();
        }

        private void Update()
        {
            if (_onMoveInputAction.IsPressed())
                OnMoveAction?.Invoke();
        }
    }
}
