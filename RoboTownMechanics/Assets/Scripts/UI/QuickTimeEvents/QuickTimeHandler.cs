using QuickTime.Rhythm;
using UnityEngine;

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
        /*[SerializeField]
        private GameObject _rhythmQuickTimeObject;*/

        public void FailedQuickTime()
        {
            Debug.Log("Failed!");
            ResetQuicktTime();
            _rhythmQuickTimeCanvas.SetActive(false);
            // Close QTE, reset QTE and launch player from workstation
        }

        public void CompletedQuickTime()
        {
            Debug.Log("Win!");
            ResetQuicktTime();
            _rhythmQuickTimeCanvas.SetActive(false);
            // Close QTE and spawn object for robot
        }

        public void ResetQuicktTime()
        {
            // reset currentHeight/Width to orignalHeight/Width and enable rhythmQTE object
        }
    }

}