using PickUps;
using PlayerInteraction.Pickup;
using UnityEngine;
using Utilities;
using WorkstationInteractionBase;

namespace Player.PickUp
{
    public class PlayerPickUp : MonoBehaviour
    {
        //--------------------Private--------------------//
        private PickUpInteraction _currentPickedUpObject;

        //--------------------Public--------------------//
        public PickUpInteraction CurrentPickedUpObject => _currentPickedUpObject;

        //--------------------Functions--------------------//
        /// <summary>
        /// When this function is called, the player will pick up the given object
        /// </summary>
        /// <param name="pickUpObject">The object to PickUp</param>
        public void PickUpObject(PickUpInteraction pickUpObject)
        {
            if (_currentPickedUpObject != null)
                return;

            pickUpObject.transform.parent = transform;
            pickUpObject.transform.position = transform.position + Vector3.up;

            _currentPickedUpObject = pickUpObject;
        }

        /// <summary>
        /// When this function is called, the object the player is holding will be put into the given station
        /// </summary>
        /// <param name="station">The station to place object inside of</param>
        public void PlaceInStation(WorkstationInteraction station)
        {
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

            //when nothing is in the station
            if (station.CurrentPickUpObjectType != PickUpObjectType.NONE)
                return;

            station.CurrentPickUpObjectType = _currentPickedUpObject.GetComponent<PickUpComponent>().PickUpObjectType;

            Destroy(_currentPickedUpObject.gameObject);
            _currentPickedUpObject = null;
        }
    }
}