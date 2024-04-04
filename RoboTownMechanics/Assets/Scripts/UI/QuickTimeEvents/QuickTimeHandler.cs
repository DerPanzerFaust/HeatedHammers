using QuickTime.Rhythm;
using System.Collections.Generic;
using UnityEngine;
using WorkstationInteractionBase;

namespace QuickTime.Handler
{
    public class QuickTimeHandler : MonoBehaviour
    {
        //--------------------Private--------------------//
        //[SerializeField]
        //private GameObject _rhythmQuickTime;
        [SerializeField]
        private GameObject _rhythmQuickTimeCanvas;
        [SerializeField]
        private RhythmHandler _rhythmHandler;
        [SerializeField]
        private WorkstationInteraction _workstationInteraction;
        [SerializeField]
        private Transform _partSpawnLocation;
        [SerializeField]
        private List<GameObject> _parts;

        //--------------------Public--------------------//
        public GameObject RhythmQuickTimeCanvas
        {
            get => _rhythmQuickTimeCanvas;
            set => _rhythmQuickTimeCanvas = value;
        }

        public RhythmHandler RhythmHandlerRef
        {
            get => _rhythmHandler;
            set => _rhythmHandler = value;
        }

        //--------------------Functions--------------------//

        /// <summary>
        /// When QTE is failed do ResetQuickTime and launch player from workstation 
        /// </summary>
        public void FailedQuickTime()
        {
            ResetQuicktTime();
            LaunchPlayer();
        }

        /// <summary>
        /// When QTE is completed do ResetQuickTime and spawn object to repair robot 
        /// </summary>
        public void CompletedQuickTime()
        {
            ResetQuicktTime();
            SpawnPart();
        }

        /// <summary>
        /// Resets Canvas, Image, Counter to original size and count. 
        /// </summary>..
        public void ResetQuicktTime()
        {
            if (!_workstationInteraction.IsOn)
            {
                _rhythmQuickTimeCanvas.SetActive(false);
                _rhythmHandler.RhythmCounter = 0;
                _rhythmHandler.RhythmCirkel.rectTransform.sizeDelta = new Vector2(_rhythmHandler.OriginalHeight, _rhythmHandler.OriginalWidth);
            }
            // Stop Interacting
            // Reset currentHeight/Width to orignalHeight/Width and enable rhythmQTE object
            // Change interaction to match InputComponent
        }

        /// <summary>
        /// Spawn part needed to repair the robot. To spawn a part there must always a transform where the part can spawn
        /// </summary>
        public void SpawnPart()
        {
            Instantiate(_parts[UnityEngine.Random.Range(0, _parts.Count)], _partSpawnLocation);
        }

        /// <summary>
        /// Launch player from workstation
        /// </summary>
        public void LaunchPlayer()
        {
            Debug.Log("Player Launched");
        }
    }

}