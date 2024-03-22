using InputNameSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Rotation
{
    public class PlayerRotation : MonoBehaviour
    {
        //--------------------Private--------------------//
        private InputComponent _inputComponent;

        [SerializeField]
        private float _rotationSpeed;

        //--------------------Function--------------------//
        private void Start()
        {
            _inputComponent = InputComponent.Instance;

            _inputComponent.OnMoveAction += RotatePlayerTowardsDirection;
        }

        private void RotatePlayerTowardsDirection()
        {
            Vector2 direction = _inputComponent.OnMoveInputAction.ReadValue<Vector2>();
            
            Quaternion rotation = Quaternion.LookRotation(new Vector3(direction.x, 0 , direction.y));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
        }
    }
}