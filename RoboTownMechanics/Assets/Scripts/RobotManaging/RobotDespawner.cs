using UnityEngine;
using ObjectPooler;


namespace RobotTimer {
    public class RobotDespawner : MonoBehaviour
    {
        //--------------------Private--------------------//

        private float _saveTime;

        private GameObject _platform;
   


        //--------------------Functions--------------------//

        private void Start()
        {
           // _saveTime = ObjectPooling.robotTime;

            _platform = GameObject.FindWithTag("Platform");
            
            Debug.Log(_platform.transform.position);
        }

        void Update()
        {
            //ObjectPooling.robotTime -= Time.deltaTime;

            //if (ObjectPooling.robotTime < 0)
                timerEnded();
            


        }

        private void timerEnded()
        {
           gameObject.SetActive(false);
            //ObjectPooling.robotTime = _saveTime;
        }
    }
}

