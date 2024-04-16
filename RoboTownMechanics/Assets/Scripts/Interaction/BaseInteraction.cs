using LocalMultiplayer.Player;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

namespace Interaction.Base
{
    public class BaseInteraction : MonoBehaviour, IInteraction
    {
        //--------------------Private--------------------//
        [SerializeField]
        private InterActionType _currentInterActionType;

        private PlayerMaster _playerMaster;

        //--------------------Public--------------------//
        UnityEvent IInteraction.onInteract
        {
            get => OnInteract;
            set => OnInteract = value;
        }

        public PlayerMaster PlayerMaster => _playerMaster;

        public InterActionType CurrentInterActionType => _currentInterActionType;

        [HideInInspector]
        public UnityEvent OnInteract;

        //--------------------Functions--------------------//
        /// <summary>
        /// The interact function which invokes the interact when the raycast hits and "stop" the interaction.
        /// </summary>
        /// <param name="playerMaster">The playerMaster that has interacted</param>
        public void Interact(PlayerMaster playerMaster)
        {
            _playerMaster = playerMaster;

            OnInteract.Invoke();
        }
    }
}

