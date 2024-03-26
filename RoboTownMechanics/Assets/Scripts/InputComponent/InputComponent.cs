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
        private InputAction _onInteractInputAction;
        private InputAction _onPickUpInputAction;

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

        public InputAction OnInteractInputAction
        {
            get => _onInteractInputAction;
            set => _onInteractInputAction = value;
        }

        public InputAction OnPickUpInputAction
        {
            get => _onPickUpInputAction;
            set => _onPickUpInputAction = value;
        }

        public Action OnMoveAction;

        //--------------------Functions--------------------//
        private void Awake()
        {
            _gameInput = new GameInput();

            _onMoveInputAction = _gameInput.Player.Move;
            _onInteractInputAction = _gameInput.Player.Interact;
            _onPickUpInputAction = _gameInput.Player.PickUp;
        }

        private void OnEnable()
        {
            _onMoveInputAction.Enable();
            _onInteractInputAction.Enable();
            _onPickUpInputAction.Enable();
        }

        private void OnDisable()
        {
            _onMoveInputAction.Disable();
            _onInteractInputAction.Disable();
            _onPickUpInputAction.Disable();
        }

        private void Update()
        {
            if (_onMoveInputAction.IsPressed())
                OnMoveAction?.Invoke();
        }
    }
}
