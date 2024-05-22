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
using Interaction.Base;

namespace Player.PickUp
{
    public class PlayerPickUp : MonoBehaviour
    {
        //--------------------Private--------------------//
        private PickUpComponent _currentPickedUpObject;

        private PlayerStateMachine _playerStateMachine;

        private PlayerMovement _playerMovement;

        private PlayerRotation _playerRotation;

        private PlayerAnimation _playerAnimation;

        private bool _isPickingUp;
        private bool _isPlacing;

        //--------------------Public--------------------//
        public PickUpComponent CurrentPickedUpObject => _currentPickedUpObject;

        //--------------------Functions--------------------//
        private void Start()
        {
            _playerStateMachine = GetComponent<PlayerStateMachine>();
            _playerAnimation = GetComponent<PlayerAnimation>();
            _playerMovement = GetComponent<PlayerMovement>();
            _playerRotation = GetComponent<PlayerRotation>();
        }

        /// <summary>
        /// When this function is called, the player will pick up the given object
        /// </summary>
        /// <param name="pickUpObject">The object to PickUp</param>
        public void PickUpObject(PickUpComponent pickUpObject)
        {
            SetWalkingState();

            if (_currentPickedUpObject != null || _isPickingUp)
                return;

            _playerMovement.CanMove = false;
            _playerRotation.CanRotate = false;
            _isPlacing = true;
           
            StartCoroutine(PickUpObjectRoutine(pickUpObject));
        }

        /// <summary>
        /// destroys the currentObject
        /// </summary>
        public void DestroyObject()
        {
            Destroy(_currentPickedUpObject.gameObject);
            _currentPickedUpObject = null;
        }

        private IEnumerator PickUpObjectRoutine(PickUpComponent pickUpObject)
        {
            _playerAnimation.PickUpObjectAnimation();
            _currentPickedUpObject = pickUpObject;

            yield return new WaitForSeconds(_playerAnimation.GetPickupAnimDuration());

            pickUpObject.transform.rotation = transform.rotation;
            pickUpObject.transform.position = transform.position + new Vector3(0, .5f, 0);
            pickUpObject.transform.SetParent(transform, true);

            _playerAnimation.HoldObjectAnimation();

            _playerMovement.CanMove = true;
            _playerRotation.CanRotate = true;
            _isPlacing = false;
        }
            

        /// <summary>
        /// When this function is called, the object the player is holding will be put into the given station
        /// </summary>
        /// <param name="station">The station to place object inside of</param>
        public void PlaceInStation(WorkstationInteraction station)
        {
            SetWalkingState();

            if (_isPlacing)
                return;

            switch (station.CurrentStationType)
            {
                case StationType.REPAIR:
                    if (_currentPickedUpObject.CurrentPickUpState != PickUpState.SCRAP)
                        return;
                    break;

                case StationType.RECYCLE:
                    if (_currentPickedUpObject.CurrentPickUpState != PickUpState.DESTROYED)
                        return;
                    break;

                case StationType.COMPLETED:
                    if (_currentPickedUpObject.CurrentPickUpState != PickUpState.COMPLETED)
                        return;
                    break;
            }

            if (station.CurrentStationType == StationType.COMPLETED)
                station.Interact(GetComponent<PlayerData>().Master);


            if (station.CurrentPickUpObjectType != PickUpObjectType.NONE)
                return;

            _playerMovement.CanMove = false;
            _playerRotation.CanRotate = false;
            _isPlacing = true;

            StartCoroutine(PlaceInStationRoutine(station));
        }

        private IEnumerator PlaceInStationRoutine(WorkstationInteraction station)
        {
            _playerAnimation.StopPickUpObjectAnimation();

            yield return new WaitForSeconds(_playerAnimation.GetPickupAnimDuration());

            station.CurrentPickUpObjectType = _currentPickedUpObject.GetComponent<PickUpComponent>().PickUpObjectType;

            Destroy(_currentPickedUpObject.gameObject);
            _currentPickedUpObject = null;

            _playerMovement.CanMove = true;
            _playerRotation.CanRotate = true;
            _isPlacing = false;
        }

        private void SetWalkingState() => _playerStateMachine.CurrentPlayerState = PlayerState.WALKING;
    }
}