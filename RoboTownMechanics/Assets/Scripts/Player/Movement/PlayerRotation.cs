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

        //--------------------Function--------------------//
        private void Start()
        {
            _master = GetComponent<PlayerData>().Master;

            _inputComponent = _master.PlayerInputComponent;

            _inputComponent.OnMoveAction += RotatePlayerTowardsDirection;
        }

        private void RotatePlayerTowardsDirection()
        {
            Vector2 direction = _inputComponent.OnMoveInputAction.ReadValue<Vector2>();
            
            Quaternion rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
        }
    }
}