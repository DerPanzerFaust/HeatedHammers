using InputNameSpace;
using LocalMultiplayer.Player;
using Player.StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;
using WorkstationInteractionBase;


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

        //--------------------Functions--------------------//
        private void Start()
        {
            _playerInput = GetComponent<PlayerData>().Master.PlayerInputComponent;

            _playerMaster = GetComponent<PlayerData>().Master;
            
            _playerStateMachine = GetComponent<PlayerStateMachine>();

            _playerInput.OnInteractInputAction.performed += DoInteract;
            _transform = transform;
        }

        private void OnDisable()
        {
            _playerInput.OnInteractInputAction.performed += DoInteract;
        }


        private void DoInteract(InputAction.CallbackContext callbackContext)
        {
            if (_playerStateMachine.CurrentPlayerState != Utilities.PlayerState.WALKING)
                return;

            if (!Physics.Raycast(_transform.position + (Vector3.up * 0.3f) + (_transform.forward * 0.2f),
                _transform.forward, out var hit, _interactRange, _interatableLayer)) return;
            if (!hit.transform.TryGetComponent(out WorkstationInteraction interactable)) return;
            
            interactable.Interact(_playerMaster);
            _playerStateMachine.CurrentPlayerState = Utilities.PlayerState.INTERACTING;
        }
    }