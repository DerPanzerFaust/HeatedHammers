using InputNameSpace;
using LocalMultiplayer.Player;
using Player.PickUp;
using Player.StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;
using Interaction.Workstations;
using System.Collections.Generic;
using Player.Drop;
using PickUps;
using static UnityEngine.InputSystem.InputAction;
using PartUtilities.Route;
using Robot.List;


namespace Interaction.Base
{
    public class Interactor : MonoBehaviour
    {
        //--------------------Private--------------------//
        [SerializeField]
        private LayerMask _interatableLayer;

        [SerializeField]
        private float _interactRange;
        [SerializeField]
        private float _fieldOfViewAngle;

        private InputComponent _playerInput;

        private PlayerStateMachine _playerStateMachine;

        private PlayerMaster _playerMaster;

        private PlayerPickUp _playerPickUp;

        private PlayerDrop _playerDrop;

        public bool _pickingUp;

        public float _charge;

        private float _maxCharge = .5f;

        private float _timeHeldDown;

        private BotPartList _botPartList;

        //--------------------Public--------------------//
        public float InteractionRange => _interactRange;
        public float FieldOfViewAngler => _fieldOfViewAngle;

        //--------------------Functions--------------------//
        private void Start()
        {
            _playerInput = GetComponent<PlayerData>().Master.PlayerInputComponent;

            _playerMaster = GetComponent<PlayerData>().Master;

            _playerStateMachine = GetComponent<PlayerStateMachine>();

            _playerPickUp = GetComponent<PlayerPickUp>();

            _playerDrop = GetComponent<PlayerDrop>();

            _playerInput.OnInteractInputAction.canceled += DoInteract;

        }

        private void Update()
        {
            if (_charge < _maxCharge && _playerInput.OnInteractInputAction.IsPressed() && _playerPickUp.CurrentPickedUpObject != null)
                ThrowCharge();
            _botPartList = BotPartList.Instance;

            _playerInput.OnInteractInputAction.performed += DoInteract;
        }

        private void OnDisable() => _playerInput.OnInteractInputAction.performed -= DoInteract;

        
        private void DoInteract(InputAction.CallbackContext callbackContext) 
        {
            if (_playerStateMachine.CurrentPlayerState != PlayerState.WALKING)
                return;

            BaseInteraction interactable = GetTopPriorityInteractionObject();

            if(interactable == null)
            {
                if (_playerPickUp.CurrentPickedUpObject == null)
                {
                    return;
                }
                else
                {
                    if (_charge >= _maxCharge)
                    {
                        _playerDrop.ThrowObject(_playerPickUp.CurrentPickedUpObject);
                        _charge = 0;
                        return;
                    }
                    else
                    {
                        _playerDrop.DropObject(_playerPickUp.CurrentPickedUpObject);
                        return;
                    }
                }
            }

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
            

            //when interacting with station
            if (interactable.CurrentInterActionType == InterActionType.WORKSTATION)
            {
                WorkstationInteraction workstationInteraction = (WorkstationInteraction)interactable;

                //any other station
                if (_playerPickUp.CurrentPickedUpObject != null)
                {
                    //if it is not the station to be used in the route do nothing
                    if (_playerPickUp.CurrentPickedUpObject.CurrentPickUpState != PickUpState.COMPLETED)
                        if (!_playerPickUp.CurrentPickedUpObject.GetComponent<PartRoute>().IsCorrectStation(workstationInteraction.Station))
                            return;

                    _playerPickUp.PlaceInStation(workstationInteraction);
                }

                //completed station and nothing in the hand
                else if (_playerPickUp.CurrentPickedUpObject == null
                && workstationInteraction.CurrentStationType == StationType.COMPLETED)
                {
                    BaseInteraction interactableFromRobot = _botPartList.GetPart(transform);

                    if (interactableFromRobot == null)
                        return;

                    _playerStateMachine.CurrentPlayerState = PlayerState.INTERACTING;
                    interactableFromRobot.Interact(_playerMaster);
                }
            }

            //when interacting with PickUpObject
            else if (interactable.CurrentInterActionType == InterActionType.PICKUP)
            {
                _playerStateMachine.CurrentPlayerState = PlayerState.INTERACTING;
                interactable.Interact(_playerMaster);
            }
        }

        /// <summary>
        /// Returns the direction from the angle given
        /// </summary>
        /// <param name="angleInDegrees">The angle you want to get the direction from</param>
        /// <param name="angleIsGlobal">if you want the direction global or not</param>
        /// <returns></returns>
        public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
        {
            if (!angleIsGlobal)
                angleInDegrees += transform.eulerAngles.y;

            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }

        private bool IsInViewAngle(Transform transformToCheck)
        {
            Vector3 direction = (transformToCheck.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, direction) < _fieldOfViewAngle / 2)
                return true;
            else
                return false;
        }
        
        /// <summary>
        /// Handles to closest interactable object and gives that object the highest priority to interact with.
        /// When no interactables are in range return null.
        /// </summary>
        public BaseInteraction GetTopPriorityInteractionObject()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _interactRange, _interatableLayer);

            List<BaseInteraction> pickUps = new();
            List<BaseInteraction> nonPickUps = new();

            List<GameObject> hitObjects = new();

            if(hitColliders.Length <= 0)
                return null;

            foreach (Collider hitCollider in hitColliders)
            {
                if (!IsInViewAngle(hitCollider.transform))
                    continue;

                if (_playerPickUp.CurrentPickedUpObject != null && hitCollider.gameObject == _playerPickUp.CurrentPickedUpObject)
                    continue;

                else if (hitObjects.Contains(hitCollider.gameObject))
                    continue;

                BaseInteraction baseInteraction = hitCollider.gameObject.GetComponent<BaseInteraction>();

                if (baseInteraction.CurrentInterActionType == InterActionType.PICKUP)
                    pickUps.Add(baseInteraction);
                else
                    nonPickUps.Add(baseInteraction);

                hitObjects.Add(hitCollider.gameObject);
            }

            if(pickUps.Count > 0)
                return GetClosestObject(pickUps);

            else
                return GetClosestObject(nonPickUps);
        }

        private BaseInteraction GetClosestObject(List<BaseInteraction> objects)
        {
            float shortestDistance = _interactRange + .5f;
            BaseInteraction closestObject = null;

            foreach (BaseInteraction baseInteraction in objects)
            {
                float distance = Vector3.Distance(transform.position, baseInteraction.transform.position);

                if(distance < shortestDistance)
                {
                    shortestDistance = distance;
                    closestObject = baseInteraction;
                }
            }

            return closestObject;
        }

        private void ThrowCharge()
        { 
            _charge += Time.deltaTime;
        }
    }
}
