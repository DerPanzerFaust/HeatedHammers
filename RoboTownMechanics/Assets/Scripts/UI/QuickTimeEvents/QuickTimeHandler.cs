using System;
using System.Collections.Generic;
using UnityEngine;
using Interaction.Workstations;
using Player.StateMachine;
using InputNameSpace;
using UnityEngine.InputSystem;

namespace QuickTime.Handler
{
    public class QuickTimeHandler : MonoBehaviour
    {
        //--------------------Private--------------------//
        [SerializeField]
        private GameObject _quickTimeObject;

        private WorkstationInteraction _workstationInteraction;
        
        [SerializeField]
        private Transform _partSpawnLocation;
        [SerializeField]
        private List<GameObject> _parts;
        private InputComponent _inputComponent;
        private PlayerStateMachine _currentPlayerState;

        private bool _quickTimeActive;

        //--------------------Public--------------------//
        public Action OnFailQuickTime;

        public GameObject QuickTimeObject
        {
            get => _quickTimeObject;
            set => _quickTimeObject = value;
        }
        public InputComponent InputComponent
        {
            get => _inputComponent;
            set => _inputComponent = value;
        }

        public bool QuickTimeActive
        {
            get => _quickTimeActive;
            set => _quickTimeActive = value;
        }

        //--------------------Functions--------------------//
        protected virtual void Start() => _workstationInteraction = GetComponent<WorkstationInteraction>();

        private void OnDisable()
        {
            OnFailQuickTime -= FailedQuickTime;
            _inputComponent = null;
        }

        /// <summary>
        /// Sets the references for the input
        /// </summary>
        /// <param name="inputComponent"></param>
        public void SetInputEvents(InputComponent inputComponent)
        {
            _inputComponent = inputComponent;

            _inputComponent.OnInteractInputAction.performed += InteractPressed;
            OnFailQuickTime += FailedQuickTime;
        }

        protected virtual void InteractPressed(InputAction.CallbackContext context)
        {

        }

        /// <summary>
        /// When QTE is failed do ResetQuickTime and launch player from workstation 
        /// </summary>
        public void FailedQuickTime()
        {
            ResetQuickTime();
            LaunchPlayer();
        }

        /// <summary>
        /// When QTE is completed do ResetQuickTime and spawn object to repair robot 
        /// </summary>
        public void CompletedQuickTime()
        {
            ResetQuickTime();
            SpawnPart();
        }

        protected virtual void ResetQuickTime()
        {
            _quickTimeActive = false;
            _currentPlayerState = _workstationInteraction.PlayerMaster.CurrentActivePlayerModel.GetComponent<PlayerStateMachine>();
            _currentPlayerState.CurrentPlayerState = Utilities.PlayerState.WALKING;
            _workstationInteraction.CurrentPickUpObjectType = Utilities.PickUpObjectType.NONE;

            if (_inputComponent != null)
                _inputComponent.OnInteractInputAction.performed -= InteractPressed;

            _quickTimeObject.SetActive(false);
        }

        /// <summary>
        /// Spawn part needed to repair the robot. To spawn a part there must always a transform where the part can spawn
        /// </summary>
        public void SpawnPart()
        {
            Instantiate(_parts[UnityEngine.Random.Range(0, _parts.Count)], 
            _partSpawnLocation.position, Quaternion.identity);
        }

        /// <summary>
        /// Launch player from workstation
        /// </summary>
        public void LaunchPlayer()
        {
        }
    }
}