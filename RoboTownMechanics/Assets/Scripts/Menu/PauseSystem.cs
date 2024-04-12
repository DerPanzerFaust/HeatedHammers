using UnityEngine;


namespace PauseHandler
{
    public class PauseSystem : SingletonBehaviour<PauseSystem>
    {
        //--------------------Public--------------------//
        public bool isPaused;


        /// <summary>
        /// the function which pauses the game by setting the timescale to 0
        /// </summary>
        public void PausingGame()
        {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0 : 1;
        }


        /// <summary>
        /// This function will resume the game as needed
        /// </summary>
        public void ResumingGame()
        {
           
        }
    }
}