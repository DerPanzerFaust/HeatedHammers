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

        public bool IsOn
        {
            get => _isOn;
            set => _isOn = value;
        }

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
    }

}