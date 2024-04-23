using UnityEngine;
using TimerHandler;
using UnityEngine.UI;

namespace TimeSlide.Handler
{
    public class TimeSliderHandler : MonoBehaviour
    {
        //--------------------Private--------------------//
        private GameTimer _gameTimer;

        private Slider _slider;

        //--------------------Functions--------------------//
        private void Start()
        {
            _gameTimer = GameTimer.Instance;

            _slider = GetComponent<Slider>();

            _gameTimer.OnCurrentTimeChanged += CurrentTimeChanged;
        }

        private void OnDisable() => _gameTimer.OnCurrentTimeChanged -= CurrentTimeChanged;

        private void CurrentTimeChanged(float currentTime)
        {
            float percentage = (currentTime / _gameTimer.GameLength) * 100;
            
            percentage = 100 - percentage;

            _slider.value = percentage;
        }
    }
}