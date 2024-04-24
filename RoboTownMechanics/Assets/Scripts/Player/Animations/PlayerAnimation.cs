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

        public void PickUpObjectAnimation()
        {
            SetCanDoRun(false);
            _animator.SetTrigger("StartLift");
        }

        public void HoldObjectAnimation()
        {
            SetCanDoRun(false);
            _animator.SetTrigger("HoldLift");
        }

        public void StopPickUpObjectAnimation()
        {
            SetCanDoRun(true);
            _animator.SetTrigger("StopLift");
        }

        private void SetCanDoRun(bool _bool) => _animator.SetBool("CanDoRun", _bool);

        public float GetPickupAnimDuration()
        {
            return .833f;
        }
    }
}

