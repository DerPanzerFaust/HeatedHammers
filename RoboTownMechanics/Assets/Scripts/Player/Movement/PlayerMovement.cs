using InputNameSpace;
using LocalMultiplayer.Player;
using UnityEngine;

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

        //--------------------Functions--------------------//
        private void Start()
        {
            _master = GetComponent<PlayerData>().Master;

            _inputComponent = _master.PlayerInputComponent;

            _inputComponent.OnMoveAction += MovePlayer;

            _rigidBody = GetComponent<Rigidbody>();

            _currentMovementForce = _maxMovementForce;
        }

        private void OnDisable() => _inputComponent.OnMoveAction -= MovePlayer;

        private void FixedUpdate()
        {
            if(!_inputComponent.OnMoveInputAction.IsPressed() && _rigidBody.velocity != Vector3.zero)
                SlowPlayer();
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