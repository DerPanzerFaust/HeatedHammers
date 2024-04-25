using InputNameSpace;
using LocalMultiplayer.Player;
using UnityEngine;

namespace Player.Rotation
{
    public class PlayerRotation : MonoBehaviour
    {
        //--------------------Private--------------------//
        private InputComponent _inputComponent;

        [SerializeField]
        private float _rotationSpeed;

        private PlayerMaster _master;

        private bool _canRotate;

        //--------------------Public--------------------//
        public bool CanRotate
        {
            get => _canRotate;
            set => _canRotate = value;
        }

        //--------------------Function--------------------//
        private void Start()
        {
            _master = GetComponent<PlayerData>().Master;

            _canRotate = true;

            _inputComponent = _master.PlayerInputComponent;

            _inputComponent.OnMoveAction += RotatePlayerTowardsDirection;
        }

        private void RotatePlayerTowardsDirection()
        {
            if (!_canRotate)
                return;

            Vector2 direction = _inputComponent.OnMoveInputAction.ReadValue<Vector2>();
            
            Quaternion rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
        }
    }
}