using System.Collections.Generic;
using UnityEngine;
using TimerHandler;

namespace Bomb.ImageChanger
{
    public class BombImageChanger : SingletonBehaviour<BombImageChanger>
    {
        //--------------------Private--------------------//
        private GameTimer _gameTimer;

        [SerializeField]
        private float _firstStagePercentage;
        [SerializeField]
        private float _secondStagePercentage;
        private float _currentPercentage;

        private bool _firstStageReached;
        private bool _secondStageReached;

        [SerializeField]
        private List<GameObject> _bombImageObjects;

        //--------------------Public--------------------//
        public List<GameObject> BombImageObjects
        {
            get => _bombImageObjects;
            set => _bombImageObjects = value;
        }

        //--------------------Functions--------------------//
        private void Start()
        {
            _gameTimer = GameTimer.Instance;
            
            _gameTimer.OnCurrentTimeChanged += CheckGameTime;
        }

        private void OnDisable() => _gameTimer.OnCurrentTimeChanged -= CheckGameTime;

        private void CheckGameTime(float currentTime)
        {
            _currentPercentage = (currentTime / _gameTimer.GameLength) * 100;

            if( _currentPercentage >= _firstStagePercentage && !_firstStageReached) 
            { 
                _firstStageReached = true;
                ChangeImage(_bombImageObjects[1]);
            }
            else if(_currentPercentage >= _secondStagePercentage && !_secondStageReached)
            {
                _secondStageReached = true;
                ChangeImage(_bombImageObjects[2]);
            }
        }

        /// <summary>
        /// Changes the Image object active to the given object
        /// </summary>
        /// <param name="gameObjectToActivate">The object to set active</param>
        public void ChangeImage(GameObject gameObjectToActivate)
        {
            foreach (GameObject imageObject in _bombImageObjects)
            {
                if(imageObject == gameObjectToActivate)
                    imageObject.SetActive(true);
                else
                    imageObject.SetActive(false);
            }
        }
    }
}

