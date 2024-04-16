using InputNameSpace;
using LocalMultiplayer.Player;
using Player.PickUp;
using Player.StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;
using Interaction.Workstations;

namespace Interaction.Base
{
    public class Interactor : MonoBehaviour
    {
        //--------------------Private--------------------//

        [SerializeField]
        private LayerMask _interatableLayer;

        [SerializeField]
        private float _interactRange;

        private InputComponent _playerInput;
        private Transform _transform;

        private PlayerStateMachine _playerStateMachine;

        private PlayerMaster _playerMaster;

        private PlayerPickUp _playerPickUp;

        //--------------------Functions--------------------//
        private void Start()
        {
            _playerInput = GetComponent<PlayerData>().Master.PlayerInputComponent;

            _playerMaster = GetComponent<PlayerData>().Master;

            _playerStateMachine = GetComponent<PlayerStateMachine>();

            _playerPickUp = GetComponent<PlayerPickUp>();

            _playerInput.OnInteractInputAction.performed += DoInteract;
            _transform = transform;
        }

        private void OnDisable()
        {
            _playerInput.OnInteractInputAction.performed += DoInteract;
        }


        private void DoInteract(InputAction.CallbackContext callbackContext)
        {
            if (_playerStateMachine.CurrentPlayerState != PlayerState.WALKING)
                return;

            if (!Physics.Raycast(_transform.position + (Vector3.up * 0.3f) + (_transform.forward * 0.2f),
                _transform.forward, out var hit, _interactRange, _interatableLayer))
                return;

            if (!hit.transform.TryGetComponent(out BaseInteraction interactable))
                return;

            if (_playerPickUp.CurrentPickedUpObject != null
                && interactable.CurrentInterActionType == InterActionType.WORKSTATION)
            {
                WorkstationInteraction workstationInteraction = (WorkstationInteraction)interactable;
                _playerPickUp.PlaceInStation(workstationInteraction);
            }
            else
            {
                _playerStateMachine.CurrentPlayerState = PlayerState.INTERACTING;
                interactable.Interact(_playerMaster);
            }
        }
    }
}
