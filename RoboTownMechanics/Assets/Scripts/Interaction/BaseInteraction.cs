using LocalMultiplayer.Player;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

namespace PlayerInteraction.Base
{
    public class BaseInteraction : MonoBehaviour, IInteraction
    {
        //--------------------Private--------------------//
        [SerializeField]
        private bool _isOn;
        
        [SerializeField]
        private InterActionType _currentInterActionType;

        private PlayerMaster _playerMaster;

        //--------------------Public--------------------//
        public bool IsOn
        {
            get => _isOn;
            set => _isOn = value;
        }

        UnityEvent IInteraction.onInteract
        {
            get => _onInteract;
            set => _onInteract = value;
        }

        public PlayerMaster PlayerMaster => _playerMaster;

        public InterActionType CurrentInterActionType => _currentInterActionType;

        [HideInInspector]
        public UnityEvent _onStopInteract;
        [HideInInspector]
        public UnityEvent _onInteract;

        //--------------------Functions--------------------//
        /// <summary>
        /// The interact function which invokes the interact when the raycast hits and "stop" the interaction.
        /// </summary>
        /// <param name="playerMaster">The playerMaster that has interacted</param>
        public void Interact(PlayerMaster playerMaster)
        {
            _playerMaster = playerMaster;

            if (!_isOn)
            {
                _onInteract.Invoke();
            }
            else
            {
                _onStopInteract.Invoke();
            }

            _isOn = !_isOn;
        }
    }
}

