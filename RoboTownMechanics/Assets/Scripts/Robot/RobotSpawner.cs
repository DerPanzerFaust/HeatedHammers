using Robot.Animations;
using System;
using System.Collections;
using UnityEngine;

namespace Robot.Spawner
{
    public class RobotSpawner : SingletonBehaviour<RobotSpawner>
    {
        //--------------------Private--------------------//
        private RobotAnimation _robotAnimation;

        [SerializeField]
        private GameObject _robotObject;

        //--------------------Public--------------------//
        private Action OnEnteredShop;
        private Action OnExitedShopSucces;
        private Action OnExitedShopUnSucces;

        //--------------------Functions--------------------//
        private void Start()
        {
            _robotAnimation = _robotObject.GetComponent<RobotAnimation>();
        }

        /// <summary>
        /// 
        /// </summary>
        public void SpawnRobot()
        {
            _robotAnimation.DoEnterAnimation();
            StartCoroutine(SpawnRobotRoutine());
        }

        private IEnumerator SpawnRobotRoutine()
        {
            yield return new WaitForSeconds(6.458f);
            OnEnteredShop?.Invoke();
        }

        /// <summary>
        /// 
        /// </summary>
        public void DeSpawnRobotSucces()
        {
            _robotAnimation.DoLeaveSuccesAnimation();
            StartCoroutine(DeSpawnRobotSuccesRoutine());
        }

        private IEnumerator DeSpawnRobotSuccesRoutine()
        {
            yield return new WaitForSeconds(5.417f);
            OnExitedShopSucces?.Invoke();
        }

        /// <summary>
        /// 
        /// </summary>
        public void DeSpawnRobotUnSucces()
        {
            _robotAnimation.DoLeaveUnSuccesAnimation();
            StartCoroutine(DeSpawnRobotUnSuccesRoutine());
        }

        private IEnumerator DeSpawnRobotUnSuccesRoutine()
        {
            yield return new WaitForSeconds(5.833f);
            OnExitedShopUnSucces?.Invoke();
        }
    }
}

