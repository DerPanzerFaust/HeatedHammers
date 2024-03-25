using UnityEngine;
using UnityEngine.Events;


namespace WorkstationInteractionBase
{
    public class WorkstationInteraction : MonoBehaviour, IInteraction
    {

        //--------------------Private--------------------//

        private bool _isOn;

        [SerializeField]
        private UnityEvent _stopInteract;

        [SerializeField]
        private UnityEvent _onInteract;

        //--------------------Functions--------------------//
        UnityEvent IInteraction.onInteract
        {
            get => _onInteract;
            set => _onInteract = value;
        }


        /// <summary>
        /// The interact function which invokes the interact when the raycast hits and "stop" the interaction.
        /// </summary>

        public void Interact()
        {
            if (_isOn)
            {
                _stopInteract.Invoke();
            }
            else
            {
                _onInteract.Invoke();
            }

            _isOn = !_isOn;
        }
    }

}