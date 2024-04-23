using PickUps;
using Player.StateMachine;
using UnityEngine;
using Utilities;
using Interaction.Workstations;
using LocalMultiplayer.Player;
using System.Xml.Serialization;

namespace Player.PickUp
{
    public class PlayerPickUp : MonoBehaviour
    {
        //--------------------Private--------------------//
        private PickUpComponent _currentPickedUpObject;

        private PlayerStateMachine _playerStateMachine;

        //--------------------Public--------------------//
        public PickUpComponent CurrentPickedUpObject
            {
            get => _currentPickedUpObject;
            set => _currentPickedUpObject = value;
            }
            
            

        //--------------------Functions--------------------//
        private void Awake() => _playerStateMachine = GetComponent<PlayerStateMachine>();

        /// <summary>
        /// When this function is called, the player will pick up the given object
        /// </summary>
        /// <param name="pickUpObject">The object to PickUp</param>
        public void PickUpObject(PickUpComponent pickUpObject)
        {
            SetWalkingState();

            if (_currentPickedUpObject != null)
                return;

            pickUpObject.transform.rotation = transform.rotation;
            pickUpObject.transform.position = transform.position + Vector3.up;
            pickUpObject.transform.SetParent(transform, true);
            
            _currentPickedUpObject = pickUpObject;
        }

        /// <summary>
        /// When this function is called, the object the player is holding will be put into the given station
        /// </summary>
        /// <param name="station">The station to place object inside of</param>
        public void PlaceInStation(WorkstationInteraction station)
        {
            SetWalkingState();

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

            //when nothing is in the station
            if (station.CurrentPickUpObjectType != PickUpObjectType.NONE)
                return;

            station.CurrentPickUpObjectType = _currentPickedUpObject.GetComponent<PickUpComponent>().PickUpObjectType;

            Destroy(_currentPickedUpObject.gameObject);
            _currentPickedUpObject = null;
        }

        private void SetWalkingState() => _playerStateMachine.CurrentPlayerState = PlayerState.WALKING;


    }
}