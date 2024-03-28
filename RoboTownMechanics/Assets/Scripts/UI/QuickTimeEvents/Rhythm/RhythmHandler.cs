using InputNameSpace;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using QuickTime.Handler;
using Unity.VisualScripting;

namespace QuickTime.Rhythm
{
    public class RhythmHandler : MonoBehaviour
    {
        //--------------------Private--------------------//
        [SerializeField] [Tooltip("Controls how many times the player needs to press to complete the QTE")]
        private int _maxCount;
        [SerializeField]
        [Tooltip("Controls how fast the player needs to react")]
        private float _tempo;
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
        private QuickTimeHandler _quickTimeHandler;
        [SerializeField]
        private Image _rhythmCirkel;

        private int _rhythmCounter;
        private InputComponent _inputComponent;

        //--------------------Public--------------------//
        public Action OnFailQuickTime;
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
        private void Start()
        { 

            _rhythmCounter = 0;

            _inputComponent = InputComponent.Instance;
            _inputComponent.Interact.performed += InteractPressed;

            OnFailQuickTime += _quickTimeHandler.FailedQuickTime;
        }

        private void Update()
        {
            MakeRhythm();

            if (_rhythmCirkel.rectTransform.sizeDelta.magnitude <= new Vector2(_targetHeight, _targetWidth).magnitude)
            {
                _quickTimeHandler.FailedQuickTime();
            }
        }

        private void MakeRhythm()
        {
            _rhythmCirkel.rectTransform.sizeDelta -= new Vector2(_targetWidth, _targetHeight) * _tempo * Time.deltaTime;

            if (_rhythmCirkel.rectTransform.sizeDelta.magnitude <= new Vector2(_targetWidth, _targetHeight).magnitude)
            {
                _quickTimeHandler.FailedQuickTime();
                OnFailQuickTime.Invoke();
            }
        }

        private void InteractPressed(InputAction.CallbackContext context)
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
                        _quickTimeHandler.CompletedQuickTime();
                    }
                }
            }
            else if (_rhythmCirkel.rectTransform.sizeDelta.magnitude >= new Vector2(_bufferHeight, _bufferWidth).magnitude)
            {
                _quickTimeHandler.FailedQuickTime();
            }

            ButtonPress.Play();
        }
    }
}