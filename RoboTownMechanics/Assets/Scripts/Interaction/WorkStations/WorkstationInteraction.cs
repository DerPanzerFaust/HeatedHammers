using Player.StateMachine;
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

        private PickUpObjectType _currentPickUpObjectType;

        //--------------------Protected--------------------//
        [SerializeField]
        protected GameObject _quickTimeCanvas;
        
        protected QuickTimeHandler _quickHandler;

        //--------------------Public--------------------//
        public StationType CurrentStationType => _currentStationType;

        public PickUpObjectType CurrentPickUpObjectType
        {
            get => _currentPickUpObjectType;
            set => _currentPickUpObjectType = value;
        }

        //--------------------Functions--------------------//
        private void Awake() => OnDrop.AddListener(InteractionStart);

        private void Start() => _quickHandler = GetComponent<QuickTimeHandler>();

        private void OnDisable() => OnDrop.RemoveListener(InteractionStart);

        protected virtual void InteractionStart()
        {
            if (_currentPickUpObjectType != PickUpObjectType.NONE)
                SpecialAction();
            else
                PlayerMaster.CurrentActivePlayerModel.GetComponent<PlayerStateMachine>().CurrentPlayerState = PlayerState.WALKING;
        }

        protected virtual void SpecialAction()
        {
        }
    }
}
