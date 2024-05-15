using QuickTime.Handler;
using UnityEngine;
using Utilities;
using Interaction.Base;

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
    }
}
