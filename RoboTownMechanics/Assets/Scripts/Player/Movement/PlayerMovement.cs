using InputNameSpace;
using UnityEngine;

namespace Player.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        //--------------------Private--------------------//
        private InputComponent _inputComponent;

        private Rigidbody _rigidBody;

        [SerializeField]
        private float _movementForce;
        [SerializeField]
        private float _maxMovementSpeed;
        [SerializeField]
        private float _slowingMultiplier;

        //--------------------Functions--------------------//
        private void Start()
        {
            _inputComponent = InputComponent.Instance;

            _inputComponent.OnMoveAction += MovePlayer;

            _rigidBody = GetComponent<Rigidbody>();
        }

        private void OnDisable() => _inputComponent.OnMoveAction -= MovePlayer;

        private void Update()
        {
            if (_rigidBody.velocity.magnitude > _maxMovementSpeed)
                _rigidBody.velocity = Vector3.ClampMagnitude(_rigidBody.velocity, _maxMovementSpeed);
        }

        private void FixedUpdate()
        {
            if(!_inputComponent.OnMoveInputAction.IsPressed() && _rigidBody.velocity != Vector3.zero)
                SlowPlayer();
        }

        private void MovePlayer()
        {
            Vector2 direction = _inputComponent.OnMoveInputAction.ReadValue<Vector2>();
            _rigidBody.AddForce(new Vector3(direction.x, 0, direction.y) * _movementForce *Time.deltaTime);
        }

        private void SlowPlayer() => _rigidBody.velocity = _rigidBody.velocity * _slowingMultiplier;
    }
}