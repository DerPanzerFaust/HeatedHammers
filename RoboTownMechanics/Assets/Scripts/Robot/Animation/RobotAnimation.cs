using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Robot.Animations
{
    public class RobotAnimation : MonoBehaviour
    {
        //--------------------Private--------------------//
        private Animator _animator;

        //--------------------Functions--------------------//
        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void DoIdleAnimation()
        {
            _animator.SetTrigger("Idle");
        }
        public void DoEnterAnimation()
        {
            _animator.SetTrigger("EnterShop");
        }
        public void DoLeaveSuccesAnimation()
        {
            _animator.SetTrigger("LeaveSucces");
        }
        public void DoLeaveUnSuccesAnimation()
        {
            _animator.SetTrigger("LeaveUnSucces");
        }
        public void DoSmashAnimation()
        {
            _animator.SetTrigger("Smash");
        }
    }
}