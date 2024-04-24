using InputNameSpace;
using LocalMultiplayer.Player;
using UnityEngine;
using Player.Animation;

namespace Player.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        //--------------------Private--------------------//
        private InputComponent _inputComponent;

        private Rigidbody _rigidBody;

        [SerializeField]
        private float _maxMovementForce;
        [SerializeField]
        private float _minimalMovementForce;
        private float _currentMovementForce;
        [SerializeField]
        private float _maxMovementSpeed;
        [SerializeField]
        private float _minimalMovementSpeed;
        [SerializeField]
        private float _slowingMultiplier;

        private PlayerMaster _master;

        private PlayerAnimation _playerAnimation;

        private bool _canMove;

        //--------------------Public--------------------//
        public bool CanMove
        {
            get => _canMove;
            set => _canMove = value;
        }

        //--------------------Functions--------------------//
        private void Start()
        {
            _master = GetComponent<PlayerData>().Master;

            _playerAnimation = GetComponent<PlayerAnimation>();

            _inputComponent = _master.PlayerInputComponent;

            _inputComponent.OnMoveAction += MovePlayer;

            _rigidBody = GetComponent<Rigidbody>();

            _currentMovementForce = _maxMovementForce;

            _canMove = true;
        }

        private void OnDisable() => _inputComponent.OnMoveAction -= MovePlayer;

        private void FixedUpdate()
        {
            if(!_inputComponent.OnMoveInputAction.IsPressed() && _rigidBody.velocity != Vector3.zero)
                SlowPlayer();

            if (CanMove)
                _playerAnimation.PlayerSpeed = _inputComponent.OnMoveInputAction.ReadValue<Vector2>().magnitude;
            else
                _playerAnimation.PlayerSpeed = 0;
        }

        private void CalculateForce()
        {
            if (_rigidBody.velocity.magnitude < 5)
                _currentMovementForce = _maxMovementForce;
            else
                _currentMovementForce = _minimalMovementForce;
        }

        private void MovePlayer()
        {
            if (!_canMove)
                return;

            CalculateForce();

            Vector2 direction = _inputComponent.OnMoveInputAction.ReadValue<Vector2>();
            _rigidBody.AddForce(new Vector3(direction.x, 0, direction.y) * _currentMovementForce *Time.deltaTime);

            if (_rigidBody.velocity.magnitude > _maxMovementSpeed)
                _rigidBody.velocity = Vector3.ClampMagnitude(_rigidBody.velocity, _maxMovementSpeed);
        }

        private void SlowPlayer()
        {
            if (_rigidBody.velocity.magnitude <= _minimalMovementSpeed)
                _rigidBody.velocity = Vector3.zero;

            _rigidBody.velocity = _rigidBody.velocity * _slowingMultiplier;
        }
    }
}