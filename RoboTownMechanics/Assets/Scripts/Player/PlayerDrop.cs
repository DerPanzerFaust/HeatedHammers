using PickUps;
using Player.StateMachine;
using UnityEngine;
using Utilities;
using Interaction.Workstations;
using LocalMultiplayer.Player;
using System.Collections;
using Player.Movement;
using Player.Animation;
using Player.Rotation;
using Player.PickUp;
using Interaction.Pickup;
using static UnityEngine.Rendering.DebugUI;
using UnityEditor;
using Interaction.Base;
using System.Xml.Serialization;

namespace Player.Drop
{
    public class PlayerDrop : MonoBehaviour
    {
        //--------------------Private--------------------//
        private PickUpComponent _currentPickedUpObject;

        private PlayerStateMachine _playerStateMachine;

        private PlayerMovement _playerMovement;

        private PlayerRotation _playerRotation;

        private PlayerAnimation _playerAnimation;

        private PlayerPickUp _playerPickUp;
     
        private PickUpInteraction _pickUpInteraction;

        private Interactor _interaction;

        private bool _isPickingUp;
        private bool _isPlacing;

        private float _partSpeed;
        [SerializeField]
        private float _startThrowSpeed;

        //--------------------Public--------------------//

        public PickUpComponent CurrentPickedUpObject => _playerPickUp.CurrentPickedUpObject;


        //--------------------Functions--------------------//
        private void Start()
        {
            _playerPickUp = GetComponent<PlayerPickUp>();
            
            _pickUpInteraction = GetComponent<PickUpInteraction>();

            _playerStateMachine = GetComponent<PlayerStateMachine>();
            _playerAnimation = GetComponent<PlayerAnimation>();
            _playerMovement = GetComponent<PlayerMovement>();
            _playerRotation = GetComponent<PlayerRotation>();

            _interaction = GetComponent<Interactor>();
        }

        /// <summary>
        /// When this function is called, the player will drop the pickedup object
        /// </summary>
        /// <param name="dropObject">the object to Drop</param>
        public void DropObject(PickUpComponent dropObject)
        {
            SetWalkingState();

            if (_isPickingUp == true)
                return;

            _playerMovement.CanMove = false;
            _playerRotation.CanRotate = false;
            _isPlacing = true; 

            StartCoroutine(DropObjectRoutine(dropObject));
        }

        private IEnumerator DropObjectRoutine(PickUpComponent dropObject)
        {
            _playerPickUp.Rigidbody.isKinematic = false;
            dropObject.gameObject.GetComponent<BoxCollider>().enabled = true;
            _playerAnimation.StopPickUpObjectAnimation();
            _currentPickedUpObject = dropObject;

            yield return new WaitForSeconds(_playerAnimation.GetPickupAnimDuration());

            dropObject.transform.rotation = transform.rotation;
            dropObject.transform.position = transform.position + transform.forward; 
            dropObject.transform.SetParent(null, true);

            _playerPickUp.CurrentPickedUpObject = null;

            _playerMovement.CanMove = true;
            _playerRotation.CanRotate = true;
            _isPlacing = false;
        }

        private void SetWalkingState() => _playerStateMachine.CurrentPlayerState = PlayerState.WALKING;



        public void ThrowObject(PickUpComponent dropObject)
        {

            SetWalkingState();

            if (_isPickingUp == true)
                return;

            _playerMovement.CanMove = false;
            _playerRotation.CanRotate = false;
            _isPlacing = true;

            StartCoroutine(ThrowObjectRoutine(dropObject));
        }

        private IEnumerator ThrowObjectRoutine(PickUpComponent dropObject)
        {
            _playerPickUp.Rigidbody.isKinematic = false;
            dropObject.gameObject.GetComponent<BoxCollider>().enabled = true;


            _partSpeed = _interaction._charge * _startThrowSpeed;
            
            _playerAnimation.StopPickUpObjectAnimation();
            _currentPickedUpObject = dropObject;

            dropObject.transform.rotation = transform.rotation;
            _playerPickUp.Rigidbody.AddForce(transform.up + transform.forward * _partSpeed);
            dropObject.transform.SetParent(null, true);

            

            yield return new WaitForSeconds(_playerAnimation.GetPickupAnimDuration());

            _playerPickUp.CurrentPickedUpObject = null;

            _playerMovement.CanMove = true;
            _playerRotation.CanRotate = true;
            _isPlacing = false;
        }

    }
}