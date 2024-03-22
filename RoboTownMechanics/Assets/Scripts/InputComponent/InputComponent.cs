using UnityEngine.InputSystem;

namespace InputNameSpace
{
    public class InputComponent : SingletonBehaviour<InputComponent>
    {
        //--------------------Private--------------------//
        private GameInput _gameInput;

        private InputAction _move;
        private InputAction _interact;
        //--------------------Public--------------------//
        public InputAction Move
        {
            get => _move; 
            set => _move = value;
        }

        public InputAction Interact
        {
            get => _interact;
            set => _interact = value;
        }

        //--------------------Functions--------------------//
        private void Awake()
        {
            _gameInput = new GameInput();

            _move = _gameInput.Player.Move;
            _interact = _gameInput.Player.Interact;
        }

        private void OnEnable()
        {
            _move.Enable();
            _interact.Enable();
        }

        private void OnDisable()
        {
            _move.Disable();
            _interact.Disable();
        }
    }
}