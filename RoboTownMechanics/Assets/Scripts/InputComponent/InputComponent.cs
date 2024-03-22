using UnityEngine.InputSystem;

namespace InputNameSpace
{
    public class InputComponent : SingletonBehaviour<InputComponent>
    {
        //--------------------Private--------------------//
        private GameInput _gameInput;

        private InputAction _move;
        //--------------------Public--------------------//
        public InputAction Move
        {
            get => _move; 
            set => _move = value;
        }

        //--------------------Functions--------------------//
        private void Awake()
        {
            _gameInput = new GameInput();

            _move = _gameInput.Player.Move;
        }

        private void OnEnable() => _move.Enable();

        private void OnDisable() => _move.Disable();
    }
}