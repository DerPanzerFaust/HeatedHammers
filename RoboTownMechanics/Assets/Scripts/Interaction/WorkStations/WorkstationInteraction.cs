using PlayerInteraction.Base;
using UnityEngine;
using Utilities;

namespace WorkstationInteractionBase
{
    public class WorkstationInteraction : BaseInteraction
    {

        //--------------------Private--------------------//
        [SerializeField]
        private StationType _currentStationType;

        private PickUpObjectType _currentPickUpObjectType;

        //--------------------Public--------------------//
        public StationType CurrentStationType => _currentStationType;

        public PickUpObjectType CurrentPickUpObjectType
        {
            get => _currentPickUpObjectType;
            set => _currentPickUpObjectType = value;
        }

        //--------------------Functions--------------------//
        private void Awake() => _onInteract.AddListener(InteractionStart);

        private void OnDisable() => _onInteract.RemoveListener(InteractionStart);

        private void InteractionStart()
        {
            if (_currentPickUpObjectType != PickUpObjectType.NONE)
                OpenQuickTime();
        }


        private void OpenQuickTime()
        {
            _quickTimeCanvas.SetActive(true);
            _quickHandler.SetInputEvents(_playerMaster.PlayerInputComponent);
            _quickHandler.QuickTimeActive = true;
        }
    }

}
