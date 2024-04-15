using LocalMultiplayer.Player;
using QuickTime.Handler;
using UnityEngine;
using UnityEngine.Events;


namespace WorkstationInteractionBase
{
    public class WorkstationInteraction : MonoBehaviour, IInteraction
    {

        //--------------------Private--------------------//
        [SerializeField]
        private bool _isOn;
        [SerializeField]
        private GameObject _quickTimeCanvas;
        [SerializeField]
        private QuickTimeHandler _quickHandler;
        private PlayerMaster _playerMaster;

        //--------------------Public--------------------//
        [HideInInspector]
        public UnityEvent OnStopInteract;
        [HideInInspector]
        public UnityEvent OnInteract;

        public bool IsOn
        {
            get => _isOn;
            set => _isOn = value;
        }
        public PlayerMaster PlayerMaster
        {
            get => _playerMaster;
            set => _playerMaster = value;
        }
        UnityEvent IInteraction.onInteract
        {
            get => OnInteract;
            set => OnInteract = value;
        }

        //--------------------Functions--------------------//
        private void Awake() => OnInteract.AddListener(OpenQuickTime);

        private void OnDisable() => OnInteract.RemoveListener(OpenQuickTime);

        /// <summary>
        /// The interact function which invokes the interact when the raycast hits and "stop" the interaction.
        /// </summary>
        public void Interact(PlayerMaster playerMaster)
        {
            _playerMaster = playerMaster;

            if (!_isOn)
            {
                OnInteract.Invoke();
            }
            else
            {
                OnStopInteract.Invoke();
            }

            _isOn = !_isOn;
        }

        private void OpenQuickTime()
        {
            _quickTimeCanvas.SetActive(true);
            _quickHandler.SetInputEvents(_playerMaster.PlayerInputComponent);
            _quickHandler.QuickTimeActive = true;
        }
    }

}