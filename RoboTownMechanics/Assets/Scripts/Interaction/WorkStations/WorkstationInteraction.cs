using LocalMultiplayer.Player;
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
        private UnityEvent _stopInteract;
        [SerializeField]
        private UnityEvent _onInteract;
        [SerializeField]
        private GameObject _quickTimeCanvas;
        
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

        //--------------------Functions--------------------//
        private void Awake() => _onInteract.AddListener(OpenQuickTime);

        private void OnDisable() => _onInteract.RemoveListener(OpenQuickTime);

        /// <summary>
        /// The interact function which invokes the interact when the raycast hits and "stop" the interaction.
        /// </summary>
        public void Interact(PlayerMaster playerMaster)
        {
            if (!_isOn)
            {
                _onInteract.Invoke();
            }
            else
            {
                _stopInteract.Invoke();
            }

            _isOn = !_isOn;
        }

        private void OpenQuickTime()
        {
            //logica voor openen canvas qt
        }
    }

}