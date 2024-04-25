using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Animation
{
    public class PlayerAnimation : MonoBehaviour
    {
        //--------------------Private--------------------//
        private Animator _animator;


        private float _playerSpeed;
        //--------------------Public--------------------//
        public float PlayerSpeed
        {
            get => _playerSpeed;
            set => _playerSpeed = value;
        }

        //--------------------Functions--------------------//
        private void Start()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            _animator.SetFloat("Speed", _playerSpeed);
        }

        /// <summary>
        /// Starts the lifting animation
        /// </summary>
        public void PickUpObjectAnimation()
        {
            SetCanDoRun(false);
            _animator.SetTrigger("StartLift");
        }

        /// <summary>
        /// Starts the holding lift animation
        /// </summary>
        public void HoldObjectAnimation()
        {
            SetCanDoRun(false);
            _animator.SetTrigger("HoldLift");
        }

        /// <summary>
        /// sets the stoplift trigger
        /// </summary>
        public void StopPickUpObjectAnimation()
        {
            SetCanDoRun(true);
            _animator.SetTrigger("StopLift");
        }

        private void SetCanDoRun(bool _bool) => _animator.SetBool("CanDoRun", _bool);

        /// <summary>
        /// gets the duration of the pickupanimation
        /// </summary>
        /// <returns>a float value of the duration</returns>
        public float GetPickupAnimDuration()
        {
            return .833f;
        }
    }
}

