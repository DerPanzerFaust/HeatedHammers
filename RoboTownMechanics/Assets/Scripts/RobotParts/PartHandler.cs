using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PartsHandler
{
    public class PartHandler : SingletonBehaviour<PartHandler>
    {
        //--------------------Public--------------------//

        //--------------------Private--------------------//

        [SerializeField]
        private float _explodetimer;
        

        public bool _repaired;

        //--------------------Functions--------------------//


        // Update is called once per frame
        void Update()
        {
            if (_repaired) //This statement will be changed to a statement from the pickup script
            {
                _explodetimer -= Time.deltaTime;
                ExplosionTimer();
                //then start a timer 
            }


            Debug.Log(_explodetimer);

            
        }

        public void ExplosionTimer()
        {
            if (_explodetimer < 0)
            {
                Debug.Log("Exploded!");
                Destroy(gameObject);
                //then part "explodes" (animation)
                //and change mesh
            }
        }

    }

}