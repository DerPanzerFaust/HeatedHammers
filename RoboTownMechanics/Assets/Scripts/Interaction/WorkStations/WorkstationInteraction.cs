using QuickTime.Handler;
using UnityEngine;
using Utilities;
using Interaction.Base;
using Robot.List;
using System.Collections.Generic;
using PartUtilities.Route;

namespace Interaction.Workstations
{
    public class WorkstationInteraction : BaseInteraction
    {

        //--------------------Private--------------------//
        [SerializeField]
        private StationType _currentStationType;

        [SerializeField]
        private WorkStation _station;

        private PickUpObjectType _currentPickUpObjectType;

        private GameObject _pickUpGameObjectReference;
        [SerializeField]
        private List<GameObject> _parts;

        [SerializeField]
        private Transform _partSpawnLocation;

        //--------------------Protected--------------------//
        [SerializeField]
        protected GameObject _quickTimeCanvas;
        
        protected QuickTimeHandler _quickHandler;

        //--------------------Public--------------------//
        public StationType CurrentStationType => _currentStationType;

        public WorkStation Station => _station;
        
        public PickUpObjectType CurrentPickUpObjectType
        {
            get => _currentPickUpObjectType;
            set => _currentPickUpObjectType = value;
        }

        public GameObject PickUpGameObjectReference
        {
            get => _pickUpGameObjectReference;
            set => _pickUpGameObjectReference = value;
        }

        //--------------------Functions--------------------//
        private void Awake() => OnInteract.AddListener(InteractionStart);

        private void Start() => _quickHandler = GetComponent<QuickTimeHandler>();

        private void OnDisable() => OnInteract.RemoveListener(InteractionStart);

        protected virtual void InteractionStart()
        {
            SpecialAction();
        }

        protected virtual void SpecialAction()
        {
        }

        public void SpawnPart()
        {
            PartRoute partRoute = _pickUpGameObjectReference.GetComponent<PartRoute>();

            if (!partRoute.CanCompleteStation())
            {
                Instantiate(_parts[Random.Range(0, _parts.Count)], 
                _partSpawnLocation.position, Quaternion.identity);

                Destroy(_pickUpGameObjectReference);
            }
            else
            {
                _pickUpGameObjectReference.transform.position = _partSpawnLocation.position;
                _pickUpGameObjectReference.transform.rotation = _partSpawnLocation.rotation;

                _pickUpGameObjectReference.SetActive(true);
            }
        }
    }
}
