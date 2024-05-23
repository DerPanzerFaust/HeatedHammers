using QuickTime.Handler;
using UnityEngine;
using Utilities;
using Interaction.Base;
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
        private GameObject _completedPart;

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

        /// <summary>
        /// This function spawns a part on the part spawn location
        /// </summary>
        public void SpawnPart()
        {
            PartRoute partRoute = _pickUpGameObjectReference.GetComponent<PartRoute>();

            if (!partRoute.CanCompleteStation())
            {
                Instantiate(_completedPart, _partSpawnLocation.position, Quaternion.identity);

                _pickUpGameObjectReference.SetActive(true);
                Destroy(_pickUpGameObjectReference);
            }
            else
            {
                _pickUpGameObjectReference.SetActive(true);
                _pickUpGameObjectReference.transform.SetParent(null);

                _pickUpGameObjectReference.transform.position = _partSpawnLocation.position;
                _pickUpGameObjectReference.transform.rotation = _partSpawnLocation.rotation;
            }
        }
    }
}
