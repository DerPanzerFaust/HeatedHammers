using Player.PickUp;
using PlayerInteraction.Base;
using UnityEngine;
using Utilities;

namespace PlayerInteraction.Pickup
{
    public class PickUpInteraction : BaseInteraction
    {
        //--------------------Private--------------------//
        [SerializeField]
        private PickUpState _currentPickUpState;

        //--------------------Public--------------------//
        public PickUpState CurrentPickUpState => _currentPickUpState;

        //--------------------Functions--------------------//
        private void Awake()
        {
            _onInteract.AddListener(PickUp);
        }

        private void OnDisable()
        {
            _onInteract.RemoveListener(PickUp);
        }

        private void PickUp()
        {
            PlayerMaster.CurrentActivePlayerModel.GetComponent<PlayerPickUp>()?.PickUpObject(this);
            IsOn = false;
        }
    }
}

