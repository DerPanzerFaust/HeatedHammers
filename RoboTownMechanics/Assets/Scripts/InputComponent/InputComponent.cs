using System;
using UnityEngine.InputSystem;

namespace InputNameSpace
{
    public class InputComponent : SingletonBehaviour<InputComponent>
    {
        //--------------------Private--------------------//
        private GameInput _gameInput;

        private InputAction _onMoveInputAction;
        //--------------------Public--------------------//
        public InputAction OnMoveInputAction
        {
            get => _onMoveInputAction; 
            set => _onMoveInputAction = value;
        }

        public Action OnMoveAction;
        //--------------------Functions--------------------//
        private void Awake()
        {
            _gameInput = new GameInput();

            _onMoveInputAction = _gameInput.Player.Move;
        }

        private void OnEnable() => _onMoveInputAction.Enable();

        private void OnDisable() => _onMoveInputAction.Disable();

        private void Update()
        {
            if (_onMoveInputAction.IsPressed())
                OnMoveAction.Invoke();
        }
    }
}