using System;
using UnityEngine.InputSystem;

namespace InputNameSpace
{
    public class InputComponent : SingletonBehaviour<InputComponent>
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
                OnMoveAction.Invoke();
        }
    }
}