using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using QuickTime.Handler;

namespace QuickTime.Rhythm
{
    public class RhythmHandler : QuickTimeHandler
    {
        //--------------------Private--------------------//
        [SerializeField] [Tooltip("Controls how many times the player needs to press to complete the QTE")]
        private int _maxCount;
        [SerializeField]
        [Tooltip("Controls how fast the player needs to react")]
        private Vector2Int _tempo;
        [SerializeField]
        private float _targetWidth;
        [SerializeField]
        private float _targetHeight;
        [SerializeField]
        private float _originalWidth;
        [SerializeField]
        private float _originalHeight;
        [SerializeField]
        private float _bufferWidth;
        [SerializeField]
        private float _bufferHeight;
        [SerializeField]
        private Animation ButtonPress;
        [SerializeField]
        private Image _rhythmCirkel;

        private int _rhythmCounter;

        //--------------------Public--------------------//
        public float OriginalWidth
        {
            get => _originalWidth;
            set => _originalWidth = value;
        }
        public float OriginalHeight
        {
            get => _originalHeight;
            set => _originalHeight = value;
        }
        public Image RhythmCirkel
        {
            get => _rhythmCirkel;
            set => _rhythmCirkel = value;
        }
        public int RhythmCounter
        {
            get => _rhythmCounter;
            set => _rhythmCounter = value;
        }

        //--------------------Functions--------------------//
        protected override void Start()
        {
            base.Start();
            _rhythmCounter = 0;
        }

        private void Update()
        {
            if (!QuickTimeActive)
                return;

            MakeRhythm();

            if (_rhythmCirkel.rectTransform.sizeDelta.magnitude <= new Vector2(_targetHeight, _targetWidth).magnitude)
            {
                FailedQuickTime();
            }
        }

        private void MakeRhythm()
        {
            int randomTempo = Random.Range(_tempo.x, _tempo.y);

            _rhythmCirkel.rectTransform.sizeDelta -= new Vector2(_targetWidth, _targetHeight) * randomTempo * Time.deltaTime;

            if (_rhythmCirkel.rectTransform.sizeDelta.magnitude <= new Vector2(_targetWidth, _targetHeight).magnitude)
            {
                FailedQuickTime();
            }
        }

        protected override void ResetQuickTime()
        {
            RhythmCounter = 0;
            RhythmCirkel.rectTransform.sizeDelta = new Vector2(OriginalHeight, OriginalWidth);
            base.ResetQuickTime();
        }

        protected override void InteractPressed(InputAction.CallbackContext context)
        {
            if (_rhythmCirkel.rectTransform.sizeDelta.magnitude >= new Vector2(_targetHeight, _targetWidth).magnitude &&
                _rhythmCirkel.rectTransform.sizeDelta.magnitude <= new Vector2(_bufferHeight, _bufferWidth).magnitude)
            {
                _rhythmCirkel.rectTransform.sizeDelta = new Vector2(_originalWidth, _originalHeight);
                if (_rhythmCirkel.rectTransform.sizeDelta == new Vector2(_originalWidth, _originalHeight))
                {
                    _rhythmCounter = _rhythmCounter + 1;
                    if (_rhythmCounter == _maxCount)
                    {
                        CompletedQuickTime();
                    }
                }
            }
            else if (_rhythmCirkel.rectTransform.sizeDelta.magnitude >= new Vector2(_bufferHeight, _bufferWidth).magnitude)
            {
                FailedQuickTime();
            }
            ButtonPress.Play();
        }
    }
}