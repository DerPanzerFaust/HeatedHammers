using QuickTime.Rhythm;
using UnityEngine;
using WorkstationInteractionBase;

namespace QuickTime.Handler
{
    public class QuickTimeHandler : MonoBehaviour
    {
        [SerializeField]
        private GameObject _rhythmQuickTime;
        [SerializeField]
        private GameObject _rhythmQuickTimeCanvas;
        [SerializeField]
        private RhythmHandler _rhythmHandler;
        [SerializeField]
        private WorkstationInteraction _workstationInteraction;
        // Location to spawn objects for robot

        public GameObject RhythmQuickTimeCanvas
        {
            get => _rhythmQuickTimeCanvas;
            set => _rhythmQuickTimeCanvas = value;
        }

        public void FailedQuickTime()
        {
            ResetQuicktTime();
            // Close QTE, reset QTE and launch player from workstation
        }

        public void CompletedQuickTime()
        {
            ResetQuicktTime();
            // Close QTE and spawn object for robot
        }

        public void ResetQuicktTime()
        {
            if (_workstationInteraction.IsOn == false)
            {
                _rhythmQuickTimeCanvas.SetActive(false);
                _rhythmHandler.RhythmCounter = 0;
                _rhythmHandler.RhythmCirkel.rectTransform.sizeDelta = new Vector2(_rhythmHandler.OriginalHeight, _rhythmHandler.OriginalWidth);
            }
            // Stop Interacting
            // Reset currentHeight/Width to orignalHeight/Width and enable rhythmQTE object
            // Change interaction to match InputComponent
        }
    }

}